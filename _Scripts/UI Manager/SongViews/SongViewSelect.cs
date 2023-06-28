



using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SongViewSelect : SongView
{


    [Header("Buttons")]
    [SerializeField] private Button _playPreview;
    //This is for select another difficulty for the song. Here you can play with 4 sides coming foods.
    [SerializeField] private Button _nextSongLevel;
    [SerializeField] private Button _prevSongLevel;

    [Header("Containers")] 
    [SerializeField] private GameObject _newDiffContainer;
    [SerializeField] private GameObject _unlockedNewDiffContainer;
    [SerializeField] private GameObject _lockedNewDiffContainer;
    
    [SerializeField] protected TextMeshProUGUI _score;
    [SerializeField] private Transform _songPlayingPreview;
    [SerializeField] private Transform _playSongPreview;

  
    
    private static (Transform, Transform) _songsPreviews;
   

    private void OnEnable()
    {
        _songData.LoadSongData();
    }

    
    protected override void Start()
    {
        base.Start();
        _score.text = _songData.Scores[(int)_currentSongDiff].ToString();
        _backgroundImage.color = _songData.BackgroundColor;
        
        Type.fontSize = 50.95f;
        
        addListeners();
        
        
        _songsPreviews = (_songPlayingPreview, _playSongPreview);
    }
    protected async override void PlayButton()
    {
        base.PlayButton();
        
        StartCoroutine(_songData.LoadNoteData((noteData =>
        {
            Debug.Log((int)_currentSongDiff);
        
            GameData gameData = new GameData(noteData, _currentSongDiff, _difficultiesDelay[(int)_currentSongDiff]);
            GameManager.Instance.StartGame(ref gameData);
        }), (int)_currentSongDiff, _songType));
        
      

    }

    void clickLeft()
    {
        if (_newDiffContainer.activeSelf)
        {
           _newDiffContainer.SetActive(false);
        
        }
        else
        {
           _lockedNewDiffContainer.SetActive(!_songData.IsExtraDiffUnlocked);
           _unlockedNewDiffContainer.SetActive(_songData.IsExtraDiffUnlocked);
           _newDiffContainer.SetActive(true);
        }

    }
    void clickRight()
    {
        if (_newDiffContainer.activeSelf)
        {
            _newDiffContainer.SetActive(false);
           
        }
        else
        {
            _newDiffContainer.SetActive(true);
            _unlockedNewDiffContainer.SetActive(_songData.IsExtraDiffUnlocked);
            _lockedNewDiffContainer.SetActive(!_songData.IsExtraDiffUnlocked);
        }
    }
    
    private void playPreview()
    {
        _songsPreviews.Item1?.gameObject.SetActive(false);
        _songsPreviews.Item2?.gameObject.SetActive(true);
        
        _songsPreviews = (_songPlayingPreview, _playSongPreview);
        
        MusicManager.Instance.SetClip(_songData.SongClip);
        MusicManager.Instance.StartSong(0f, true);
    }

    private void addListeners()
    {
        _playPreview.onClick.AddListener(playPreview);
        _nextSongLevel.onClick.AddListener(clickRight);
        _prevSongLevel.onClick.AddListener(clickLeft);
    }
    public override void CheckBackData()
    {
        _unlockedSong.SetActive(_songData.IsUnlocked);
        _lockedSong.SetActive(!_songData.IsUnlocked);

        if (!_songData.IsUnlocked)
        {
            
            if(PlayerLevelManager.Instance.Level < _songData.LevelRequired)
            {
                _levelRequired.text = $"Level required: {_songData.LevelRequired.ToString()}";
                _levelRequired.color = Color.red;
            }
            else
            {
                
            }
            
            
            _unlockPrice.text = _songData.UnlockPrice.ToString();
        }
       
    }

    protected override void buySong()
    {
        if(PlayerLevelManager.Instance.Level < _songData.LevelRequired)
        {
            //show pop up cant buy
            UIManagers.Instance.OnNotEnoughLevelButton(PlayerLevelManager.Instance.Level.ToString(), _songData.LevelRequired.ToString());
            return;
        }
        
        Consumables.DiamondManager.TryToBuy(_songData.UnlockPrice, (canBuy) =>
        {
            if (!canBuy)
            {
                //show pop up cant buy
                UIManagers.Instance.OnNotEnoughDiamondsButton(Consumables.DiamondManager.Diamonds.ToString(), _songData.UnlockPrice.ToString());
                return;
            }
            
            _songData.UnlockSong();
            
        });
        
        base.buySong();
    }


    protected override void setDiff()
    {
        Color currentColor = Color.green;
        if(CurrentDifficultyMode == 0)
        {
            _currentSongDiff = SongDiff.Easy;
           
            Type.text = "EASY";
            currentColor = Color.green;
            Type.color = currentColor;
        }
        else if(CurrentDifficultyMode == 1)
        {

            if (!_songData.DifficultiesLocks[1])
            {
                Type.text = "LOCKED";
                currentColor = Color.yellow;
                Type.color = Color.yellow;
                return;
            }
            
            _currentSongDiff = SongDiff.Normal;

            Type.text = "NORMAL";
            currentColor = Color.yellow;
            Type.color = Color.yellow;
        }
        else if(CurrentDifficultyMode == 2)
        {
            
            if (!_songData.DifficultiesLocks[2])
            {
                Type.text = "LOCKED";
                currentColor = Color.red;
                Type.color = Color.red;
                return;
            }
            _currentSongDiff = SongDiff.Hard;

            Type.text = "HARD";
            currentColor = Color.red;
            Type.color = Color.red;
        }
        
        _score.text = _songData.Scores[(int)_currentSongDiff].ToString();
        _score.color = currentColor;
        
        _songData.LoadSongData();
    }
}
