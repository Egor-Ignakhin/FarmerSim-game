using System.Collections;

using UnityEngine;

namespace FarmerSim
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float time;

        private IEnumerator Start()
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}