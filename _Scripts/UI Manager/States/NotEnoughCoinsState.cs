public class NotEnoughCoinsState : UIState
{
    public NotEnoughCoinsState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.NotEnoughDiamonds.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        _uiManager.NotEnoughDiamonds.gameObject.SetActive(false);

    }
}