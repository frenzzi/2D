using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 3.4f;
        [SerializeField] private float _jumpHeight = 6.5f;
        [SerializeField] private float _gravityScale = 1.5f;

        private bool _isFacingRight = true;
        private float _moveDirection = 0;
        private Rigidbody2D _rigidbody2d;
        private BoxCollider2D _mainCollider;
        private List<Collider2D> _groundColliders = new List<Collider2D>();

        private bool IsGrounded => _groundColliders.Count > 0;

        private void Awake()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _mainCollider = GetComponent<BoxCollider2D>();
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

        private void OnCollisionStay2D(Collision2D contactCollision)
        {
            if (_groundColliders.Contains(contactCollision.collider) == false)
                foreach (var contactPoint in contactCollision.contacts)
                    if (contactPoint.point.y < _mainCollider.bounds.min.y)
                    {
                        _groundColliders.Add(contactCollision.collider);
                        break;
                    }
        }

        private void OnCollisionExit2D(Collision2D contactCollision)
        {
            if (_groundColliders.Contains(contactCollision.collider))
                _groundColliders.Remove(contactCollision.collider);
        }

        private void ControleDirection()
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D) == false)
                    _moveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _moveDirection = 1;
            }
            else
            {
                if (IsGrounded || _rigidbody2d.velocity.magnitude == 0)
                {
                    _moveDirection = 0;
                }
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
            _rigidbody2d.velocity = new Vector2((_moveDirection) * _maxSpeed, _rigidbody2d.velocity.y);
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

