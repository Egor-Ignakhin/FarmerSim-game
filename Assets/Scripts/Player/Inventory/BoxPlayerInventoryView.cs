
using FarmerSim.Invnentory;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player
{
    public class BoxPlayerInventoryView : MonoBehaviour, IInventoryView
    {
        public event Action<int> OnWheatPackSelled;
        private IInventoryModel playerInventoryModel;
        [SerializeField] private List<GameObject> wheatPacks = new List<GameObject>();

        public void Initialize(IInventoryModel inventoryModel)
        {
            this.playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCountByType<WheatPackItem>();
            for (int i = 0; i < wheatPacks.Count; i++)
            {
                wheatPacks[i].SetActive(packsCount > i);
            }
        }

        internal GameObject GetUpperWheatPack()
        {
            for (int i = wheatPacks.Count - 1; i >= 0; i--)
            {
                if (wheatPacks[i].activeInHierarchy)
                {
                    OnWheatPackSelled?.Invoke(15);
                    return wheatPacks[i];
                }
            }

            return null;
        }

        private void OnDestroy()
        {
            playerInventoryModel.InventoryModelChanged -= OnInventoryModelChanged;
        }
    }
}