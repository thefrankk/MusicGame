using System.Linq;
using Spine;
using UnityEngine;
using UnityEngine.UI;

public class FoodShop : ShopManager
{
    [SerializeField] private Image _foodSelectedSprite;
    
    private SkinFood _skinFoodSelected;
    
    //Properties
    public SkinFood SkinFoodSelected => _skinFoodSelected;
    
    public override void Start()
    {
        loadData();

    }
    protected void OnEnable()
    {
        if(_allItems.Length > 0)
            loadData();
    }
    
    protected override void loadData()
    {
        Debug.Log("load data");
        var foodSelectedID = PlayerPrefs.GetInt("FoodSelectedID", 0);
        
        var foodItem = _allItems.Where((x) => x.ItemForBuy is ItemFood)
            .FirstOrDefault((x) => x.ItemForBuy.Id == foodSelectedID);

        _skinFoodSelected = foodItem as SkinFood;
        SelectItem(foodItem);
        
    }

    protected override void FindAndSubscribeItems()
    {
        Debug.Log("Truing to find items");
        _allItems = FindObjectsOfType<SkinFood>(true);

        Debug.Log(_allItems.Length);
        foreach (var item in _allItems)
        {
            item.OnBuyItem += BuyItem;
            item.OnSelectItem += SelectItem;
        }
    }

    public override void SelectItem(Item item)
    {
        _skinFoodSelected?.DeselectItem();
        base.SelectItem(item);
        _skinFoodSelected = item as SkinFood;
        _foodSelectedSprite.sprite = _skinFoodSelected.ItemForBuy.ItemImage;
        PlayerPrefs.SetInt("FoodSelectedID", item.ItemForBuy.Id);
        GameManager.Instance.SetFoodSprite(_skinFoodSelected.ItemForBuy.ItemImage);
    }
    
}
