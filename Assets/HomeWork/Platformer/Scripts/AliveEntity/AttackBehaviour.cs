using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    abstract public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField, Min(0)] protected int _damage = 1;
        
        protected string _attackTag;
        protected List<Collider2D> _targetColliders = new List<Collider2D>();

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _attackTag)
            {
                if (_targetColliders.Contains(collision.collider) == false)
                    _targetColliders.Add(collision.collider);
            }
        }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _attackTag)
            {
                if (_targetColliders.Contains(collision.collider))
                    _targetColliders.Remove(collision.collider);
            }
        }

        protected void Attack()
        {
            foreach (var _target in _targetColliders)
            {
                _target.GetComponent<Enemy>().TakeDamage(_damage);
            }
        }
    }
}

