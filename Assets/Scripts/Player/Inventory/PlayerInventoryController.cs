
using FarmerSim.Invnentory;

using UnityEngine;

namespace FarmerSim.Player
{
    internal class PlayerInventoryController : MonoBehaviour, IInventoryController
    {
        private IInventoryModel model;

        [SerializeField] private GameObject playerInventoryViewUIGM;
        [SerializeField] private GameObject playerInventoryViewBoxGM;

        [SerializeField] private IInventoryView playerInventoryViewUI;
        [SerializeField] private IInventoryView playerInventoryViewBox;

        private void Awake()
        {
            model = new DefaultPlayerInventoryModel();
            playerInventoryViewUI = playerInventoryViewUIGM.GetComponent<IInventoryView>();
            playerInventoryViewBox = playerInventoryViewBoxGM.GetComponent<IInventoryView>();

            playerInventoryViewUI.Initialize(model);
            playerInventoryViewBox.Initialize(model);
        }

        public void PushItem(IInventoryItem item)
        {
            model.PushItem(item);
        }

        public bool IsNotFilled()
        {
            return model.IsNotFilled();
        }

        public bool HaveItems<T>()
        {
            return model.HaveItems<T>();
        }

        public void SellItem<T>(int moneyCount)
        {
            model.SellItem<T>(moneyCount);
        }
    }
}