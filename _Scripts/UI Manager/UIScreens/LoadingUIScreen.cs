using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;


public class LoadingUIScreen : UIScreen
{
    [Header("UI")]
    [SerializeField] private Slider _fillingBar;
    [SerializeField] private TextMeshProUGUI _value;

    public Action OnLoadingEndend;
    
    public void StartLoadingScreen(Action callback)
    {

        var duration = UnityEngine.Random.Range(2, 4);
        _fillingBar.DOValue(1, duration).onComplete = () =>
        {
            callback?.Invoke();
            _value.text = "LOADING " + 0 + "%";
            _fillingBar.value = 0;
        };

        DOVirtual.Float(0, 1, duration, (value) =>
        {
            _value.text = "LOADING " + (int)(value * 100) + "%";
        });


    }
   
}
