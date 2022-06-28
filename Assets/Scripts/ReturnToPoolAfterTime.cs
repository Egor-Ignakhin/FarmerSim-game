
using System.Collections;

using UnityEngine;

namespace FarmerSim
{
    public class ReturnToPoolAfterTime : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private PoolableObject poolableObject;

        private void OnEnable()
        {
            StartCoroutine(nameof(Timer));
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(delay);
            poolableObject.Realize();
        }
    }
}