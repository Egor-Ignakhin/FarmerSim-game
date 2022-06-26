using FarmerSim.Player.Inventory;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Mud
{
    public sealed class DefaultMudController : MonoBehaviour, IMudController
    {
        [SerializeField] private List<GameObject> cropObjectsGMs = new List<GameObject>();
        private readonly List<ICrop> cropObjects = new List<ICrop>();

        [SerializeField] private GameObject playerInventoryControllerGM;
        private IPlayerInventoryController playerInventoryController;

        [SerializeField] private Collider playerCollider;

        private void Awake()
        {
            playerInventoryController = playerInventoryControllerGM.GetComponent<IPlayerInventoryController>();
            for (int i = 0; i < cropObjectsGMs.Count; i++)
            {
                ICrop crop = cropObjectsGMs[i].GetComponent<ICrop>();
                cropObjects.Add(crop);

                crop.OnSnipped += OnCropSnipped;
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                foreach (var wheatObject in cropObjects)
                {
                    wheatObject.Grow(Time.deltaTime);
                }

                yield return null;
            }
        }

        private void OnCropSnipped(ICrop sender)
        {
            DropPack(sender);
            DropSlicedWheat(sender);
            EnableVFX(sender);
        }

        private void DropPack(ICrop sender)
        {
            var pack = Instantiate(Resources.Load<WheatCropPack>("InteractableWheatPack"));
            pack.transform.position = sender.GetCropPackInstantiatePosition();
            pack.Initialize(playerInventoryController, playerCollider);
        }

        private void DropSlicedWheat(ICrop sender)
        {
            GameObject slicedWheat = Instantiate(Resources.Load<GameObject>("SlicedGrass"));
            slicedWheat.transform.position = sender.GetCropPackInstantiatePosition();
        }

        private void EnableVFX(ICrop sender)
        {
            sender.PlayVFX();
        }

        private void OnDestroy()
        {
            foreach(ICrop crop in cropObjects)
            {
                crop.OnSnipped -= OnCropSnipped;
            }
        }
    }
}