using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class GameOverPopUp : UIPopUp, IUseControllerPopUp<GameOver>, IInformativePopUp
{
    [Header("Buttons")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _continueWithAD;
    [SerializeField] private Button _continueWithGems;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _currentContinues;
    [FormerlySerializedAs("_continuePrice")] [SerializeField] private TextMeshProUGUI _continuePriceText;



    private int _continuePrice;

    protected override void addListeners()
    {
        base.addListeners();
        _homeButton.onClick.AddListener(UIManagers.Instance.PopState);
        _homeButton.onClick.AddListener(GameManager.Instance.ResetGameToStart);
        _homeButton.onClick.AddListener(() =>
        {
            _continueWithAD.interactable = true;
            _continueWithGems.interactable = true;
            
            UIManagers.Instance.OnLoadingButton(() =>
            {
                UIManagers.Instance.OnMenuButton();
            });
        });

        
        _replayButton.onClick.AddListener(() =>
        { 
            
            _continueWithAD.interactable = true;
            _continueWithGems.interactable = true;
            
            UIManagers.Instance.PopState();
            GameManager.Instance.ResetGameToStart();
            GameManager.Instance.ReplayGame();
        });
        
        _continueWithGems.onClick.AddListener(() =>
        {
            Consumables.DiamondManager.TryToBuy(_continuePrice, (canbuy) =>
            {
                if (canbuy)
                {
                    UIManagers.Instance.PopState();
                    ((IUseControllerPopUp<GameOver>)this)._controller.Revive();
                    Consumables.ContinuesManager.Substract(1);
                }
                else
                {
                    UIManagers.Instance.OnNotEnoughDiamondsButton(Consumables.DiamondManager.Diamonds.ToString(), _continuePrice.ToString());
                }
            });
            
         
        });
        
        
        
       
    }


    GameOver IUseControllerPopUp<GameOver>._controller { get => FindObjectOfType<GameOver>(); }

    public void SetText(params string[] data)
    {
        _currentContinues.text = data[0];

        _continuePrice = UnityEngine.Random.Range(55, 155);

        _continuePriceText.text = _continuePrice.ToString();
        bool canContinue = int.Parse(_currentContinues.text) > 0;
        _continueWithAD.interactable = canContinue;
        _continueWithGems.interactable = canContinue;
    }
}