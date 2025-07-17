using UnityEngine;

[RequireComponent(typeof(VolumeChanger))]
public class AlarmArea : MonoBehaviour
{
    [SerializeField] private VolumeChanger _volumeChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Theft>(out _))
        {
            _volumeChanger.ChangeToMax();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Theft>(out _))
        {
            _volumeChanger.ChangeToMin();
        }
    }
}
