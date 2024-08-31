using System.Collections;
using TMPro;
using UnityEngine;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private float _smoothDecreaseDuration = 0.5f;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Color _damageHealthColor;
    [SerializeField] private AnimationCurve _colorBehaviour;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private AnimationClip _hurtPlayerAnimation;

    private Color _originalPlayerColor;

    void Start()
    {
        _originalPlayerColor = _healthText.color;
        _healthText.text = _health.MaxHealth.ToString();
    }

    private void OnEnable()
    {
        _health.Changed += TakeDamage;
    }

    private void OnDisable()
    {
        _health.Changed -= TakeDamage;
    }

    private void TakeDamage(float currentHealth)
    {
        _playerAnimator.Play(_hurtPlayerAnimation.name);
        StartCoroutine(DecreaseHealthSmothly(currentHealth));
    }

    private IEnumerator DecreaseHealthSmothly(float target)
    {
        float elapsedTime = 0f;
        float previousValue = float.Parse(_healthText.text);

        while (elapsedTime < _smoothDecreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smoothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValue, target, normalizedPosition);
            _healthText.text = intermediateValue.ToString("F0");

            _healthText.color = Color.Lerp(_originalPlayerColor, _damageHealthColor, _colorBehaviour.Evaluate(normalizedPosition));

            yield return null;
        }
    }
}
