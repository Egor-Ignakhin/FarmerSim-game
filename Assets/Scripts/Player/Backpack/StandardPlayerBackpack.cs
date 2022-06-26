
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace FarmerSim.Player.Inventory
{
    public class StandardPlayerBackpack : MonoBehaviour, IPlayerBackpack
    {
        [SerializeField] private List<Transform> inventoryItems = new List<Transform>();

        public Transform GetItem<T>()
        {
            return inventoryItems.Last(item => item.gameObject.activeInHierarchy);
        }
    }
}