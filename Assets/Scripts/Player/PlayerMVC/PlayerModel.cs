using System;

namespace FarmerSim.Player
{
    public class DefaultPlayerModel : IPlayerModel
    {
        public event Action OnPlayerChanged;
        private IPlayerBehavior currentBehavior;

        public IPlayerBehavior GetCurrentBehavior()
        {
            return currentBehavior;
        }

        public void SetCurrentBehavior(IPlayerBehavior playerBehavior)
        {
            currentBehavior = playerBehavior;

            OnPlayerChanged?.Invoke();
        }
    }
}