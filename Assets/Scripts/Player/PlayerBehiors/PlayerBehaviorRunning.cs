using System;

namespace FarmerSim.Player
{
    public class PlayerBehaviorRunning : IPlayerBehavior
    {
        private float directionSign;
        public PlayerBehaviorRunning(float directionSign)
        {
            this.directionSign = directionSign;
        }

        public void DoAction()
        {
            throw new System.NotImplementedException();
        }

        internal float GetDirectionSign()
        {
            return directionSign;
        }
    }
}