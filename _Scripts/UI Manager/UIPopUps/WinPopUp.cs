
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopUp : UIRewardPopUp, IInformativePopUp
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _songName;
    [SerializeField] private TextMeshProUGUI _difficulty;
    
 
    protected override void addListeners()
    {
        base.addListeners();
        
        _closeButton.onClick.AddListener(() =>
        {
            UIManagers.Instance.OnLoadingButton(() =>
            {
                UIManagers.Instance.OnMenuButton();
            });
        });
        
        
    }

    public void SetText(params string[] data)
    {
        _levelText.text = data[0];
        _heartsRewardText.text = data[1];
        _diamondsRewardText.text = data[2];
        _expText.text = data[3];
        _filler.value = float.Parse(data[4]);
        _score.text = $"Score: {data[5]}";
        _songName.text = data[6];
        _difficulty.text = data[7];
    }
}
