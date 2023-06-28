


using UnityEngine;
using UnityEngine.UI;

public class SongViewRecording : SongView
{

    [Header("Recording button")]
    [SerializeField]
    private Button _recordingButton;
    protected override void Start()
    {
        base.Start();
        _recordingButton.onClick.AddListener(RecordButton);
        _backgroundImage.color = _songData.BackgroundColor;

    }

    public override void CheckBackData()
    {
        _unlockedSong.SetActive(_songData.IsRecordingSongUnlocked);
        _lockedSong.SetActive(!_songData.IsRecordingSongUnlocked);

        if (!_songData.IsRecordingSongUnlocked)
        {
            _unlockPrice.text = _songData.UnlockRecordingPrice.ToString();
        }
    }

    protected void RecordButton()
    {
        GameManager.Instance.SetCurrentSongType(_songType);
        base.PlayButton();
        UIManagers.Instance.OnLoadingButton(() =>
        {
            UIManagers.Instance.OnRecordingButton();
        });
    }

    protected override void PlayButton()
    {
        base.PlayButton();
        StartCoroutine(_songData.LoadNoteData((noteData =>
        {
            Debug.Log((int)_currentSongDiff);
        
            GameData gameData = new GameData(noteData, _currentSongDiff, _difficultiesDelay[(int)_currentSongDiff]);
            GameManager.Instance.StartGame(ref gameData);
        }), (int)_currentSongDiff, _songType));
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
            _currentSongDiff = SongDiff.Normal;

            Type.text = "NORMAL";
            currentColor = Color.yellow;
            Type.color = Color.yellow;
        }
        else if(CurrentDifficultyMode == 2)
        {
            _currentSongDiff = SongDiff.Hard;

            Type.text = "HARD";
            currentColor = Color.red;
            Type.color = Color.red;
        }
        GameManager.Instance.SetCurrentDiff((int)_currentSongDiff);
        
    }

    protected override void buySong()
    {
        Consumables.DiamondManager.TryToBuy(_songData.UnlockPrice, (canBuy) =>
        {
            if (!canBuy)
            {
                //Can't buy logic
                UIManagers.Instance.OnNotEnoughDiamondsButton();
                return;
            }
            
            _songData.UnlockForRecording();
        });
        
        base.buySong();
    }
}
