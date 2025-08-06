using UnityEngine;
using UnityEngine.Events;

namespace Platformer
{
    public class Enemy : MonoBehaviour
    {
        public event UnityAction<bool> PatrolBehaviourChanged;

        private void Start()
        {
            PatrolBehaviourChanged?.Invoke(true);
        }
    }
}