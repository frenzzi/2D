using System;
using UnityEngine;

namespace Plarformer
{

    [RequireComponent(typeof(BoxCollider2D))]
    public class Coin : MonoBehaviour
    {
        public event Action Destroyed;
        private string _playerTag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == _playerTag)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Destroyed.Invoke();
        }
    }
}