using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        _counterText.text = _counter.Value.ToString();
    }

    private void OnEnable()
    {
        _counter.Changed += ChangeText;
    }

    private void OnDisable()
    {
        _counter.Changed -= ChangeText;
    }

    private void ChangeText(float newValue)
    {
        _counterText.text = newValue.ToString();
    }
}
