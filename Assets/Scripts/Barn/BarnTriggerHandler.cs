using FarmerSim.Player;
using FarmerSim.Player.Inventory;
using FarmerSim.Triggers;

using System;

using UnityEngine;

namespace FarmerSim.Barn
{
    internal sealed class BarnTriggerHandler : MonoBehaviour, ITriggerHandler
    {
        [SerializeField] private GameObject defaultPlayerInventaryControllerGM;
        private IPlayerInventoryController defaultPlayerInventaryController;

        public event Action OnColliderEnter;
        public event Action OnColliderExit;
        private Collider lastEnteredObject;
        private Collider lastExitedObject;

        [SerializeField] private Collider playerCollider;

        [SerializeField] private BarnWheatService barnWheatService;

        private void Awake()
        {
            defaultPlayerInventaryController = defaultPlayerInventaryControllerGM.GetComponent<IPlayerInventoryController>();

            barnWheatService.Initialize();
        }


        public Collider GetLastEnteredCollider()
        {
            return lastEnteredObject;
        }

        public Collider GetLastExitedCollider()
        {
            return lastExitedObject;
        }

        private void Update()
        {
            barnWheatService.Update();
        }

        public void OnTriggerEnter(Collider other)
        {
            lastEnteredObject = other;
            OnColliderEnter?.Invoke();

            if (lastEnteredObject != playerCollider)
                return;

            if (defaultPlayerInventaryController.HaveItems<WheatPackItem>())
            {
                barnWheatService.SetCanTakeWheat(true);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            lastExitedObject = other;
            OnColliderExit?.Invoke();

            if (lastExitedObject != playerCollider)
                return;

            barnWheatService.SetCanTakeWheat(false);
        }
    }
}