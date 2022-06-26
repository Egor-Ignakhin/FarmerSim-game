
using FarmerSim.Mud;

using UnityEngine;

namespace FarmerSim.Player
{
    public class PlayerKnife : MonoBehaviour, IPlayerWeapon
    {
        [SerializeField] private Transform guideObject;
        [SerializeField] private MeshRenderer meshRenderer;

        private bool canAttack;

        private void Update()
        {
            if (canAttack)
                Attack();
        }

        public void SetCanAttack(bool value)
        {
            canAttack = value;
            meshRenderer.enabled = canAttack;
        }

        private void Attack()
        {
            Ray ray = new Ray(guideObject.position, guideObject.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 2, ~0))
            {
                if (hit.transform.TryGetComponent(out ICrop crop))
                {
                    crop.Snip();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(guideObject.position, guideObject.forward);
        }
    }
}