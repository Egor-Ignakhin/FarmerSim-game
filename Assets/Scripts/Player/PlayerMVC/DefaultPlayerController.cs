
using UnityEngine;

namespace FarmerSim.Player
{
    public class DefaultPlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerModel playerModel;

        [SerializeField] private GameObject playerViewGM;
        private IPlayerView playerView;

        [SerializeField] private FloatingJoystick floatingJoystick;

        [SerializeField] private CharacterController characterController;

        [SerializeField] private Menu.CutWheatButton cutWheatButton;

        [SerializeField] private GameObject playerKnifeGM;


        [SerializeField] private PlayerInventoryController playerInventoryController;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        public float RotationSmoothTime = 0.12f;

        private void Awake()
        {
            InitializeMVC();

            cutWheatButton.OnPointerDownEvent += () =>
            {
                playerModel.SetCurrentBehavior(new PlayerBehaviorCutting());
                playerModel.SetCurrentWeapon(playerKnifeGM.GetComponent<IPlayerWeapon>());
            };
            cutWheatButton.OnPointerUpEvent += () =>
            {
                playerModel.SetCurrentBehavior(playerModel.GetLastBehavior());
                playerModel.SetCurrentWeapon(null);
            };
        }

        private void InitializeMVC()
        {
            playerModel = new DefaultPlayerModel(playerInventoryController);
            playerView = playerViewGM.GetComponent<IPlayerView>();

            playerView.Initialize(playerModel);
        }

        private void Update()
        {
            if (playerModel.GetCurrentBehavior() is PlayerBehaviorCutting)
                return;

            OperatePlayerMovement();
        }

        private void OperatePlayerMovement()
        {
            Vector2 direction = floatingJoystick.Direction;

            if (direction.y == 0)
            {
                if (direction.x != 0)
                {
                    playerModel.SetCurrentBehavior(new PlayerBehaviorWalking());
                }
                else
                {
                    playerModel.SetCurrentBehavior(new PlayerBehaviorIdle());
                }
            }
            else
            {
                float runningSpeed = Vector2.Distance(Vector2.zero, direction);
                playerModel.SetCurrentBehavior(new PlayerBehaviorRunning(runningSpeed));
            }

            RotateCharacter(new Vector3(floatingJoystick.Direction.x, 0, floatingJoystick.Direction.y));
        }

        public void RotateCharacter(Vector3 direction)
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                  Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        public bool HaveItems<T>()
        {
            return playerModel.HaveItems<T>();
        }
    }
}