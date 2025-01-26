using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    [SerializeField] int healAmount = 5;



    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            Debug.Log("Should be trying to heal now");
            healthManager = FindObjectOfType<HealthManager>();
            healthManager.HealPlayer(healAmount);
        }
    }
}
