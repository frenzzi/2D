using UnityEngine;

[RequireComponent(typeof(VolumeChanger))]
public class AlarmArea : MonoBehaviour
{
    [SerializeField] private VolumeChanger _volumeChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Theft>())
        {
            _volumeChanger.ChangeToMax();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Theft>())
        {
            _volumeChanger.ChangeToMin();
        }
    }
}
