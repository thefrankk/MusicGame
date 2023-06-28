using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Spine.Unity;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance;
    [SerializeField] private GameObject _objOne;
    [SerializeField] private GameObject _objTwo;
    [SerializeField] private GameObject _objThree;
    


    private bool _isInitilizing;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }
    

    public async void StartGame(Action callback)
    {
        if (_isInitilizing)
            return;

         loadingGame(callback);
        
       
        
    }
    
    private async void loadingGame(Action callback)
    {
        _isInitilizing = true;

        _objOne.SetActive(true);
        await Task.Delay(700);
        _objTwo.SetActive(true);
        _objOne.SetActive(false);
        await Task.Delay(700);
        _objThree.SetActive(true);
        _objTwo.SetActive(false);
        await Task.Delay(700);
        _objThree.SetActive(false);
        await Task.Delay(500);
        callback?.Invoke();
        _isInitilizing = false;
    }
}
