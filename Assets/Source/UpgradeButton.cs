using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private UpgradeItems _item;

    public int Cost;
    [SerializeField] private int _costAddition;

    private void Awake()
    {
        Cost = DataStorage.Instance.Load(_item);
    }

    private void Start()
    {
       UpdateText();
    }

    private void OnEnable()
    {
        EventBus.Instance.DataLoaded += UpdateText;
        EventBus.Instance.SavingGame += SaveCost;
    }

    private void OnDisable()
    {
        EventBus.Instance.DataLoaded -= UpdateText;
        EventBus.Instance.SavingGame -= SaveCost;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (DataStorage.Instance.Score < Cost)
        {
            return;
        }

        DataStorage.Instance.Score -= Cost;
        Cost += _costAddition;

        switch (_item)
        {
            case UpgradeItems.PerClick:
                DataStorage.Instance.ScorePerClick++;
                break;
            
            case UpgradeItems.PerSecond:
                DataStorage.Instance.ScorePerSecond++;
                break;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        switch (_item)
        {
            case UpgradeItems.PerClick:
                _buttonText.text = $"+{DataStorage.Instance.ScorePerClick + 1} за клик стоит {Cost}$";
                break;
            
            case UpgradeItems.PerSecond:
                _buttonText.text = $"+{DataStorage.Instance.ScorePerSecond + 1} в секунду стоит {Cost}$";
                break;
        }
    }

    private void SaveCost()
    {
        DataStorage.Instance.Save(_item, Cost);
    }
}