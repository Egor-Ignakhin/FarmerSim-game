
using FarmerSim.Invnentory;
using FarmerSim.Player;

using UnityEngine;

namespace FarmerSim.Mud
{
    public class WheatPack : MonoBehaviour
    {
        private Transform target;
        private IInventoryController playerInventory;

        internal void Initialize(IInventoryController playerInventory)
        {
            this.playerInventory = playerInventory;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController>
                (out var character))
            {
                target = character.transform;
            }
        }

        private void Update()
        {
            if (!target)
                return;

            if (!playerInventory.IsNotFilled())
                return;

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2);

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                playerInventory.PushItem(new WheatPackItem());

                Destroy(gameObject);
            }
        }
    }
}