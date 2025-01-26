using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{

    [SerializeField] int health;
    [Range(100, 500)][SerializeField] int maxHealth;
    

    [Header("Health UI Stuff")]
    [SerializeField] GameObject lowHealthUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Slider healthBar;

    bool isDead = false;


    [Header("Player Damage Taken Stuff")]
    SpriteRenderer spriteRenderer;
    [SerializeField] Color damageColor = Color.red;
    [SerializeField] Color noDamageColor = Color.white;

    Coroutine damageRoutine;

    // Start is  called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            Debug.Log("No sprite renderer found");
            Application.Quit();
        }
        spriteRenderer.color = noDamageColor;


        Reset();
    }



    public void Reset(){
        health = maxHealth;
        healthBar = FindObjectOfType<Slider>();
        healthBar.value = healthBar.maxValue;
        isDead = false;
    }

    public void SetHealth(){
        healthBar.value = health;
    }


    public void Damage(int amount){
        ChangeHealth(-amount);

        if(damageRoutine != null) StopCoroutine(damageRoutine);
        damageRoutine = StartCoroutine(ChangeColor());

        if(health <= 0){
            isDead = true;
            //FindObjectOfType<GameManager>().EndGame();
        }
        UIChecks();
    }

    public void Heal(int amount){
        ChangeHealth(amount);
        UIChecks();
    }

    void UIChecks(){
        if(health <= (maxHealth / 5) && !lowHealthUI.activeSelf){
            lowHealthUI.SetActive(true);
        } 
        if(isDead){
            gameOverUI.SetActive(true);
        }
    }


    void ChangeHealth(int amount){
        health += amount;
        // healthBar.value = health;
        Math.Clamp(health, 0, maxHealth);
        healthBar.value = health;
    }

    IEnumerator ChangeColor(){
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = noDamageColor;
    }
}
