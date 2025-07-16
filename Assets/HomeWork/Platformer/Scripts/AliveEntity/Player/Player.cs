using Plarformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerAttackBehaviour))]
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            gameObject.tag = "Player";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}