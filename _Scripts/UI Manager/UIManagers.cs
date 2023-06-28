using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    
    public static UIManagers Instance { get; private set; }
    
    [Header("UI Screens")]
    [SerializeField] private UIScreen _menuUI;
    [SerializeField] private UIScreen _gameplayUI;
    [SerializeField] private UIScreen _loadingUI;
    [SerializeField] private UIScreen _recordingUI;
    
    [Header("Pop ups")]
    [SerializeField] private UIScreen _settingsUI;
    [SerializeField] private UIScreen _gameOverUI;
    [SerializeField] private UIScreen _pauseUI;
    [SerializeField] private UIScreen _winUI;
    [SerializeField] private GameObject _continueUI;
    [SerializeField] private UIScreen _spinWheelUI;
    [SerializeField] private UIScreen _dailyRewardUI;
    [SerializeField] private UIScreen _notEnoughDiamondsUI;
    [SerializeField] private UIScreen _notEnoughLevelUI;
    [SerializeField] private GameObject _moreCoinsUI;
    [SerializeField] private UIScreen _moreHeartsUI;
    [SerializeField] private GameObject _rateAppUI;
    [SerializeField] private UIScreen _songRecordedUI;
    [SerializeField] private UIScreen _levelUpPopUp;
    [SerializeField] private UIScreen _shopPopUp;
    // Properties Screens
    public UIScreen MenuUI { get => _menuUI; }
    public UIScreen GameplayUI {get => _gameplayUI; }

    public UIScreen RecordingUI => _recordingUI;
    //Properties Pop ups
    public UIScreen SettingsUI {get => _settingsUI; }
    public UIScreen GameOverUI {get => _gameOverUI; }
    public UIScreen PauseUI {get => _pauseUI; }
    public UIScreen WinUI {get => _winUI; }
    public GameObject ContinueUI {get => _continueUI; }
    public UIScreen LoadingUI {get => _loadingUI; }
    public UIScreen SpinWheelUI {get => _spinWheelUI; }
    public UIScreen DailyRewardUI {get => _dailyRewardUI; }
    public UIScreen NotEnoughDiamonds {get => _notEnoughDiamondsUI; }
    public GameObject MoreCoinsUI {get => _moreCoinsUI; }
    public UIScreen MoreHeartsUI {get => _moreHeartsUI; }
    public GameObject RateAppUI {get => _rateAppUI; }
    public UIScreen SongRecordedUI => _songRecordedUI;
    public UIScreen NotEnoughLevel => _notEnoughLevelUI;
    public UIScreen LevelUp => _levelUpPopUp;
    public UIScreen ShopUI => _shopPopUp;
    
    

    private Stack<UIState> _uiStateStack = new Stack<UIState>();

    private UIState _currentState;

    private Dictionary<Type, Delegate> _eventDictionary = new Dictionary<Type, Delegate>();

    private delegate void SwitchingState(UIState state);
    private delegate void PushingState(UIState state);


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        SwitchingState switchState = SwitchState;
        PushingState pushState = PushState;

        _eventDictionary.Add(typeof(UIScreen), switchState);
        _eventDictionary.Add(typeof(UIPopUp), pushState);
    }
    private void Start()
    {
       createState(MenuUI.GetCurrentType()).DynamicInvoke(new MenuState(this));
    }

    public void SwitchState(UIState state)
    {
        _currentState?.ExitState();

        _currentState = state;

        _currentState?.EnterState();
    }
    
    public void PushState(UIState state)
    {

        _uiStateStack.Push(state);

        _uiStateStack.Peek()?.EnterState();
    }

    public void PopState()
    {
        if (_uiStateStack.Count > 0)
        {
            UIState lastState = _uiStateStack.Pop();
            lastState.ExitState();
        }
    }
    
    
    public T GetCurrentState<T>() where T : UIState
    {
        return (T)_currentState;
    }
    // Add methods here to be hooked up to your UI buttons
    public void OnPlayButton()
    {
        createState(_gameplayUI.GetCurrentType()).DynamicInvoke(new GameplayState(this));

    }

    public void OnSongRecordedButton()
    {
        createState(_songRecordedUI.GetCurrentType()).DynamicInvoke(new SongRecordedState(this));
    }
    public void OnSettingsButton()
    {
        createState(_settingsUI.GetCurrentType()).DynamicInvoke(new SettingsState(this));

    }

    public void OnMenuButton()
    {
        createState(_menuUI.GetCurrentType()).DynamicInvoke(new MenuState(this));
    }
    
    public void OnLuckyWheelButton()
    {
        createState(_spinWheelUI.GetCurrentType()).DynamicInvoke(new SpinWheelState(this));
    }
    
    public void OnDailyRewardButton()
    {
        createState(_dailyRewardUI.GetCurrentType()).DynamicInvoke(new DailyRewardState(this));
    }
    public void OnMoreHeartsButton()
    {
        createState(_moreHeartsUI.GetCurrentType()).DynamicInvoke(new MoreHeartsState(this));
    }

    public void OnRateAppButton()
    {
     //   createState(_rateAppUI.GetCurrentType()).DynamicInvoke(new RateAppState(this));
    }
    public void OnNotEnoughLevelButton(params string[] message)
    {
        createState(_notEnoughLevelUI.GetCurrentType()).DynamicInvoke(new NotEnoughLevelState(this));
        IInformativePopUp popUp = _notEnoughLevelUI.GetComponent<IInformativePopUp>();
        Debug.Log(popUp);
        popUp.SetText(message);
    }

    /// <summary>
    /// CURRENT DIAMONDS
    /// NEEDED DIAMONDS
    /// </summary>
    /// <param name="message"></param>
    public void OnNotEnoughDiamondsButton(params string[] message)
    {
        createState(_notEnoughDiamondsUI.GetCurrentType()).DynamicInvoke(new NotEnoughCoinsState(this));
        IInformativePopUp popUp = _notEnoughDiamondsUI.GetComponent<IInformativePopUp>();
        popUp.SetText(message);
    }
    
    public void OnLevelUpButton(out Button button, params string[] message)
    {
        createState(_levelUpPopUp.GetCurrentType()).DynamicInvoke(new LevelUpState(this));
        LevelUpPopUp popUp = _levelUpPopUp.GetComponent<LevelUpPopUp>();
        button = popUp.GetCloseButton();
        popUp.SetText(message);
    }
    public void OnRecordingButton()
    {
        createState(_recordingUI.GetCurrentType()).DynamicInvoke(new RecordingState(this));
    }
    public void OnPauseButton(IPauseSettings settings, params string[] message)
    {
        createState(_pauseUI.GetCurrentType()).DynamicInvoke(new PauseState(this));
        PausePopUp popUp = _pauseUI.GetComponent<PausePopUp>();
        popUp.SetText(message);
        popUp.SetPauseSettings(settings);
       
    }
    public void OnShopButton()
    {
        createState(_shopPopUp.GetCurrentType()).DynamicInvoke(new ShopState(this));
    }

    public void OnLoadingButton(Action callback)
    {
        SwitchState(new LoadingState(this));
        LoadingUIScreen loadingUI = LoadingUI.GetComponent<LoadingUIScreen>();
        loadingUI.StartLoadingScreen(callback);

       
          //  loadingUI.OnLoadingEndend += callback;
            //loadingUI.OnLoadingEndend += () => unsubscribe(ref loadingUI, ref callback);
    }

    // private void unsubscribe(ref LoadingUIScreen screen, ref Action action)
    // {
    //     screen.OnLoadingEndend -= action;
    // }
    
    public void OnGameOverButton(params string[] message)
    {
        createState(_gameOverUI.GetCurrentType()).DynamicInvoke(new GameOverState(this));
        IInformativePopUp popUp = _gameOverUI.GetComponent<IInformativePopUp>();
        popUp.SetText(message);
    }
   
 /// <summary>
 /// _levelText
 /// _heartsRewardText.
 /// _diamondsReward
 /// _expText
 /// _filler
 /// _score
 /// _songName
 /// _difficulty
 /// </summary>
 /// <param name="message"></param>
    public void OnWinButton(params string[] message)
    {
        createState(_winUI.GetCurrentType()).DynamicInvoke(new WinState(this));
        IInformativePopUp popUp = _winUI.GetComponent<IInformativePopUp>();
        popUp.SetText(message);
    }

  
    private Delegate createState(Type type)
    {
        return _eventDictionary[type];
    }

   /* public void OnGameOverButton()
    {
        SwitchState(new GameOverState(this));
    }
  
    
    public void OnNotEnoughCoinsButton()
    {
        SwitchState(new NotEnoughCoinsState(this));
    }

    public void OnMoreCoinsButton()
    {
        SwitchState(new MoreCoinsState(this));
    }

  

 
    */

  
}