namespace FarmerSim.Player
{
    public class PlayerBehaviorRunning : IPlayerBehavior
    {
        private readonly float directionSign;

        public PlayerBehaviorRunning(float directionSign)
        {
            this.directionSign = directionSign;
        }

        internal float GetDirectionSign()
        {
            return directionSign;
        }
    }
}