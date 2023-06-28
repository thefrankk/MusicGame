using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//change this..
public class PanelSetting : UIScreen
{
    [Header("Music & Sound & Vibration")]
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _soundVolumeSlider;
    [SerializeField] private Toggle _vibrationToggle;

    [Header("Buttons")]
    [Header("Close screen")]
    [SerializeField] private Button _closeButton;
    [Header("Rate")]
    [SerializeField] private Button _rateButton;
    [Header("Language")]
    [SerializeField] private Button _languageButton;
    [Header("Like")]
    [SerializeField] private Button _likeButton;
    [Header("About")]
    [SerializeField] private Button _aboutButton;

    private void Awake()
    {
        addListeners();
    }

    private void addListeners()
    {
        _closeButton.onClick.AddListener(UIManagers.Instance.PopState);
    }

    void Update()
    {
    }
    public void SetVolume(float Value)
    {
        PlayerPrefs.SetFloat("Volume", Value);
    }
    public void RateUs()
    {
        Application.OpenURL("");
    }
    public void Share()
    {
        Application.OpenURL("Share");
    }
}
