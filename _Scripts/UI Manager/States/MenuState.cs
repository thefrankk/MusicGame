using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : UIState
{
    
    public MenuState(UIManagers uiManager) 
    {
        this._uiManager = uiManager;
    }
    public override void EnterState()
    {
        _uiManager.MenuUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.MenuUI.gameObject.SetActive(false);
        
    }
}
