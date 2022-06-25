
using UnityEngine;

namespace FarmerSim.Player
{
    public class PlayerKnife : MonoBehaviour, IPlayerWeapon
    {
        [SerializeField] private Transform rayStartObject;
        private bool canAttack;
        [SerializeField] private MeshRenderer meshRenderer;

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
            Ray ray = new Ray(rayStartObject.position, rayStartObject.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 2, ~0))
            {
                if (hit.transform.TryGetComponent<Mud.WheatObject>(out var wheat)){
                    wheat.Cut();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(rayStartObject.position, rayStartObject.forward);
        }

        public Vector3 GetWorldPosition()
        {
            return rayStartObject.position;
        }

        public Vector3 GetWorldDirection()
        {
            return rayStartObject.forward;
        }
    }
}