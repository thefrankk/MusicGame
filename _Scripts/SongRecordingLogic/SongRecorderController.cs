
using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class SongRecorderController : MonoBehaviour
{
   

    private NoteRecordingLogic _noteRecordingLogic;

  
    [Header("slider filler")]
    [SerializeField] private Slider _fillerSlider;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Transform _glow;
    
    private string _songName;
    private float _songLenght;
    private string _diff;
    private string _songType;
    
    public static bool IsRecording = false;
    public static float CurrentSongRunningTime;
    
    //Path where whe save the json file
    private string filePath = "/Resources/SongsDataJson";
   
    
    //Properties
    
    private void Awake()
    {
        _noteRecordingLogic = FindObjectOfType<NoteRecordingLogic>();
    }

  
    

    public void StartRecording()
    {
        GameStarter.Instance.StartGame(() =>
        {
            IsRecording = true;
            MusicManager.Instance.StartSong();
            _songLenght = MusicManager.Instance.GetCurrentSongLenght();
            _songName = MusicManager.Instance.GetCurrentSongName();
            _diff = GameManager.Instance.GetCurrentDiff().ToString();
            _songType = GameManager.Instance.GetCurrentSongType();
            

            BarFiller.StartFiller(_songLenght);
        });

        
      
    }

    public void StopRecording()
    {
        IsRecording = false;
        MusicManager.Instance.StopSong();
        BarFiller.StopFiller();
        _noteRecordingLogic.DeleteRecordings();
    }

    public void EndRecording()
    {
        IsRecording = false;
        saveIntoJson(_noteRecordingLogic.AllNotes);
        
        UIManagers.Instance.OnSongRecordedButton();
    }

    private void saveIntoJson(object obj)
    {
        Debug.Log(obj);
        string json = JsonConvert.SerializeObject(obj);
        Debug.Log(json);

        saveIntoFile(json, _songName+"_"+_diff+"_"+_songType);
    }
    
    private async void saveIntoFile(string json, string name)
    {
        filePath += $"/{name}.json";
        
        Debug.Log(json);
        Debug.Log(Application.dataPath + filePath);

        _loadingText.transform.ScaleToInOut(new Vector3(1.5f, 1.5f, 1.5f), 1.5f);
        _glow.ScaleToInOut(new Vector3(1.5f, 1.5f, 1.5f), 2);
       // File.WriteAllText(Application.dataPath + filePath, json);
        Tween tween = _fillerSlider.DOValue(1, 15f);
        await File.WriteAllTextAsync(Application.dataPath + filePath, json);
        
        Debug.Log("File is completed written.");
       // _fillerSlider.DOValue(1, 1f);

    }
    private void Update()
    {
        if (!IsRecording)
            return;

        if (CurrentSongRunningTime <= _songLenght)
        {
             CurrentSongRunningTime += Time.deltaTime;
        }
        else
        {
            EndRecording();
        }
    }
    
    
  

    public void SetSongName(string name)
    {
        _songName = name;
    }
}
