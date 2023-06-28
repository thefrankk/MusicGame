
using TMPro;
using UnityEngine;

public class NotEnoughLevelPopUp : UIPopUp, IInformativePopUp
{
    
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _levelNeeded;
    protected override void addListeners()
    {
        base.addListeners();
    }
    
    public void SetText(params string[] data)
    {
        _currentLevel.text = $"Your current level is <color=red>{data[0]}</color>";
        _levelNeeded.text = $"Level required to unlock: <color=yellow>{data[1]}</color>";
    }
}
