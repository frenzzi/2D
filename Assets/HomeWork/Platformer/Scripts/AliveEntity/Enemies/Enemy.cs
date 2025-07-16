using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _health = 10;

        private bool _isFacingRight = true;
        private float _moveDirection = 0;
        private Rigidbody2D _rigidbody2d;
        private BoxCollider2D _mainCollider;
        private List<Collider2D> _groundColliders = new List<Collider2D>();

        private void Awake()
        {
            gameObject.tag = "Enemy";
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _mainCollider = GetComponent<BoxCollider2D>();
            _rigidbody2d.freezeRotation = true;
            _rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        virtual public void TakeDamage(int damage)
        {
            if (damage < 0)
                return;
            
            if (damage >= _health) 
                _health = 0;
            else
                _health -= damage;

            if(_health == 0)
                Destroy(gameObject);
        }
    }
}
