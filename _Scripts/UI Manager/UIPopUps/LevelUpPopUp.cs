
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopUp : UIRewardPopUp, IInformativePopUp
{
    

    protected override void addListeners()
    {
        base.addListeners();
        _closeButton.onClick.AddListener(UIManagers.Instance.PopState);
    }

    public void SetText(params string[] data)
    {
        _levelText.text = data[0];
        _heartsRewardText.text = data[1];
        _diamondsRewardText.text = data[2];
        _expText.text = data[3];
        _filler.value = float.Parse(data[4]);
    }

    public ref Button GetCloseButton() => ref _closeButton;
    
}
