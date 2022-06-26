using System;

using UnityEngine;

namespace FarmerSim.Triggers
{
    public interface ITriggerHandler
    {
        public event Action OnColliderEnter;
        public event Action OnColliderExit;

        public Collider GetLastEnteredCollider();
        public Collider GetLastExitedCollider();

        void OnTriggerEnter(Collider other);

        void OnTriggerExit(Collider other);
    }
}