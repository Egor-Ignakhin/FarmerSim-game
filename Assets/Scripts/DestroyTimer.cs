using System.Collections;

using UnityEngine;

namespace FarmerSim
{
    public class DestroyTimer : MonoBehaviour
    {
        [SerializeField] private float delay = 5;

        private IEnumerator Start()
        {
            while (delay > 0)
            {
                delay -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}