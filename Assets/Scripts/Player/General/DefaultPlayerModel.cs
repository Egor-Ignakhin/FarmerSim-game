using FarmerSim.Player.Inventory;

using System;

namespace FarmerSim.Player
{
    public class DefaultPlayerModel : IPlayerModel
    {
        public event Action Changed;
        private IPlayerBehavior currentBehavior;
        private IPlayerBehavior lastPlayerBehavior;
        private readonly IPlayerInventoryController inventoryController;
        private IPlayerWeapon currentWeapon;

        public DefaultPlayerModel(IPlayerInventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        public IPlayerBehavior GetCurrentBehavior()
        {
            return currentBehavior;
        }

        public void SetCurrentBehavior(IPlayerBehavior playerBehavior)
        {
            lastPlayerBehavior = currentBehavior;

            currentBehavior = playerBehavior;

            Changed?.Invoke();
        }

        public IPlayerBehavior GetLastBehavior()
        {
            return lastPlayerBehavior;
        }

        public bool HaveItems<T>()
        {
            return inventoryController.HaveItems<T>();
        }

        public IPlayerWeapon GetCurrentWeapon()
        {
            return currentWeapon;
        }
        public void SetCurrentWeapon(IPlayerWeapon value)
        {
            currentWeapon?.SetCanAttack(false);

            currentWeapon = value;

            currentWeapon?.SetCanAttack(true);
        }
    }
}