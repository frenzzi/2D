using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumeChangeSpeed = 1.0f;

    private float _minVolume = 0;
    private float _maxVolume = 1.0f;

    private IEnumerator _activeCoroutine;


    private void Awake()
    {
        _sound.volume = 0;
    }

    public void ChangeToMax()
    {
        ChangeVolume(_maxVolume);
    }

    public void ChangeToMin()
    {
        ChangeVolume(_minVolume);
    }

    private void ChangeVolume(float targetVolume)
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = ChangeTo(targetVolume);
        StartCoroutine(_activeCoroutine);
    }

    private IEnumerator ChangeTo(float newVolume)
    {
        while (Mathf.Approximately(_sound.volume, newVolume) == false)
        {
            _sound.volume = Mathf.MoveTowards(
                _sound.volume, 
                newVolume, 
                _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
