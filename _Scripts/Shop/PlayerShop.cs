using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShop : ShopManager
{
    private MenuPopup _menuPopup;
    private PlayerShopSelected _playerShopSelected;
    
    private CharacterControllers _characterSelected;
    
    private int _index = 0;
    
    
    [Header("Buttons for select player")]
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _menuPopup = GetComponentInParent<MenuPopup>();
        setCharacterPosition(_menuPopup.CharacterLeft, _menuPopup.CharacterRight, 3);
        _playerShopSelected = new PlayerShopSelected(_characterSelected, null);
    }


    public override void Start()
    {
      
        _rightButton.onClick.AddListener(rightArrow);
        _leftButton.onClick.AddListener(leftArrow);

        loadData();

    }


    protected void OnEnable()
    {
        setCharacterPosition(_menuPopup.CharacterLeft, _menuPopup.CharacterRight, 3);

        _index = 0;
        
        if(_allItems.Length > 0)
            loadData();

    }

    public override void SelectItem(Item item)
    {
        _playerShopSelected.GetItemFromCharacter(_characterSelected)?.DeselectItem();
        base.SelectItem(item);
        _playerShopSelected.TryToAddCharacter(_characterSelected, item);

        PlayerPrefs.SetInt("PlayerSelected" + _index, item.ItemForBuy.Id);
        ItemCharacter itemCharacter = (ItemCharacter) item.ItemForBuy;
        
        Debug.Log(_index);
        _characterSelected.SetPlayerData(itemCharacter.PlayerData);
    }

    protected override void loadData()
    {
        Debug.Log("Loading data");
        var firstCharacterIndex = PlayerPrefs.GetInt("PlayerSelected0", 0);
        var secondCharacterIndex = PlayerPrefs.GetInt("PlayerSelected1", 0);

        var charachterItem = _allItems.Where((x) => x.ItemForBuy is ItemCharacter)
                                .FirstOrDefault((x) => x.ItemForBuy.Id == (_index == 0 ? firstCharacterIndex : secondCharacterIndex));
        
        //var item = _allItems.FirstOrDefault((x) => x.ItemForBuy.Id == (_index == 0 ? firstCharacterIndex : secondCharacterIndex));
       // _playerShopSelected.TryToAddCharacter(_characterSelected, charachterItem);
        SelectItem(charachterItem);
    }

    protected override void FindAndSubscribeItems()
    {
        Debug.Log("Truing to find items");
        _allItems = FindObjectsOfType<SkinPlayer>(true);

        Debug.Log(_allItems.Length);
        foreach (var item in _allItems)
        {
            item.OnBuyItem += BuyItem;
            item.OnSelectItem += SelectItem;
        }
    }


    private void setCharacterPosition(CharacterControllers selectedCharacter, CharacterControllers nonSelected,
        float pos)
    {
        _characterSelected = selectedCharacter;
        _characterSelected.transform.DOMoveX(_menuPopup.CharacterCenter.transform.position.x, 0.5f);
        nonSelected.transform.DOMoveX(nonSelected.transform.position.x + pos, 0.5f);
    }

   

    private void rightArrow()
    {
        _playerShopSelected.GetItemFromCharacter(_characterSelected)?.DeselectItem();
        setCharacterPosition(_menuPopup.CharacterLeft, _menuPopup.CharacterRight, 3);
        _index = 0;
        var itemFromCharacter = _playerShopSelected.GetItemFromCharacter(_characterSelected);

        if (itemFromCharacter != null)
        {
            itemFromCharacter?.SelectItem();
            PlayerPrefs.SetInt("PlayerSelected" + _index, itemFromCharacter.ItemForBuy.Id);
        }
        else
        {
            loadData();
        }
    }

    private void leftArrow()
    {
        _playerShopSelected.GetItemFromCharacter(_characterSelected)?.DeselectItem();
        setCharacterPosition(_menuPopup.CharacterRight, _menuPopup.CharacterLeft, -3);
        _index = 1;

        var itemFromCharacter = _playerShopSelected.GetItemFromCharacter(_characterSelected);

        if (itemFromCharacter != null)
        {
            itemFromCharacter?.SelectItem();
            PlayerPrefs.SetInt("PlayerSelected" + _index, itemFromCharacter.ItemForBuy.Id);
        }
        else
        {
            loadData();
        }
    }
}
