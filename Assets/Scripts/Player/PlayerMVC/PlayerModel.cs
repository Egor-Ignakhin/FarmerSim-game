using System;

namespace FarmerSim.Player
{
    public class DefaultPlayerModel : IPlayerModel
    {
        public event Action OnPlayerChanged;
        private IPlayerBehavior currentBehavior;
        private IPlayerBehavior lastPlayerBehavior;

        public IPlayerBehavior GetCurrentBehavior()
        {
            return currentBehavior;
        }

        public void SetCurrentBehavior(IPlayerBehavior playerBehavior)
        {
            lastPlayerBehavior = currentBehavior;

            currentBehavior = playerBehavior;

            OnPlayerChanged?.Invoke();
        }

        public IPlayerBehavior GetLastBehavior()
        {
            return lastPlayerBehavior;
        }
    }
}