using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumeChangeSpeed = 1.0f;

    private float _minVolume = 0;
    private float _maxVolume = 1.0f;

    private void Awake()
    {
        _sound.volume = 0;
    }

    public void ChangeToMax()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeTo(_maxVolume));
    }

    public void ChangeToMin()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeTo(_minVolume));
    }

    private IEnumerator ChangeTo(float newVolume)
    {
        while (_sound.volume != newVolume)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, newVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
