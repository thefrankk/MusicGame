using System;
using UnityEngine;
using UnityEngine.UI;

public class BarFiller : MonoBehaviour
{
    [SerializeField] private Image _fillingBar;
    
    private static float _fillingTime;
    private static bool _isFilling;


    public static Action OnFillerEnded;

    public static void StartFiller(float seconds)
    {
        _fillingTime = seconds;
        _isFilling = true;
    }

    public static void UnPauseFiller()
    {
        _isFilling = true;
    }
    public static void PauseFiller()
    {
        _isFilling = false;
    }
    public static void StopFiller()
    {
        _fillingTime = 0;
        _isFilling = false;
        
    }
    void Update()
    {

        if (!_isFilling)
        {
            _fillingBar.fillAmount = 0;
            return;
        }
          
        
        
        if(_fillingBar.fillAmount < 1)
        {
            _fillingBar.fillAmount += Time.deltaTime / _fillingTime;
           
        }
        else 
        { 
            _isFilling = false;
            OnFillerEnded?.Invoke();
        }
        
        
    }
}