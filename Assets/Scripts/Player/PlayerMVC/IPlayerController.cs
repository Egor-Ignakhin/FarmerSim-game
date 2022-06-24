public interface IPlayerController
{
    IPlayerWeapon GetCurrentWeapon();

    bool HaveItems<T>();
}
