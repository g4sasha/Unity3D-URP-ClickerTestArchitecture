using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        EventBus.Instance.ScoreChanged += DisplayScore;
    }

    private void OnDisable()
    {
        EventBus.Instance.ScoreChanged -= DisplayScore;
    }

    private void DisplayScore(int value)
    {
        _score.text = value.ToString() + '$';
    }
}