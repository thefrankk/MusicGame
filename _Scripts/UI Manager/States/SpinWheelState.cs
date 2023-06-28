public class SpinWheelState : UIState
{
    public SpinWheelState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }
    public override void EnterState()
    {
        _uiManager.SpinWheelUI.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.SpinWheelUI.gameObject.SetActive(false);

    }
}