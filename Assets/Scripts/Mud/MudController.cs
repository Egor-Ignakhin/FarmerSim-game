
using FarmerSim.Player;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Mud
{
    public class MudController : MonoBehaviour
    {
        [SerializeField] private List<WheatObject> wheatObjects = new List<WheatObject>();
        [SerializeField] private PlayerInventoryController playerInventory;

        private void Awake()
        {
            foreach (var wheatObject in wheatObjects)
            {
                wheatObject.Initialize(playerInventory);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                foreach (var wheatObject in wheatObjects)
                {
                    wheatObject.Up(Time.deltaTime);
                }

                yield return null;
            }
        }
    }
}