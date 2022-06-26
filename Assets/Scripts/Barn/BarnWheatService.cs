using FarmerSim.Player;
using FarmerSim.Player.Inventory;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Barn
{
    [Serializable]
    public sealed class BarnWheatService
    {
        private bool canTakeWheat;
        private readonly List<Transform> takedPacks = new List<Transform>();
        [SerializeField] private Transform packsTarget;

        [SerializeField] private GameObject defaultPlayerInventaryControllerGM;
        private IPlayerInventoryController defaultPlayerInventaryController;

        [SerializeField] private GameObject standardPlayerBackpackGM;
        private IPlayerBackpack standardPlayerBackpack;

        internal void Initialize()
        {
            standardPlayerBackpack = standardPlayerBackpackGM.GetComponent<IPlayerBackpack>();
            defaultPlayerInventaryController = defaultPlayerInventaryControllerGM.GetComponent<IPlayerInventoryController>();
        }

        internal void Update()
        {
            if (canTakeWheat)
            {
                TakeWheat();
            }
            if (takedPacks.Count > 0)
            {
                MoveTakedPacks();
            }
        }

        private void TakeWheat()
        {
            if (defaultPlayerInventaryController.HaveItems<WheatPackItem>())
            {
                Transform pack = standardPlayerBackpack.GetItem<WheatPackItem>();
                Transform packInst = UnityEngine.Object.Instantiate(pack);
                packInst.gameObject.SetActive(true);
                packInst.SetPositionAndRotation(pack.position, pack.rotation);

                takedPacks.Add(packInst);
            }
        }

        private void MoveTakedPacks()
        {
            for (int i = 0; i < takedPacks.Count; i++)
            {
                Transform pack = takedPacks[i];
                pack.position = Vector3.MoveTowards(pack.position,
                    packsTarget.position, Time.deltaTime * 10);

                if (pack.position == packsTarget.position)
                {
                    UnityEngine.Object.Destroy(pack.gameObject);
                    takedPacks.RemoveAt(i);
                    defaultPlayerInventaryController.SellItem<WheatPackItem>(15);
                }
            }
        }

        internal void SetCanTakeWheat(bool value)
        {
            canTakeWheat = value;
        }
    }
}