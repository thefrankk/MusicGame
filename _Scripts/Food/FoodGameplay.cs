using System;
using UnityEngine;

public class FoodGameplay : FoodLogic
{
    
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DestroyObject();

        }
    }

    protected override void DestroyObject(float delay = 0)
    {
        base.DestroyObject(delay);

        if (delay != 0)
        {
            GameManager.Instance.ReceiveDamage();
            Consumables.ScoreManager.Substract(5);
            CameraShake.Instance.ShakeCamera();
        }
        else
        {
            _effectEat[UnityEngine.Random.Range(0, _effectEat.Length)].Play();
        }
     
        
        
    }
}
