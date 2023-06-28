
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RecordInputHandler : MonoBehaviour
{
    [SerializeField] private Button[] _buttonForSetNote;
    [SerializeField] private GameObject[] _containersButtons;
    
    
    
    //Buttons for set note actions
    public Action<int> OnButtonPressed;

    private void Awake()
    {
        Debug.Log("added");
        foreach (var button in _buttonForSetNote)
        {

            button.onClick.AddListener(() => OnButtonPressed?.Invoke(Array.IndexOf(_buttonForSetNote, button)));
            button.onClick.AddListener(() =>
            {
                // _containersButtons[Array.IndexOf(_buttonForSetNote, button)].SetActive(true);
                button.transform.ScaleToAndBack(new Vector3(1.3f, 1.3f, 1.3f), 0.1f, new Vector3(0.75f, 0.75f, 0.75f));
            });

        }

      
    }

   
}


