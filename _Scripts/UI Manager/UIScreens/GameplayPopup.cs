using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Spine.Unity;
using System.Runtime.InteropServices;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameplayPopup : UIScreen, IPauseSettings
{
    [Header("Manager")]
    public GameManager ManagerController;
    public GameStarter GameStarter;

    [Header("UI Manager")]
    public Button PauseBtn;
    public GameObject StartGuide;

    [Header("Manager")]
    public GameObject BlockUI;

    [Header("Controller UI")]
    [FormerlySerializedAs("CurrentHeat")] public TextMeshProUGUI CurrentHeartsText;

    [Header("Animation")]
    public SkeletonGraphic Character;
    public SkeletonAnimation CharLeft;
    public SkeletonAnimation CharRight;

    [Header("floating Controller")]
    internal float CurrentTime = 0.1f;
    internal float CurrentTimeNext = 0.1f;
    internal float TimeChanging = 3f;

    [Header("Particles")]
    public GameObject EffectHeart;
    public GameObject PerfabeHeart;


    [Header("UI Controller")]
    [SerializeField] private TextMeshProUGUI Coins;
    [SerializeField] private TextMeshProUGUI Heath;
   
    [Header("Boolean Manager")]
    internal bool IsPause = false;
    internal bool SetAuto = false;
  

  

    


    public Action OnGameStarted;

    private void Awake()
    {
        PauseBtn.onClick.AddListener(() =>
        {
            UIManagers.Instance.OnPauseButton(this,
                MusicManager.Instance.GetCurrentSongName(),
                $"Score: {Consumables.ScoreManager.Score.ToString()}",
                GameManager.Instance.GameData.Difficulty.ToString()
                
            );
        });
    }


    private void actualizeTexts()
    {
        CurrentHeartsText.text = "" + Consumables.HeartsManager.Hearts;
    }
    void Update()
    {

            actualizeTexts();



    }
    void OnEnable()
    {
        CharLeft.AnimationState.SetAnimation(0, "idle_main", true);
        CharRight.AnimationState.SetAnimation(0, "idle_main", true);
    
        PauseBtn.gameObject.SetActive(false);
        StartGuide.SetActive(true);
        BlockUI.SetActive(true);
    }
    
    public void ClickStart()
    {
        #if UNITY_EDITOR == true
        if (Input.GetMouseButtonDown(0))
        {
            PauseBtn.gameObject.SetActive(true);
            StartGuide.SetActive(false);
            
            
            
            GameStarter.StartGame(() =>
            {
                BlockUI.SetActive(false);
                OnGameStarted?.Invoke();
            });
        }
        #endif
        
        
        if(Input.touchCount == 2)
        {
            PauseBtn.gameObject.SetActive(true);
            StartGuide.SetActive(false);
            
           
            GameStarter.StartGame(() =>
            {
                BlockUI.SetActive(false);
                OnGameStarted?.Invoke();
            });
        }
    }

   
    public void ResetUI()
    {
       // CurrentHeart = PlayerPrefs.GetInt("HEART");
       
      
        PauseBtn.gameObject.SetActive(false);
        StartGuide.SetActive(true);
        BlockUI.SetActive(true);
        CharLeft.AnimationState.SetAnimation(0, "idle_main", true);
        CharRight.AnimationState.SetAnimation(0, "idle_main", true);
       
       
    }

    public void StopGame()
    {
        MusicManager.Instance.StopSong();
        GameManager.Instance.StopGame();
        GameManager.Instance.ResetGameToStart();
    }
}
