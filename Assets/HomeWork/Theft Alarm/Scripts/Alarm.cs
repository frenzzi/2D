using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumeChangeSpeed = 1.0f;

    private bool isActivate = false;

    private void Start()
    {
        _sound.volume = 0;
        StartCoroutine(Activating());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Theft>())
        {
            isActivate = !isActivate;
        }
    }

    private IEnumerator Activating()
    {
        while (isActiveAndEnabled)
        {
            if (isActivate)
            {
                _sound.volume = Mathf.MoveTowards(_sound.volume, 1, _volumeChangeSpeed * Time.deltaTime);
            }
            else
            {
                _sound.volume = Mathf.MoveTowards(_sound.volume, 0, _volumeChangeSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
