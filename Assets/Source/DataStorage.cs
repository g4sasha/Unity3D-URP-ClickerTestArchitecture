using System.Threading.Tasks;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    [SerializeField] bool _resetProgress;
    [SerializeField, Range(1, 600)] private int AutosaveSecondsInterval = 5;

    public int ScorePerClick = 1;
    public int ScorePerSecond = 0;

    private int _score = 0;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            EventBus.Instance.ScoreChanged?.Invoke(_score);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (PlayerPrefs.HasKey("Score") && !_resetProgress)
            {
                Load();
            }

            if (_resetProgress)
            {
                _resetProgress = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        AutoSaveCycle();
    }

    private async void AutoSaveCycle()
    {
        while (Application.isPlaying)
        {
            Save();
            await Task.Delay(AutosaveSecondsInterval * 1000);
        }
    }

    public void Save()
    {
        EventBus.Instance.SavingGame?.Invoke();
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("ScorePerClick", ScorePerClick);
        PlayerPrefs.SetInt("ScorePerSecond", ScorePerSecond);
        Debug.Log("Data saved");
    }

    public void Save(UpgradeItems item, int value)
    {
        PlayerPrefs.SetInt(item.ToString(), value);
    }

    public void Load()
    {
        Score = PlayerPrefs.GetInt("Score");
        ScorePerClick = PlayerPrefs.GetInt("ScorePerClick");
        ScorePerSecond = PlayerPrefs.GetInt("ScorePerSecond");
        EventBus.Instance.DataLoaded?.Invoke();
        Debug.Log("Data loaded");
    }

    public int Load(UpgradeItems item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }
}