
using System;

using UnityEngine;

namespace FarmerSim.Mud
{
    public class WheatCrop : MonoBehaviour, ICrop
    {
        public event Action<ICrop> OnSnipped;
        private float currentRiseTime = 0;
        private readonly float riseTime = 10;
        private bool snipped = true;
        [SerializeField] private GameObject meshRendGM;
        [SerializeField] private Collider mcollider;
        [SerializeField] private Transform wheatPackInstantiatePlace;
        [SerializeField] private ParticleSystem mparticleSystem;

        public void Grow(float riseDelta)
        {
            if (!CanUp())
            {
                if (snipped)
                {
                    ShowCulture();
                }
                return;
            }

            currentRiseTime += riseDelta;
        }

        private bool CanUp()
        {
            return currentRiseTime < riseTime;
        }

        private void ShowCulture()
        {
            meshRendGM.SetActive(true);
            mcollider.enabled = true;

            snipped = false;
        }

        public void Snip()
        {
            meshRendGM.SetActive(false);
            currentRiseTime = 0;
            mcollider.enabled = false;

            snipped = true;

            OnSnipped?.Invoke(this);
        }

        public Vector3 GetCropPackInstantiatePosition()
        {
            return wheatPackInstantiatePlace.position;
        }

        public void PlayVFX()
        {
            mparticleSystem.Play();
        }
    }
}