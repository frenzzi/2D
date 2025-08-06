using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class GemCounter : MonoBehaviour
    {
        [SerializeField] private GemSpawner _spawner;
        [SerializeField] private int _score;
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _score = 0;
            _text.text = $"Гемы: {_score}";
        }

        private void OnEnable()
        {
            _spawner.GemCollected += UpScore;
        }

        private void OnDisable()
        {
            _spawner.GemCollected -= UpScore;
        }

        private void UpScore()
        {
            _score++;
            _text.text = $"Гемы: {_score}";
        }
    }
}