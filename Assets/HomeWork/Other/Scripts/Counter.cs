using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField, Min(0)] private float _counterStep = 1f;
    [SerializeField, Min(0)] private float _counterStepTime = 0.5f;

    private float _value = 0f;
    private bool _isActive = false;

    public float Value => _value;

    public event Action<float> Changed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                StartCoroutine(AddValue());
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator AddValue()
    {
        while (_isActive)
        {
            _value += _counterStep;
            Changed?.Invoke(_value);
            yield return new WaitForSeconds(_counterStepTime);
        }
    }
}
