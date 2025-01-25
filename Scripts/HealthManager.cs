using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Player health values")]
    [SerializeField] int playerHealth;
    [Range(100, 300)]
    [SerializeField] int maxPlayerHealth;

    [Header("Health UI")]
    [SerializeField] GameObject lowHealthUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Slider healthBar;

    [Header("Miscellaneous")]
    public bool amIDead;



    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
        //Setting the default value of the healthbar
        healthBar.maxValue = maxPlayerHealth;
        healthBar.value = maxPlayerHealth;
    }

    public void DamagePlayer(int damage){
        if(playerHealth > 0){
            playerHealth -= damage;
            if (playerHealth < (maxPlayerHealth / 5)){
                //They aren't dead just yet but have low health
                if(lowHealthUI != null) lowHealthUI.SetActive(true);
            }
        } 
        if(playerHealth <= 0){
            //They have run out of health and should be dead
            amIDead = true;
            if(gameOverUI != null) gameOverUI.SetActive(true);
        }
        
        healthBar.value = playerHealth;
    }

    public void HealPlayer(int manna){
        if(playerHealth < (maxPlayerHealth - manna)){
            playerHealth += manna;
            if(playerHealth > (maxPlayerHealth / 5) && lowHealthUI.activeSelf){
                //Turning off low health UI if they have enough
                if(lowHealthUI != null) lowHealthUI.SetActive(false);
                healthBar.value = playerHealth;
            }
            
        } 
    }
}
