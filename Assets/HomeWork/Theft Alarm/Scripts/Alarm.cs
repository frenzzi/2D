using UnityEngine;

[RequireComponent(typeof(VolumeChanger))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private VolumeChanger _volumeChanger;

    private bool _isActivate = false;

    private Vector3 _enterPosition;
    private Vector3 _exitPosition;
    private Vector3 _alarmPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enterPosition = collision.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _exitPosition = collision.transform.position;

        if (collision.gameObject.GetComponent<Theft>())
        {
            Activate();
        }
    }

    private void Activate()
    {
        if (IsChangeEntrance() == false)
            return;

        _isActivate = !_isActivate;

        if (_isActivate)
        {
            _volumeChanger.ChangeToMax();
        }
        else
        {
            _volumeChanger.ChangeToMin();
        }
    }

    private bool IsChangeEntrance()
    {
        bool isChangeEntrance = false;

        Vector3 normalizeEnterPosition = _enterPosition - transform.position;
        Vector3 normalizeExitPosition = _exitPosition - transform.position;

        if ((normalizeEnterPosition.x * normalizeExitPosition.x < 0) || (normalizeEnterPosition.y * normalizeExitPosition.y < 0))
            isChangeEntrance = true;

        return isChangeEntrance;
    }
}
