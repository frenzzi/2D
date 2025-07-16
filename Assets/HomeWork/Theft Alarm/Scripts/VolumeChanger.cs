using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumeChangeSpeed = 1.0f;

    private float _minVolume = 0;
    private float _maxVolume = 1.0f;

    private IEnumerator _maxVolumeCoroutine;
    private IEnumerator _minVolumeCoroutine;

    private void Awake()
    {
        _sound.volume = 0;
        _maxVolumeCoroutine = ChangeTo(_maxVolume);
        _minVolumeCoroutine = ChangeTo(_minVolume);
    }

    public void ChangeToMax()
    {
        StopCoroutine(_minVolumeCoroutine);
        StartCoroutine(_maxVolumeCoroutine);
    }

    public void ChangeToMin()
    {
        StopCoroutine(_maxVolumeCoroutine);
        StartCoroutine(_minVolumeCoroutine);
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
