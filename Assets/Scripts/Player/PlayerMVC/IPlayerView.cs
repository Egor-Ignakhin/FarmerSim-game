namespace FarmerSim.Player
{
    public interface IPlayerView
    {
        IPlayerModel PlayerModel { get; set; }
        void Initialize(IPlayerModel playerModel);
    }
}