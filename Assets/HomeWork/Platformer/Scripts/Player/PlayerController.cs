using UnityEngine;
using System;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 3.4f;
        [SerializeField] private float _jumpHeight = 6.5f;
        [SerializeField] private float _gravityScale = 1.5f;

        public bool IsGrounded => Math.Abs(_rigidbody2d.velocity.y) < 0.01f;
        public bool IsRising => _rigidbody2d.velocity.y > 0;

        public event Action MovementBegun;
        public event Action MovementStopped;

        private int _moveDirection = 0;
        private Rigidbody2D _rigidbody2d;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _rigidbody2d.freezeRotation = true;
            _rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rigidbody2d.gravityScale = _gravityScale;
        }

        private void Update()
        {
            ControleDirection();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void ControleDirection()
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D) == false)
                {
                    _moveDirection = -1;
                    MovementBegun?.Invoke();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _moveDirection = 1;
                MovementBegun?.Invoke();
            }
            else
            {
                _moveDirection = 0;
                MovementStopped?.Invoke();
            }

            if (_moveDirection != 0)
            {
                Rotate();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        }

        private void Move()
        {
            _rigidbody2d.velocity = new Vector2(_moveDirection * _maxSpeed, _rigidbody2d.velocity.y);
        }

        private void Jump()
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpHeight);
        }

        private void Rotate()
        {
            transform.localScale = new Vector3(_moveDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

