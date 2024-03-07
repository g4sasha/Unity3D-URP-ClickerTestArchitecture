using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, Range(0, 500)] private int AutosaveSecondsInterval = 5;

    private void Awake()
    {
        GameCycle();
        AutoSaveCycle();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DataStorage.Instance.Score += DataStorage.Instance.ScorePerClick;
    }

    private async void GameCycle()
    {
        while (Application.isPlaying)
        {
            await Task.Delay(1000);
            DataStorage.Instance.Score += DataStorage.Instance.ScorePerSecond;
        }
    }

    private async void AutoSaveCycle()
    {
        while (Application.isPlaying)
        {
            DataStorage.Instance.Save();
            await Task.Delay(AutosaveSecondsInterval * 1000);
        }
    }
}