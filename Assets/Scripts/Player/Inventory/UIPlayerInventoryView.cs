
using FarmerSim.Invnentory;

using UnityEngine;

namespace FarmerSim.Player
{
    public class UIPlayerInventoryView : MonoBehaviour, IInventoryView
    {
        private IInventoryModel playerInventoryModel;

        [SerializeField] private TMPro.TextMeshProUGUI packsCountText;

        public void Initialize(IInventoryModel inventoryModel)
        {
            this.playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCountByType<WheatPackItem>();
            packsCountText.SetText($"{packsCount}/{playerInventoryModel.GetMaxItemsCount()}");
        }

        private void OnDestroy()
        {
            playerInventoryModel.InventoryModelChanged -= OnInventoryModelChanged;
        }
    }
}