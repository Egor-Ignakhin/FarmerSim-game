
using FarmerSim.Player;

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

        private readonly List<Transform> takedPacks = new List<Transform>();
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
            if (takedPacks.Count > 0)
            {
                MoveTakedPacks();
            }
        }

        private void TakeWheat()
        {
            if (playerController.HaveItems<WheatPackItem>())
            {
                Transform pack = boxPlayerInventoryView.GetUpperWheatPack().transform;
                var packInst = Instantiate(pack);
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
                    Destroy(pack.gameObject);
                    takedPacks.RemoveAt(i);
                    boxPlayerInventoryView.SellWheatPack();
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