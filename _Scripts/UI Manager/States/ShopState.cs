public class ShopState : UIState
{
    public ShopState(UIManagers uiManagers)
    {
        this._uiManager = uiManagers;
    }

    public override void EnterState()
    {
        _uiManager.ShopUI.gameObject.SetActive(true);

    }

    public override void ExitState()
    {
        _uiManager.ShopUI.gameObject.SetActive(false);

    }
}