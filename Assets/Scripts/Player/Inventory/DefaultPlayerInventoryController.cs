
using FarmerSim.Invnentory;

using System;

using UnityEngine;

namespace FarmerSim.Player.Inventory
{
    internal class DefaultPlayerInventoryController : MonoBehaviour, IPlayerInventoryController
    {
        public event Action<int> OnItemSold;

        private IInventoryModel model;

        [SerializeField] private GameObject playerInventoryViewUIGM;
        private IPlayerInventoryView playerInventoryViewUI;

        [SerializeField] private GameObject playerInventoryViewBackpackGM;
        private IPlayerInventoryView playerInventoryViewBackpack;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            model = new DefaultPlayerInventoryModel();
            playerInventoryViewUI = playerInventoryViewUIGM.GetComponent<IPlayerInventoryView>();
            playerInventoryViewBackpack = playerInventoryViewBackpackGM.GetComponent<IPlayerInventoryView>();

            playerInventoryViewUI.Initialize(model);
            playerInventoryViewBackpack.Initialize(model);
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

            OnItemSold?.Invoke(moneyCount);
        }
    }
}