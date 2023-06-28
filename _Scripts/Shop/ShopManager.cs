
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public abstract class ShopManager : MonoBehaviour 
{

    protected Item[] _allItems;


    protected virtual void Awake()
    {
        FindAndSubscribeItems();

    }

    public virtual void Start()
    {
      
    }

   

    public void BuyItem(Item item)
    {
        Consumables.DiamondManager.TryToBuy(item.ItemForBuy.Price, (canbuy) =>
        {
            if (!canbuy)
            {

                UIManagers.Instance.OnNotEnoughDiamondsButton(Consumables.DiamondManager.Diamonds.ToString(), item.ItemForBuy.Price.ToString());
                return;
            }
           
            item.ItemForBuy.Unlock(); 
            item.CheckData();
           
        });
    }
    public virtual void SelectItem(Item item)
    {
        item.SelectItem();
    }

    protected abstract void loadData();

    protected abstract void FindAndSubscribeItems();
 

}


public struct PlayerShopSelected
{

    private Dictionary<CharacterControllers, Item> _characters;

    public PlayerShopSelected(CharacterControllers character, Item item)
    {
        _characters = new Dictionary<CharacterControllers, Item>();
        _characters.Add(character, item);
    }
    
    public void TryToAddCharacter(CharacterControllers character, Item item)
    {
        if (_characters.ContainsKey(character))
        {
            _characters[character] = item;
        }
        else
        {
            _characters.Add(character, item);
        }
    }
    
    public Item GetItemFromCharacter(CharacterControllers character)
    {
        if (_characters.ContainsKey(character))
        {
            return _characters[character];
        }
        else
        {
            return null;
        }
    }
}