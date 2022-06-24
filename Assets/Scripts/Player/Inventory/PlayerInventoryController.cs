
using FarmerSim.Invnentory;

using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player
{
    internal class PlayerInventoryController : MonoBehaviour, IInventoryController
    {
        private IInventoryModel model;

        [SerializeField] private List<GameObject> playerInventoryViewGMs = new List<GameObject>();
        private readonly List<IInventoryView> playerInventoryViews = new List<IInventoryView>();

        private void Awake()
        {
            model = new DefaultPlayerInventoryModel();
            foreach (var viewGM in playerInventoryViewGMs)
            {
                IInventoryView playerInventoryView = viewGM.GetComponent<IInventoryView>();
                playerInventoryViews.Add(playerInventoryView);

                playerInventoryView.Initialize(model);
            }
        }

        public void PushItem(IInventoryItem item)
        {
            model.PushItem(item);
        }

        public bool IsNotFilled()
        {
            return model.IsNotFilled();
        }
    }
}