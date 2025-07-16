using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private string _platformTag;

        private int _countOfCheckerOnPlatform = 0;

        private bool IsNoneCheckerOnPlatform => _countOfCheckerOnPlatform == 0;

        public event Action<bool> OnEdge;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == _platformTag)
            {
                _countOfCheckerOnPlatform--;
                OnEdge?.Invoke(IsNoneCheckerOnPlatform);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == _platformTag)
            {
                _countOfCheckerOnPlatform++;
            }
        }
    }
}
