
using UnityEngine;

namespace FarmerSim.Player
{
    public class DefaultPlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerModel playerModel;

        [SerializeField] private GameObject playerViewGM;
        private IPlayerView playerView;

        [SerializeField] private JoystickManager joystickManager;

        private Vector3 movement;
        private Vector3 velocity;
        private float moveSpeed = 4;
        private float gravity = 0.5f;

        [SerializeField] private CharacterController characterController;

        [SerializeField] private Menu.CutWheatButton cutWheatButton;

        [SerializeField] private GameObject playerKnifeGM;
        [SerializeField] private IPlayerWeapon currentWeapon;

        private void Awake()
        {
            InitializeMVC();

            cutWheatButton.OnPointerDownEvent += () =>
            {
                playerModel.SetCurrentBehavior(new PlayerBehaviorCutting());
                currentWeapon.SetCanAttack(true);
            };
            cutWheatButton.OnPointerUpEvent += () =>
            {
                playerModel.SetCurrentBehavior(playerModel.GetLastBehavior());
                currentWeapon.SetCanAttack(false);
            };

            currentWeapon = playerKnifeGM.GetComponent<IPlayerWeapon>();
        }

        private void InitializeMVC()
        {
            playerModel = new DefaultPlayerModel();
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
            float horizontal = joystickManager.GetHorizontal();
            float vertical = joystickManager.GetVertical();

            if (vertical == 0)
            {
                if (horizontal != 0)
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
                playerModel.SetCurrentBehavior(new PlayerBehaviorRunning());
            }
        }

        private void FixedUpdate()
        {
            float horizontal = joystickManager.GetHorizontal();
            float vertical = joystickManager.GetVertical();

            if (characterController.isGrounded)
            {
                velocity.y = 0;
            }
            else
            {
                velocity.y -= gravity * Time.deltaTime;
            }

            movement = transform.forward * vertical;
            transform.Rotate(100 * horizontal * Time.deltaTime * Vector3.up);

            characterController.Move(moveSpeed * Time.deltaTime * movement);
            characterController.Move(velocity);
        }

        public IPlayerWeapon GetCurrentWeapon()
        {
            return currentWeapon;
        }
    }
}