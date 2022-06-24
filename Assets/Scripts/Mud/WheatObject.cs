
using EzySlice;

using FarmerSim.Invnentory;

using System;

using UnityEngine;

namespace FarmerSim.Mud
{
    public class WheatObject : MonoBehaviour
    {
        private float currentRiseTime = 0;
        private readonly float riseTime = 10;
        private bool cutted = true;
        [SerializeField] private GameObject meshRendGM;
        [SerializeField] private Collider mcollider;
        [SerializeField] private Transform wheatPackInstantiatePlace;

        private IInventoryController playerInventoryController;

        internal void Initialize(IInventoryController playerInventory)
        {
            this.playerInventoryController = playerInventory;
        }

        internal void Up(float increaseValue)
        {
            if (!CanUp())
            {
                if (cutted)
                {
                    ShowCulture();
                }
                return;
            }

            currentRiseTime += increaseValue;
        }

        private bool CanUp()
        {
            return currentRiseTime < riseTime;
        }

        internal void Cut()
        {
            meshRendGM.SetActive(false);
            currentRiseTime = 0;
            mcollider.enabled = false;

            cutted = true;

            DropPack();
            DropSlicedWheat();
        }

        private void DropPack()
        {
            var pack = Instantiate(Resources.Load<WheatPack>("WheatPack"));
            pack.transform.position = wheatPackInstantiatePlace.position;
            pack.Initialize(playerInventoryController);
        }

        private void DropSlicedWheat()
        {
            GameObject slicedWheat = Instantiate(Resources.Load<GameObject>("SlicedGrass"));
            slicedWheat.transform.position = wheatPackInstantiatePlace.position;
        }

        private void ShowCulture()
        {
            meshRendGM.SetActive(true);
            mcollider.enabled = true;

            cutted = false;
        }
    }
}