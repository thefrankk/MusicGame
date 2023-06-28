using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreHeartUIPopup : UIPopUp, IUseControllerPopUp<MoreHeartsController>
{
  
    [Header("Buttons")]
    [SerializeField] private Button[] _moreHeartsButtons;



    protected override void addListeners()
    {
        base.addListeners();
      
      
    }

    MoreHeartsController IUseControllerPopUp<MoreHeartsController>._controller { get;}
   
}
