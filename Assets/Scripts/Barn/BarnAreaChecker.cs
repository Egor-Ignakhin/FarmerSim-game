using System;

using UnityEngine;

namespace FarmerSim.Barn
{
    internal class BarnAreaChecker : MonoBehaviour
    {
        public event Action OnPlayerEnter;
        public event Action OnPlayerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<CharacterController>())
            {
                OnPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.GetComponent<CharacterController>())
            {
                OnPlayerExit?.Invoke();
            }
        }
    }
}