
using UnityEngine;

namespace FarmerSim.Player
{
    public class PlayerInputHandler
    {
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        public float RotationSmoothTime = 0.12f;

        public IPlayerBehavior ComputeCurrentPlayerBehavior(Vector2 joystickDirection)
        {
            if (joystickDirection.y == 0)
            {
                if (joystickDirection.x != 0)
                {
                    return new PlayerBehaviorWalking();
                }
                else
                {
                    return new PlayerBehaviorIdle();
                }
            }
            else
            {
                float runningSpeed = Vector2.Distance(Vector2.zero, joystickDirection);
                return new PlayerBehaviorRunning(runningSpeed);
            }
        }

        internal Quaternion ComputeCurrentRotation(Vector3 direction, Vector3 currentGlobalEulers)
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                     Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(currentGlobalEulers.y,
                _targetRotation,
                ref _rotationVelocity,
                RotationSmoothTime);

            return Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
}