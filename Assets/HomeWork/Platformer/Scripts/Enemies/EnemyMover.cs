using Platformer;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyMover : MonoBehaviour
    {
        public enum MoveAxis
        {
            OnlyX,
            OnlyY,
            XY
        }

        private const float DistanceThreshold = 0.1f;

        [SerializeField] private MoveAxis _moveAxis;
        [SerializeField, Min(0)] private float _speed;
        [SerializeField] private bool _isFacingRight = true;

        private Enemy _enemy;
        private Rigidbody2D _rigidbody2d;
        private Coroutine _moveCoroutine;

        public event UnityAction MoveEnded;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _rigidbody2d.freezeRotation = true;
            _rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            _enemy = GetComponent<Enemy>();
        }

        public IEnumerator MoveToTarget(Transform target)
        {
            if (target == null)
            {
                yield return null;
            }

            StopMovement();

            _moveCoroutine = StartCoroutine(MoveTowardsTarget(target));
            yield return _moveCoroutine;
        }

        public void StopMovement()
        {
            StopCurrentMovement();
            StopRigidbody();
        }

        private void StopCurrentMovement()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }

        private void StopRigidbody()
        {
            _rigidbody2d.velocity = Vector2.zero;
        }

        private IEnumerator MoveTowardsTarget(Transform target)
        {
            while (IsAtTargetPosition(target) == false)
            {
                Vector2 direction = GetMovementDirection(target);

                if (direction == Vector2.zero)
                {
                    break;
                }

                SetFacingDirection(direction);

                _rigidbody2d.velocity = direction * _speed;

                yield return null;
            }

            StopRigidbody();

            FixPositionAtTarget(target);

            MoveEnded?.Invoke();

            _moveCoroutine = null;
        }

        private bool IsAtTargetPosition(Transform target)
        {
            float distance = 0;

            if (_moveAxis == MoveAxis.XY)
                distance = Vector2.Distance(transform.position, target.position);
            else if (_moveAxis == MoveAxis.OnlyX)
                distance = Mathf.Abs(transform.position.x - target.position.x);
            else if (_moveAxis == MoveAxis.OnlyY)
                distance = Mathf.Abs(transform.position.y - target.position.y);

            return distance < DistanceThreshold;
        }

        private Vector2 GetMovementDirection(Transform target)
        {
            Vector2 delta = target.position - transform.position;

            switch (_moveAxis)
            {
                case MoveAxis.OnlyX:
                    return new Vector2(Mathf.Sign(delta.x), 0f);

                case MoveAxis.OnlyY:
                    return new Vector2(0f, Mathf.Sign(delta.y));

                case MoveAxis.XY:
                    return delta.normalized;

                default:
                    return Vector2.zero;
            }
        }

        private void SetFacingDirection(Vector2 direction)
        {
            if (_moveAxis == MoveAxis.OnlyY)
            {
                return;
            }

            if (direction.x != 0)
            {
                Vector3 scale = transform.localScale;

                if (_isFacingRight)
                    scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction.x);
                else
                    scale.x = -Mathf.Abs(scale.x) * Mathf.Sign(direction.x);

                transform.localScale = scale;
            }
        }

        private void FixPositionAtTarget(Transform target)
        {
            Vector3 position = transform.position;

            switch (_moveAxis)
            {
                case MoveAxis.OnlyX:
                    position.x = target.position.x;
                    break;

                case MoveAxis.OnlyY:
                    position.y = target.position.y;
                    break;

                case MoveAxis.XY:
                    position.x = target.position.x;
                    position.y = target.position.y;
                    break;
            }

            _rigidbody2d.MovePosition(position);
        }
    }
}