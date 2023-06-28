using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private Sprite _currentSprite;
    [SerializeField] private ParticleSystem _eatEffect;
    [SerializeField] private ParticleSystem _speedEffect;
    
    
    //Properties
    public string Name => _name;
    public Sprite CurrentSprite => _currentSprite;
    public ParticleSystem EatEffect => _eatEffect;
    public ParticleSystem SpeedEffect => _speedEffect;
    
}
