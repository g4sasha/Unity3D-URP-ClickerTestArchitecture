using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private UpgradeItems _item;

    public int Cost;
    [SerializeField] private int _costAddition;

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
                _buttonText.text = $"+{DataStorage.Instance.ScorePerClick + 1} за клик стоит {Cost}$";
                break;
            
            case UpgradeItems.PerSecond:
                DataStorage.Instance.ScorePerSecond++;
                _buttonText.text = $"+{DataStorage.Instance.ScorePerSecond + 1} в секунду стоит {Cost}$";
                break;
        }
    }
}

enum UpgradeItems
{
    PerClick,
    PerSecond
}