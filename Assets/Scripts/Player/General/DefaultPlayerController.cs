
using FarmerSim.Menu;
using FarmerSim.Player.Inventory;

using UnityEngine;

namespace FarmerSim.Player
{
    public class DefaultPlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerModel playerModel;

        [SerializeField] private GameObject animatorPlayerViewGM;
        private IPlayerView animatorPlayerView;

        [SerializeField] private FloatingJoystick floatingJoystick;

        [SerializeField] private CharacterController characterController;

        [SerializeField] private GameObject cutWheatButtonGM;
        private IGameButton cutWheatButton;

        [SerializeField] private GameObject playerKnifeGM;

        [SerializeField] private GameObject playerInventoryControllerGM;
        private IPlayerInventoryController playerInventoryController;

        private readonly PlayerInputHandler playerInputHandler = new PlayerInputHandler();

        private void Awake()
        {
            InitializeGeneral();
            InitializeSubs();
        }

        private void InitializeGeneral()
        {
            playerInventoryController = playerInventoryControllerGM.GetComponent<IPlayerInventoryController>();
            playerModel = new DefaultPlayerModel(playerInventoryController);
            animatorPlayerView = animatorPlayerViewGM.GetComponent<IPlayerView>();

            animatorPlayerView.Initialize(playerModel);
        }

        private void InitializeSubs()
        {
            cutWheatButton = cutWheatButtonGM.GetComponent<IGameButton>();
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

        private void Update()
        {
            if (playerModel.GetCurrentBehavior() is PlayerBehaviorCutting)
                return;

            playerModel.SetCurrentBehavior(playerInputHandler
                .ComputeCurrentPlayerBehavior(floatingJoystick.Direction));

            transform.rotation = playerInputHandler.
                ComputeCurrentRotation(new Vector3(floatingJoystick.Direction.x,
                0, floatingJoystick.Direction.y),
                transform.eulerAngles);
        }

        public bool HaveItems<T>()
        {
            return playerModel.HaveItems<T>();
        }
    }
}