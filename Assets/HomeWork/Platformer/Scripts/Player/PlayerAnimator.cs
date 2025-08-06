using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _controller;

        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _controller.MovementBegun += BegunRunningAnimation;
            _controller.MovementStopped += StopRunningAnimation;
        }

        private void OnDisable()
        {
            _controller.MovementBegun -= BegunRunningAnimation;
            _controller.MovementStopped -= StopRunningAnimation;
        }

        private void Update()
        {
            _animator.SetBool("IsGrounded", _controller.IsGrounded);

            if (_controller.IsGrounded == false)
            {
                _animator.SetBool("IsRising", _controller.IsRising);
            }
        }

        private void BegunRunningAnimation()
        {
            _animator.SetBool("IsMoving", true);
        }

        private void StopRunningAnimation()
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}

