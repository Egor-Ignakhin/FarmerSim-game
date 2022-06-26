using FarmerSim.Invnentory;

using System;

namespace FarmerSim.Player.Inventory
{
    public interface IPlayerInventoryController : IInventoryController
    {
        event Action<int> OnItemSold;
    }
}