
using FarmerSim.Invnentory;

using UnityEngine;

namespace FarmerSim.Player
{
    public class UIPlayerInventoryView : MonoBehaviour, IInventoryView
    {
        private IInventoryModel playerInventoryModel;

        [SerializeField] private TMPro.TextMeshProUGUI packsCountText;
        [SerializeField] private TMPro.TextMeshProUGUI moneyCountText;

        public void Initialize(IInventoryModel inventoryModel)
        {
            this.playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCountByType<WheatPackItem>();
            var moneyCount = playerInventoryModel.GetMoneyCount();

            packsCountText.SetText($"{packsCount}/{playerInventoryModel.GetMaxItemsCount()}");
            moneyCountText.SetText($"{moneyCount}");
        }

        private void OnDestroy()
        {
            playerInventoryModel.InventoryModelChanged -= OnInventoryModelChanged;
        }
    }
}