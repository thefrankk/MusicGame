public class LoadingState : UIState
{
    public LoadingState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.LoadingUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.LoadingUI.gameObject.SetActive(false);
    }
}