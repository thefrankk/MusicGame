
public class ShopPopUp : UIPopUp, IUseControllerPopUp<ShopManager>
{

    private MenuPopup _menuPopup;

    
    private void Awake()
    {
        _menuPopup = GetComponentInParent<MenuPopup>();
    }
    
    protected override void addListeners()
    {
        base.addListeners();
        _closeButton.onClick.AddListener(() =>
        {
            _menuPopup.SetCharactersToInitialPosition();
        });
        
     
    }
    
    ShopManager IUseControllerPopUp<ShopManager>._controller { get => FindObjectOfType<ShopManager>();  }
}
