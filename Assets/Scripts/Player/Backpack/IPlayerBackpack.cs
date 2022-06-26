using UnityEngine;

namespace FarmerSim.Player
{
    public interface IPlayerBackpack
    {
        Transform GetItem<T>();
    }
}