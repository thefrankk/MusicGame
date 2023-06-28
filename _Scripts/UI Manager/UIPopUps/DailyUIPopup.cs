using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyUIPopup : UIPopUp, IUseControllerPopUp<DailyController>
{
    
    [Header("Buttons")]
    [SerializeField] private Button _receiveDailyButton;

    private DailyController controller;

    protected override void addListeners()
    {
        base.addListeners();
      
       
    }

    DailyController IUseControllerPopUp<DailyController>._controller { get => FindObjectOfType<DailyController>();  }
    
}
