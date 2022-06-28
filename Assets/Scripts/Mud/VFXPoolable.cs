using UnityEngine;

namespace FarmerSim.Mud
{
    public class VFXPoolable : PoolableObject
    {
        [SerializeField] private ParticleSystem mparticleSystem;

        public void Play()
        {
            mparticleSystem.Play();
        }
    }
}