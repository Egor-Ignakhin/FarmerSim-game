using FarmerSim.Player.Inventory;

using UnityEngine;

namespace FarmerSim.Mud
{
    public interface ICropPack
    {
        void Initialize(IPlayerInventoryController playerInventoryController, Collider playerCollider);
    }
}