using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPlayer : Item
{
    
    [SerializeField] private Image _backGroundImage;

    [SerializeField] protected TextMeshProUGUI _nameText;

    
    [Header("BackgroundColor")]
    [SerializeField] private Color _bkgColor;
    
    //Properties
    public Color BkgColor => _bkgColor;

    
    public override void ConfigItem()
    {
        base.ConfigItem();
        _backGroundImage.color = _bkgColor;
        ItemCharacter itemCharacter = (ItemCharacter)_itemForBuy;
        
        _nameText.text = itemCharacter.PlayerData.Name;
    }
}