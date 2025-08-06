using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Platformer
{
    public class GemSpawner : MonoBehaviour
    {
        [SerializeField] private Gem _prefab;
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private List<Vector2> _spawnPositions = new List<Vector2>();

        public event Action GemCollected;

        private int _poolCapacity;
        private int _poolMaxSize;
        private ObjectPool<Gem> _pool;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);
                _spawnPositions.Add(childTransform.position);
            }

            _poolCapacity = transform.childCount;
            _poolMaxSize = transform.childCount;
            
            _pool = new ObjectPool<Gem>
                (
                createFunc: () => Create(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
                );
        }

        private void Start()
        {
            StartCoroutine(Repeater());
        }

        private Gem Create()
        {
            return Instantiate(_prefab);
        }

        public void Release(Gem obj)
        {
            GemCollected?.Invoke();
            obj.Collected -= Release;

            _pool.Release(obj);
        }

        private void ActionOnGet(Gem obj)
        {
            obj.Collected += Release;

            SetPosition(obj);
            obj.gameObject.SetActive(true);
        }

        private void GetGem()
        {
            if (_pool.CountActive < _poolMaxSize)
            {
                Gem obj = _pool.Get();
            }
        }

        private void SetPosition(Gem obj)
        {
            obj.transform.position = _spawnPositions[UnityEngine.Random.Range(0, _poolCapacity)];
        }

        private IEnumerator Repeater()
        {
            while (isActiveAndEnabled)
            {
                GetGem();

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}

