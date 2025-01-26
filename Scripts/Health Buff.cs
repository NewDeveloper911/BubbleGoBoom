using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    [SerializeField] Collider2D player;
    [SerializeField] int healAmount = 5;
    void OnTriggerEnter2D(Collider2D other){
        if (other == player){
            healthManager.HealPlayer(healAmount);
        }
    }
}
