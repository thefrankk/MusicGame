using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIState
{
  protected UIManagers _uiManager;
  public abstract void EnterState();
  public abstract void ExitState();
  
}
