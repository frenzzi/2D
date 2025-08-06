using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Platformer
{
    [RequireComponent(typeof(EnemyMover))]
    public class PatrolBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;

        private EnemyMover _enemyMover;
        private Enemy _enemy;
        private bool _isStop;
        private Coroutine _patrolCoroutine;

        private void Awake()
        {
            InitializeComponent();

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                _isStop = true;
            }
            else
            {
                _isStop = false;
            }
        }

        private void InitializeComponent()
        {
            _enemy = GetComponent<Enemy>();
            _enemyMover = GetComponent<EnemyMover>();
        }

        private void OnEnable()
        {
            if (_isStop == false)
            {
                _enemy.PatrolBehaviourChanged += ChangeBehaviour;
            }
        }

        private void OnDisable()
        {
            if (_isStop == false)
            {
                _enemy.PatrolBehaviourChanged -= ChangeBehaviour;
            }

            StopPatrol();
        }

        private void ChangeBehaviour(bool isPatrol)
        {
            if (isPatrol)
            {
                if (_patrolCoroutine == null)
                {
                    _patrolCoroutine = StartCoroutine(MoveByTheWay());
                }
            }
            else
            {
                StopPatrol();
            }
        }

        private void StopPatrol()
        {
            if (_patrolCoroutine != null)
            {
                StopCoroutine(_patrolCoroutine);
                _patrolCoroutine = null;
            }
        }

        private IEnumerator MoveByTheWay()
        {
            while (isActiveAndEnabled)
            {
                for (int i = 0; i < _patrolPoints.Length; i++)
                {
                    yield return _enemyMover.MoveToTarget(_patrolPoints[i]);
                }

                for (int i = _patrolPoints.Length - 2; i > 0; i--)
                {
                    yield return _enemyMover.MoveToTarget(_patrolPoints[i]);
                }
            }
        }
    }
}
