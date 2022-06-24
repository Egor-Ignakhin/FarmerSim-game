using UnityEngine;

public interface IPlayerWeapon
{
    void SetCanAttack(bool value);

    Vector3 GetWorldPosition();
    Vector3 GetWorldDirection();
}
