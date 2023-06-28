using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScreen : MonoBehaviour
{
    
    public virtual Type GetCurrentType() 
    {
        return typeof(UIScreen);
    }
}

public class UIPopUp : UIScreen
{
    [SerializeField] protected Button _closeButton;



    protected virtual void OnEnable()
    {
        addListeners();
    }

    private void OnDisable()
    {
        Debug.Log("LEVEL UP DEACTIVATED");
        removeListeners();
    }

    protected virtual void addListeners()
    {
        _closeButton.onClick.AddListener(UIManagers.Instance.PopState);
    }

    protected virtual void removeListeners()
    {
        _closeButton.onClick.RemoveAllListeners();
    }


    public override Type GetCurrentType()
    {
        return typeof(UIPopUp);

    }
}


public class UIRewardPopUp : UIPopUp
{
    [Header("Texts")]
    [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected TextMeshProUGUI _heartsRewardText;
    [SerializeField] protected TextMeshProUGUI _diamondsRewardText;
    [SerializeField] protected TextMeshProUGUI _expText;
    
    [Header("TextFiller")]
    [SerializeField] protected Slider _filler;
    
    [Header("Claim x2 Button")]
    [SerializeField] private Button _claimX2Button;
}
public interface IInformativePopUp
{
    /// <summary>
    /// Sets the text of the pop up
    /// </summary>
    /// <param name="data">First current, then needed</param>
    void SetText(params string[] data);
}

public interface IUseControllerPopUp<T>
{
    [SerializeField] protected internal T _controller { get; }
}