using UnityEngine;

namespace Platformer
{
    public class EnemyAttackBehaviour : AttackBehaviour
    {
        [SerializeField] protected string _enemyAttackTag = "Player";

        private void Awake()
        {
            _attackTag = _enemyAttackTag;
        }
    }
}