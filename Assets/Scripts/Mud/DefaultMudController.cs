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

        private MudWheatPackPool wheatPackPool;
        private SlicedGrassPool slicedGrassPool;
        private SliceVFXPool vfxpool;

        private void Awake()
        {
            playerInventoryController = playerInventoryControllerGM.GetComponent<IPlayerInventoryController>();
            for (int i = 0; i < cropObjectsGMs.Count; i++)
            {
                ICrop crop = cropObjectsGMs[i].GetComponent<ICrop>();
                cropObjects.Add(crop);

                crop.OnSnipped += OnCropSnipped;
            }
            wheatPackPool = new MudWheatPackPool(transform, cropObjects.Count);
            slicedGrassPool = new SlicedGrassPool(transform, cropObjects.Count);
            vfxpool = new SliceVFXPool(transform, cropObjects.Count);
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
            WheatPackPoolable pack = wheatPackPool.GetObjectFromPool();
            pack.transform.position = sender.GetCropPackInstantiatePosition();
            pack.GetComponent<ICropPack>().Initialize(playerInventoryController, playerCollider);
        }

        private void DropSlicedWheat(ICrop sender)
        {
            GameObject slicedWheat = slicedGrassPool.GetObjectFromPool().gameObject;
            slicedWheat.transform.position = sender.GetCropPackInstantiatePosition();
        }

        private void EnableVFX(ICrop sender)
        {
            VFXPoolable vfx = vfxpool.GetObjectFromPool();
            vfx.transform.position = sender.GetCropPackInstantiatePosition();
            vfx.Play();
        }

        private void OnDestroy()
        {
            foreach (ICrop crop in cropObjects)
            {
                crop.OnSnipped -= OnCropSnipped;
            }
        }
    }
}