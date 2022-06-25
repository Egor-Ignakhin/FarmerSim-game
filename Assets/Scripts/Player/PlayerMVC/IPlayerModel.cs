using System;

namespace FarmerSim.Player
{
    public interface IPlayerModel
    {
        event Action OnPlayerChanged;

        IPlayerBehavior GetCurrentBehavior();
        void SetCurrentBehavior(IPlayerBehavior playerBehavior);

        IPlayerBehavior GetLastBehavior();

        bool HaveItems<T>();

        IPlayerWeapon GetCurrentWeapon();
        void SetCurrentWeapon(IPlayerWeapon value);
    }
}