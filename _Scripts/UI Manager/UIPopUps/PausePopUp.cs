using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausePopUp : UIPopUp, IInformativePopUp
{
    [Header("Home button")] 
    [SerializeField]
    private Button _homeButton;

    [Header("Texts")] 
    [SerializeField] private TextMeshProUGUI _songName;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _difficulty;

    private IPauseSettings _pauseSettings;

    [SerializeField] private Button _replayButton;
    protected override void addListeners()
    {
        base.addListeners();
        _homeButton.onClick.AddListener(UIManagers.Instance.PopState);
        _homeButton.onClick.AddListener(() =>
        {
            _pauseSettings.StopGame();
            
            UIManagers.Instance.OnLoadingButton(() =>
            {
                UIManagers.Instance.OnMenuButton();
            });
            
        });
        _replayButton.onClick.AddListener(() =>
        {
            UIManagers.Instance.PopState();
            _pauseSettings.StopGame();
            
            GameManager.Instance.ReplayGame();
        });
    }

    public void SetText(params string[] data)
    {
        _songName.text = data[0];
        _score.text = data[1];
        _difficulty.text = data[2];
    }
    
    public void SetPauseSettings(IPauseSettings settings)
    {
        _pauseSettings = settings;
    }
}