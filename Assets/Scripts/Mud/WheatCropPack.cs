using FarmerSim.Player;
using FarmerSim.Player.Inventory;
using FarmerSim.Triggers;

using UnityEngine;

namespace FarmerSim.Mud
{
    public class WheatCropPack : MonoBehaviour, ICropPack
    {
        private Transform target;
        private IPlayerInventoryController playerInventory;

        [SerializeField] private GameObject mtriggerHandlerGM;
        private ITriggerHandler mtriggerHandler;

        public void Initialize(IPlayerInventoryController playerInventory, Collider playerCollider)
        {
            this.playerInventory = playerInventory;
            mtriggerHandler = mtriggerHandlerGM.GetComponent<ITriggerHandler>();


            mtriggerHandler.OnColliderEnter += () =>
            {
                if (mtriggerHandler.GetLastEnteredCollider() == playerCollider)
                {
                    target = playerCollider.transform;
                }
            };
            mtriggerHandler.OnColliderExit += () =>
            {
                if (mtriggerHandler.GetLastExitedCollider() == playerCollider)
                {
                    target = null;
                }
            };


        }

        private void Update()
        {
            if (!target)
                return;

            if (!playerInventory.IsNotFilled())
                return;

            Move();

            if (CanPushItemInPlayerInventory())
            {
                Push();
            }
        }

        private bool CanPushItemInPlayerInventory()
        {
            return Vector3.Distance(transform.position, target.position) < 0.3f;
        }

        private void Push()
        {
            playerInventory.PushItem(new WheatPackItem());

            Destroy(gameObject);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                target.position, Time.deltaTime * 2);
        }
    }
}