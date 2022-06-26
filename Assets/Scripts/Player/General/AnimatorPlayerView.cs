
using UnityEngine;

namespace FarmerSim.Player
{
    public class AnimatorPlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Animator playerAnimator;

        private IPlayerModel playerModel;

        public void Initialize(IPlayerModel playerModel)
        {
            this.playerModel = playerModel;
            this.playerModel.Changed += OnModelChanged;
        }

        private void OnModelChanged()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            IPlayerBehavior playerBehavior = playerModel.GetCurrentBehavior();

            bool behaviorIsRunning = playerBehavior is PlayerBehaviorRunning;
            bool behaviorIsWaking = playerBehavior is PlayerBehaviorWalking;
            bool behaviorIsCutting = playerBehavior is PlayerBehaviorCutting;

            playerAnimator.SetBool("IsRunning", behaviorIsRunning);
            if (behaviorIsRunning)
            {
                playerAnimator.SetFloat("RunningMultiplier",
                    (playerBehavior as PlayerBehaviorRunning).GetDirectionSign());
            }

            playerAnimator.SetBool("IsWalking", behaviorIsWaking);
            playerAnimator.SetBool("IsCutting", behaviorIsCutting);
        }

        private void OnDestroy()
        {
            playerModel.Changed -= this.OnModelChanged;
        }
    }
}