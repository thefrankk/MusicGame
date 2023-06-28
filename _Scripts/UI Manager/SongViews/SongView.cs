using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class SongView : MonoBehaviour
{



    [SerializeField] protected SongData _songData;

    [Header("UI Shower")]
    [SerializeField] protected TextMeshProUGUI _songName;

    [SerializeField] protected TextMeshProUGUI _levelRequired;
    [SerializeField] protected TextMeshProUGUI _unlockPrice;

    [Header("Controller Locked")]
    [SerializeField] private Button _unlockSongByDiamonds;
    [SerializeField] private Button _unlockSongByAD;
    
    [Header("Containers")]
    [SerializeField] protected GameObject _lockedSong;
    [SerializeField] protected GameObject _unlockedSong;

    [Header("Buttons")]
    [SerializeField] private Button _buttonLocked;
    [SerializeField] private Button _buttonUnlocked;

    [SerializeField] protected Image _backgroundImage;

    
    //If you want to record for original music you need to set this in "ORIGINAL"
    //If you want to record for user music you need to set this in "BYUSER"
    [Header("Song type -- For recording -- ORIGINAL or RECORDED BY USER")]
    [SerializeField] protected string _songType;
    
    [Header("Difficult controller")]
    public TextMeshProUGUI Type;
    public Button Left;
    public Button Right;

    internal int CurrentDifficultyMode = 0;
    
    //Properties
    public SongData SongData => _songData;


    public enum SongDiff
    {
        Easy,
        Normal,
        Hard,
    }

    protected float[] _difficultiesDelay = { 1.8f, 1.2f, 0.5f };
    protected SongDiff _currentSongDiff;
    

    private void Awake()
    {
        _unlockSongByDiamonds.onClick.AddListener(buySong);
        _unlockSongByAD.onClick.AddListener(buyAD);
    }

   


    protected virtual void Start()
    {
           
        Left.onClick.AddListener(ClickLeft);
        Right.onClick.AddListener(ClickRight);
       
        _songName.text = _songData.SongName;
        
        _buttonUnlocked.onClick.AddListener(PlayButton);

       
       
        CheckBackData();
        setDiff();
    }

    public abstract void CheckBackData();

    protected async virtual void PlayButton()
    {
        MusicManager.Instance.SetClip(_songData.SongClip);
        MusicManager.Instance.StopSong();
        GameManager.Instance.SetCurrentSongType(_songType);
    }
   
    void ClickLeft()
    {
        if(CurrentDifficultyMode > 0)
        {
            CurrentDifficultyMode -= 1;
        }
        else
        {
            CurrentDifficultyMode = 0;
        }

        setDiff();
    }
    void ClickRight()
    {
        if(CurrentDifficultyMode < 2)
        {
            CurrentDifficultyMode += 1;
        }
        else
        {
            CurrentDifficultyMode = 0;
        }

        setDiff();
    }

    protected abstract void setDiff();

    protected virtual void buySong()
    {
        CheckBackData();
    }

    private void buyAD()
    {
        throw new NotImplementedException();
    }







}

