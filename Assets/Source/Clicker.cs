using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerDownHandler
{
    private void Awake()
    {
        GameCycle();
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
}