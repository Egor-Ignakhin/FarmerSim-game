using FarmerSim.Triggers;

using System;

using UnityEngine;

namespace FarmerSim.Mud
{
    public sealed class WheatPackTriggerHandler : MonoBehaviour, ITriggerHandler
    {
        public event Action OnColliderEnter;
        public event Action OnColliderExit;
        private Collider lastEnteredCollider;
        private Collider lastExitedCollider;

        [SerializeField] private Collider playerCollider;

        public Collider GetLastEnteredCollider()
        {
            return lastEnteredCollider;
        }

        public Collider GetLastExitedCollider()
        {
            return lastExitedCollider;
        }

        public void OnTriggerEnter(Collider other)
        {
           lastEnteredCollider = other;

            OnColliderEnter?.Invoke();
        }

        public void OnTriggerExit(Collider other)
        {
            lastExitedCollider = other;

            OnColliderExit?.Invoke();
        }
    }
}