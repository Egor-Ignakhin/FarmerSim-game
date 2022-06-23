
using System;

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

        private void Awake()
        {
            InitializeMVC();
        }

        private void InitializeMVC()
        {
            playerModel = new DefaultPlayerModel();
            playerView = playerViewGM.GetComponent<IPlayerView>();

            playerView.Initialize(playerModel);
        }

        private void Update()
        {
            float horizontal = joystickManager.GetHorizontal();
            float vertical = joystickManager.GetVertical();

            if (vertical == 0)
            {
                if(horizontal != 0)
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
    }
}