using System;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuPopup : UIScreen
{

    [Header("Buttons")] 
    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _luckyWhellButton;
    [SerializeField] private Button _dailyButton;
    [SerializeField] private Button _moreHeartsButton;
    [SerializeField] private Button _shopButton;
    
    [Header("Buttons songs")]
    [SerializeField] private Button _hotNewSongsButton;
    [SerializeField] private Button _allSongsButton;
    [SerializeField] private Button _myRecordedSongsButton;
    
    [Header("Overlay UI")]
    [SerializeField] private TextMeshProUGUI _diamondsText;
    [FormerlySerializedAs("_heartsText")] [SerializeField] private TextMeshProUGUI _currentHeartsText;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _currentExpText;
   
    
    [SerializeField] private Slider _expSlider;


    [Header("UI Controller")]
    [SerializeField] private GameObject[] _listHotNew;
    [SerializeField] private GameObject[] _allSongs;
    [SerializeField] private GameObject[] _songs;
    [SerializeField] private GameObject[] _developerSongs;
    [SerializeField] private GameObject[] _mySongs;

    [Header("GameObjects")]
    [SerializeField] private CharacterControllers _characterLeft;
    [SerializeField] private CharacterControllers _characterRight;
    [SerializeField] private GameObject _characterCenter;

    [Header("Boolean Manager")]
    internal float MovingFilling = 0.05f;


    [Header("Vectors Controller")]
    internal Vector3 PositionLeft;
    internal Vector3 PositionRight;

    [Header("Boolean Manager")]
  
    internal bool SetPositionInitial = true;
    internal bool StartPosition = false;
    
    //Properties
    public CharacterControllers CharacterLeft => _characterLeft;
    public GameObject CharacterCenter => _characterCenter;
    public CharacterControllers CharacterRight => _characterRight;

    void OnEnable()
    {
          _characterCenter.transform.localPosition = new Vector3(0, 3.62f, 0);
          _characterRight.transform.localPosition = PositionRight;
          _characterLeft.transform.localPosition = PositionLeft;

        DiamondManager.OnAssetChanged += ActualizeText;
        HeartsManager.OnAssetChanged += ActualizeText;

        ActualizeText();
    }

    private void OnDisable()
    {
        DiamondManager.OnAssetChanged -= ActualizeText;
        HeartsManager.OnAssetChanged -= ActualizeText;
    }

    public void ActualizeText()
    {
        _diamondsText.text = Consumables.DiamondManager.Diamonds.ToString();
        _currentHeartsText.text = Consumables.HeartsManager.Hearts.ToString()+"/"+Consumables.HeartsManager.MaxHearts.ToString();
        _currentLevelText.text = PlayerLevelManager.Instance.Level.ToString();
        _currentExpText.text = PlayerLevelManager.Instance.CurrentExp.ToString()+"/"+PlayerLevelManager.Instance.ExpNedeed.ToString();
      
        _expSlider.value = PlayerLevelManager.Instance.CurrentExp / PlayerLevelManager.Instance.ExpNedeed;
    }
    void Awake()
    {
        if (SetPositionInitial)
        {
            PositionLeft = _characterLeft.transform.localPosition;
            PositionRight = _characterRight.transform.localPosition;
            SetPositionInitial = false;
        }
        else
        {
            StartPosition = true;
        }

       
    }

    void Start()
    {
        foreach (GameObject obj in _listHotNew)
        {
            obj.SetActive(false);
         
        }
        loadShopData();
        addListeners();
    }

    public void SetCharactersToInitialPosition()
    {
        CharacterRight.transform.DOMoveX(PositionRight.x, 0.5f);
        CharacterLeft.transform.DOMoveX(PositionLeft.x, 0.5f);
    }
    
    private void loadShopData()
    {
        Item[] _allItems = FindObjectsOfType<Item>(true);
        loadPlayerData(_allItems);
        loadFoodData(_allItems);
    }

    private void loadPlayerData(Item[] items)
    {
        var firstCharacterIndex = PlayerPrefs.GetInt("PlayerSelected0", 0);
        var secondCharacterIndex = PlayerPrefs.GetInt("PlayerSelected1", 0);

        var item1 = items.FirstOrDefault((x) => x.ItemForBuy.Id == firstCharacterIndex);
        var item2 = items.FirstOrDefault((x) => x.ItemForBuy.Id == secondCharacterIndex);


        var itemCharacter1 = items.Where((x) => x.ItemForBuy is ItemCharacter)
                                               .Select((x) => x.ItemForBuy)
                                               .FirstOrDefault((x) => x.Id == firstCharacterIndex) as ItemCharacter;
        
        var itemCharacter2 = items.Where((x) => x.ItemForBuy is ItemCharacter)
                                                .Select((x) => x.ItemForBuy) 
                                                .FirstOrDefault((x) => x.Id == secondCharacterIndex) as ItemCharacter;
        
        _characterLeft.SetPlayerData(itemCharacter1.PlayerData);
        _characterRight.SetPlayerData(itemCharacter2.PlayerData);
    }

    private void loadFoodData(Item[] items)
    {
        var firstFoodIndex = PlayerPrefs.GetInt("FoodSelected0", 0);
        
        var item1 = items.FirstOrDefault((x) => x.ItemForBuy.Id == firstFoodIndex);
        
        var itemFood = items.Where((x) => x.ItemForBuy is ItemFood)
                                               .Select((x) => x.ItemForBuy)
                                               .FirstOrDefault((x) => x.Id == firstFoodIndex) as ItemFood;
        
        
        GameManager.Instance.SetFoodSprite(itemFood.ItemImage);
    }

  
 
  

    
    private bool _isSongsScaling = false;
    public void SetAllSongs()
    {
        if(_isSongsScaling) return;
        Debug.Log("settings al songs");
        disableAllSongViews((() =>
        {
             scaleObjects(_allSongs);
        }));

    }

    
    public void SetHotNew()
    {
        if(_isSongsScaling) return;

        Debug.Log("hotnews");

        disableAllSongViews((() =>
        {
            scaleObjects(_listHotNew);
        }));
    }
    public void SetMyRecordedSongs()
    {
        if(_isSongsScaling) return;

        #if UNITY_EDITOR == true
        disableAllSongViews((() =>
        {
            scaleObjects(_developerSongs);
        }));
        return;
        #endif
        Debug.Log("recorded");

        disableAllSongViews((() =>
        {
            scaleObjects(_mySongs);
        }));


    }
    private async void scaleObjects(GameObject[] objects)
    {
        _isSongsScaling = true;
        
        _allSongsButton.interactable = false;
        _hotNewSongsButton.interactable = false;
        _myRecordedSongsButton.interactable = false;

        int index = 0;
        foreach (var song in objects)
        {
            if (index < 5)
            {
                song.SetActive(true);
                song.transform.ScaleTo(new Vector3(0.87f, 0.87f, 0.87f), 0.3f);

                await  Task.Delay(200);
            }
            else //This is for only charge the first 5 songs and all else will be charged instantly.
            {
                song.SetActive(true);
                song.transform.localScale = new Vector3(0.87f, 0.87f, 0.87f);
            }

            index++;

        }

        _allSongsButton.interactable = true;
        _hotNewSongsButton.interactable = true;
        _myRecordedSongsButton.interactable = true;
        
        _isSongsScaling = false;


    }
    private void disableAllSongViews(Action callback)
    {
        foreach (GameObject obj in _allSongs)
        {
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.SetActive(false);
        }
        callback?.Invoke();
    }
    
    private void addListeners()
    {
        _settingsButton.onClick.AddListener(UIManagers.Instance.OnSettingsButton);
        _luckyWhellButton.onClick.AddListener(UIManagers.Instance.OnLuckyWheelButton);
        _dailyButton.onClick.AddListener(UIManagers.Instance.OnDailyRewardButton);
        
        _allSongsButton.onClick.AddListener(SetAllSongs);
        _hotNewSongsButton.onClick.AddListener(SetHotNew);
        _myRecordedSongsButton.onClick.AddListener(SetMyRecordedSongs);
        _shopButton.onClick.AddListener(UIManagers.Instance.OnShopButton);
      //  _moreHeartsButton.onClick.AddListener(UIManagers.Instance.OnMoreHeartsButton);
        
    }
}
