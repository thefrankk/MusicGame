using System;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterControllers : MonoBehaviour
{

    [Header("Showing")]
    public SkeletonAnimation Graphics;
    public SkeletonAnimation Effect;

    [Header("Controller Particle")]
    public ParticleSystem EffectPlayText;

    private SpriteRenderer _spriteRenderer;
    
    [Header("Particles")] 
    [SerializeField] private ParticleSystem[] _effectEat;

    private ParticleSystem _eatEffect;
    private PlayerData _playerData;
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
           // Graphics.AnimationState.SetAnimation(0, "eat", false);
           // Effect.AnimationState.SetAnimation(0, "animation", false);
            StartCoroutine(BackAnim());
            Consumables.ScoreManager.Add(10);
            EffectPlayText.Play();
            _effectEat[Random.Range(0, _effectEat.Length)].Play();
            //_eatEffect.Play();
           // Destroy(other.gameObject);
        }
    }
    
    IEnumerator BackAnim()
    {
        yield return new WaitForSeconds(0.5f);
        //Graphics.AnimationState.SetAnimation(0, "idle_main", true);
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }


    public void SetPlayerData(PlayerData data)
    {
        _playerData = data;
        _spriteRenderer.sprite = data.CurrentSprite;
        _eatEffect = data.EatEffect;
    }
}
