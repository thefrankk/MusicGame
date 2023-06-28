using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordingUIScreen : UIScreen, IPauseSettings
{
    [Header("Buttons")]
    [SerializeField] private Button _pauseButton;
    [Header("Characters")]
    [SerializeField] private  GameObject ContainerCharacter;
    [SerializeField] private GameObject CharacterLeft;
    [SerializeField] private GameObject CharacterRight;

    [Header("Position")]
    [SerializeField] private Transform PositionLeft;
    [SerializeField] private Transform PositionRight;
    
    [SerializeField] private Button _openRecordingUI;

    [Header("Texts")] 
    [SerializeField] private TextMeshProUGUI _notesCount;
    
    [Header("Song Recorder controller")]
    [SerializeField] SongRecorderController _songRecorderController;

    private void OnEnable()
    {
       
        ContainerCharacter.transform.localPosition = new Vector3(0, 0f, 0);

        CharacterLeft.transform.localPosition = PositionLeft.transform.localPosition;
        CharacterRight.transform.localPosition = PositionRight.transform.localPosition;
            
        _songRecorderController.StartRecording();
        
        NoteRecordingLogic.OnNoteCreated += actualizeText;
    }
    
    private void OnDisable()
    {
        NoteRecordingLogic.OnNoteCreated -= actualizeText;
    }

    private void actualizeText(int notesCount)
    {
        _notesCount.text = notesCount.ToString();
    }
    private void Awake()
    {
            UIManagers.Instance.OnRecordingButton();

            
            _pauseButton.onClick.AddListener(() =>
            {
                UIManagers.Instance.OnPauseButton(this,
                    MusicManager.Instance.GetCurrentSongName(),
                    $"-",
                    $"-"
                );
            });
        
    }

    public void StopGame()
    {
       _songRecorderController.StopRecording();
    }
}
