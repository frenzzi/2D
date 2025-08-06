using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Platformer
{
    public class Enemy : MonoBehaviour
    {
        public event UnityAction<bool> PatrolBehaviourChanged;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
        }

        private void Start()
        {
            PatrolBehaviourChanged?.Invoke(true);
        }
    }
}