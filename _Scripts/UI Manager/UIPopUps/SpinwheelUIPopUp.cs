using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinwheelUIPopUp : UIPopUp, IUseControllerPopUp<SpineController>
{
   
   [Header("Buttons")]
   [SerializeField] private Button _spinButton;
   [SerializeField] private Button _watchAdButton;
   


   protected override void addListeners()
   {
      
      base.addListeners();
      
      _spinButton.onClick.AddListener(((IUseControllerPopUp<SpineController>)this)._controller.Spin);
      _watchAdButton.onClick.AddListener(((IUseControllerPopUp<SpineController>)this)._controller.Spin);
      
      ((IUseControllerPopUp<SpineController>)this)._controller.OnSpinAvailable += (b => _spinButton.interactable = b);
   }


   SpineController IUseControllerPopUp<SpineController>._controller { get;  }

}
