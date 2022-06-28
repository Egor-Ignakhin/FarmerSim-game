using System;

using UnityEngine;

namespace FarmerSim
{
    public abstract class PoolableObject : MonoBehaviour
    {
        public event Action<PoolableObject> Realized;

        public void Realize()
        {
            Realized?.Invoke(this);
        }
    }
}