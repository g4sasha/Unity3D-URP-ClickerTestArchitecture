using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    [SerializeField] bool _resetProgress;

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
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("ScorePerClick", ScorePerClick);
        PlayerPrefs.SetInt("ScorePerSecond", ScorePerSecond);
        Debug.Log("Data saved");
    }

    public void Load()
    {
        Score = PlayerPrefs.GetInt("Score");
        ScorePerClick = PlayerPrefs.GetInt("ScorePerClick");
        ScorePerSecond = PlayerPrefs.GetInt("ScorePerSecond");
        Debug.Log("Data loaded");
    }
}