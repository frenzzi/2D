using UnityEngine;

namespace Platformer
{
    public class PlayerAttackBehaviour : AttackBehaviour
    {
        [SerializeField] private string _playerAttackTag = "Enemy";
        private int _attackButton = 0;

        private void Awake()
        {
            _attackTag = _playerAttackTag;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(_attackButton))
            {
                Attack();
            }
        }
    }
}