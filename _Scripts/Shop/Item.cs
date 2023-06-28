
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
        [Header("Item for buy")]
        [SerializeField] protected ItemForBuy _itemForBuy;
        
        [Header("Texts")] 
        [SerializeField] protected TextMeshProUGUI _priceText;
        [SerializeField] protected TextMeshProUGUI _levelRequiredText;
        
        [Header("Image")]
        [SerializeField] protected Image _itemImage;
        [Header("Containers")] 
    
        [SerializeField] protected Transform _isUnlockedContainer;
        [SerializeField] protected Transform _isLockedContainer;
        [SerializeField] protected Transform _isSelectedContainer;
        [SerializeField] protected Transform _isLockedAndNotReachLevel;

        [Header("Buttons")]
        [SerializeField] protected Button _buyButton;
        [SerializeField] protected Button _selectButton;

        
        //Actions
        public Action<Item> OnBuyItem;
        public Action<Item> OnSelectItem;
        
        
        private bool _isSelected = false;



        //properties
        public ItemForBuy ItemForBuy => _itemForBuy;
        public bool IsSelected => _isSelected;
          
        private void Awake()
        {
                addListeners();
        }

        private void OnEnable()
        {
                CheckData();
        }

        private void Start()
        {
                ConfigItem();
        }

        private void addListeners()
        {
                _buyButton.onClick.AddListener(() =>
                {
                        OnBuyItem?.Invoke(this);
                }); 
                
                _selectButton.onClick.AddListener(() =>
                {
                        OnSelectItem?.Invoke(this);
                });
        }

        public void CheckData()
        {
            _itemForBuy.LoadItemData();
            
            _isUnlockedContainer.gameObject.SetActive(_itemForBuy.IsUnlocked);
            _isLockedContainer.gameObject.SetActive(!_itemForBuy.IsUnlocked);
            _isSelectedContainer.gameObject.SetActive(_itemForBuy.IsSelected);
            _isUnlockedContainer.gameObject.SetActive(!_itemForBuy.IsSelected);
        }

        public void SelectItem()
        {
                _isSelected = true;
                _isSelectedContainer.gameObject.SetActive(_isSelected);
                _isUnlockedContainer.gameObject.SetActive(!_isSelected);
                
                Debug.Log("selected");
        }
        
        public void DeselectItem()
        {
                Debug.Log("deselected");

                _isSelected = false;
                _isSelectedContainer.gameObject.SetActive(_isSelected);
                _isUnlockedContainer.gameObject.SetActive(!_isSelected);

        }
        
        public virtual void ConfigItem()
        {
                _itemImage.sprite = _itemForBuy.ItemImage;
                _priceText.text = _itemForBuy.Price.ToString();
                _levelRequiredText.text = _itemForBuy.LevelRequired.ToString();
        }
        
       
}
