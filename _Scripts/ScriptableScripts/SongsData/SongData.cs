
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "Song Data", order = 0)]
public class SongData : ScriptableObject
{
    [SerializeField] private string _songName;
    [SerializeField] private MusicManager.SongClips _songClip;
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private int _unlockPrice;
    [SerializeField] private int _unlockRecordingPrice;

    [SerializeField] private int _levelRequired;

    [SerializeField] private int _levelRequieredExtraDiff;
    [SerializeField] private int _unlockPriceExtraDiff;
    
    private int[] _scores = new int[3];
    private int[] _scoresExtraDiff = new int[3];

    
    [SerializeField] private bool _isUnlocked = false;
    [SerializeField] private bool _isRecordingSongUnlocked = false;
    private bool _isExtraDiffUnlocked = false;
    
    private bool[] _difficultiesLocks = new bool[3];
    private bool[] _difficutlesExtraDiff = new bool[3];

    private LinkedList<NoteData> _noteDatas;

    //Properties
    public string SongName => _songName;
    public MusicManager.SongClips SongClip => _songClip;
    public int[] Scores => _scores;
    public Color BackgroundColor => _backgroundColor;
    public bool IsUnlocked => _isUnlocked;
    public bool IsRecordingSongUnlocked => _isRecordingSongUnlocked;
    public LinkedList<NoteData> NoteDatas => _noteDatas;
    public bool[] DifficultiesLocks => _difficultiesLocks;
    public int UnlockRecordingPrice => _unlockRecordingPrice;
    public int UnlockPrice => _unlockPrice;
    public int LevelRequired => _levelRequired;
    public bool IsExtraDiffUnlocked => _isExtraDiffUnlocked;
    public int LevelRequieredExtraDiff => _levelRequieredExtraDiff;
    public int UnlockPriceExtraDiff => _unlockPriceExtraDiff;
    public int[] ScoresExtraDiff => _scoresExtraDiff;
    
    private readonly string filePath = "SongsDataJson/";
 
    public void LoadSongData()
    {
        _isUnlocked = PlayerPrefs.GetInt(_songName + "unlocked", 0) == 1;
        _isExtraDiffUnlocked = PlayerPrefs.GetInt(_songName+ "extraDiff" + "unlocked", 0) == 1;
        _isRecordingSongUnlocked = PlayerPrefs.GetInt(_songName + "recordingUnlocked", 0) == 1;
        
        _scores[0] = PlayerPrefs.GetInt(_songName + "score" + 0, 0);
        _scores[1] = PlayerPrefs.GetInt(_songName + "score" + 1, 0);
        _scores[2] = PlayerPrefs.GetInt(_songName + "score" + 2, 0);
        
        _scoresExtraDiff[0] = PlayerPrefs.GetInt(_songName + "extraDiff" + "score" + 0, 0);
        _scoresExtraDiff[1] = PlayerPrefs.GetInt(_songName + "extraDiff" + "score" + 1, 0);
        _scoresExtraDiff[2] = PlayerPrefs.GetInt(_songName + "extraDiff" + "score" + 2, 0);
        
        _difficultiesLocks[0] = PlayerPrefs.GetInt(_songName + 0 + "lockStatus", 1) == 1;
        _difficultiesLocks[1] = PlayerPrefs.GetInt(_songName + 1 + "lockStatus", 0) == 1;
        _difficultiesLocks[2] = PlayerPrefs.GetInt(_songName + 2 + "lockStatus", 0) == 1;
        
        _difficutlesExtraDiff[0] = PlayerPrefs.GetInt(_songName + "extraDiff" + 0 + "lockStatus", 1) == 1;
        _difficutlesExtraDiff[1] = PlayerPrefs.GetInt(_songName + "extraDiff" + 1 + "lockStatus", 0) == 1;
        _difficutlesExtraDiff[2] = PlayerPrefs.GetInt(_songName + "extraDiff" + 2 + "lockStatus", 0) == 1;
    }

    public void UnlockSong()
    {
        _isUnlocked = true;
        PlayerPrefs.SetInt(_songName + "unlocked", 1);
    }
    public void UnlockExtraDiff()
    {
        _isExtraDiffUnlocked = true;
        PlayerPrefs.SetInt(_songName + "extraDiff" + "unlocked", 1);
    }
    public void UnlockForRecording()
    {
        _isRecordingSongUnlocked = true;
        PlayerPrefs.SetInt(_songName + "recordingUnlocked", 1);
    }
    public IEnumerator LoadNoteData(Action<LinkedList<NoteData>> callback, int diff, string songType)
    {
        ResourceRequest obj = Resources.LoadAsync<TextAsset>(filePath + _songName+"_"+diff+"_"+songType);

        while (!obj.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log(obj.asset);
        _noteDatas = JsonConvert.DeserializeObject<LinkedList<NoteData>>(obj.asset.ToString());

        callback?.Invoke(_noteDatas);
    }
}

    

