
using FarmerSim.Player;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Barn
{
    public class BarnController : MonoBehaviour
    {
        [SerializeField] private BarnAreaChecker mudAreaChecker;
        [SerializeField] private DefaultPlayerController playerController;

        [SerializeField] private BoxPlayerInventoryView boxPlayerInventoryView;

        private bool canTakeWheat;

        private List<Transform> takedPacks = new List<Transform>();
        [SerializeField] private Transform packsTarget;

        private void Awake()
        {
            mudAreaChecker.OnPlayerEnter += OnPlayerEnterInArea;
            mudAreaChecker.OnPlayerExit += OnPlayerExitFromArea;
        }

        private void OnPlayerEnterInArea()
        {
            if (playerController.HaveItems<WheatPackItem>())
            {
                canTakeWheat = true;
            }
        }

        private void Update()
        {
            if (canTakeWheat)
            {
                TakeWheat();
            }
            if(takedPacks.Count > 0)
            {
                MoveTakedPacks();
            }
        }

        private void TakeWheat()
        {
            if (playerController.HaveItems<WheatPackItem>())
            {
                var pack = boxPlayerInventoryView.GetUpperWheatPack();
                var packInst = Instantiate(pack).transform;
                packInst.gameObject.SetActive(true);
                packInst.position = pack.transform.position;
                packInst.rotation = pack.transform.rotation;

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

                if(pack.position == packsTarget.position)
                {
                    Destroy(pack.gameObject);
                    takedPacks.RemoveAt(i);
                }
            }
        }


        private void OnPlayerExitFromArea()
        {
            canTakeWheat = false;
        }

        private void OnDestroy()
        {
            mudAreaChecker.OnPlayerEnter -= OnPlayerEnterInArea;
            mudAreaChecker.OnPlayerExit -= OnPlayerExitFromArea;
        }
    }
}