using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongRecordedPopUp : UIPopUp
{
 
    
    
    [Header("Re-do song button")]
    [SerializeField] private Button _redoSongButton;
    protected override void addListeners()
    {
        base.addListeners();
        _closeButton.onClick.AddListener(UIManagers.Instance.OnMenuButton);
    }

    
}
