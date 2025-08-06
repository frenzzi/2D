using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Gem : MonoBehaviour
    {
        public event Action<Gem> Collected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Player>(out _))
            {
                Collected?.Invoke(this);
            }

            //if (collision.gameObject.TryGetComponent<Gem>(out _))
            //{
            //    Destroy(this);
            //}
        }
    }
}