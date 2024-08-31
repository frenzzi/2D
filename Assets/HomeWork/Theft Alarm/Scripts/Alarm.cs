using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumeChangeSpeed = 1.0f;

    private bool _isActivate = false;

    private bool IsMaxOrMinVolume => (_sound.volume == 0) || (_sound.volume == 1);

    private void Awake()
    {
        _sound.volume = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Theft>())
        {
            _isActivate = !_isActivate;
            StartCoroutine(Activating());
        }
    }

    private IEnumerator Activating()
    {
        while (isActiveAndEnabled)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, Convert.ToInt32(_isActivate), _volumeChangeSpeed * Time.deltaTime);

            if (IsMaxOrMinVolume)
            {
                break;
            }

            yield return null;
        }
    }

}
