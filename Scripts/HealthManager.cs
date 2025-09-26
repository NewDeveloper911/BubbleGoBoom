using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Player health values")]
    [SerializeField] int playerHealth;
    [Range(100, 300)][SerializeField] int maxPlayerHealth;
    public bool amIDead;

    [Header("Health UI")]
    [SerializeField] GameManager game;
    //[SerializeField] GameObject lowHealthUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Slider healthBar;

    [Header("Player Damage Taken Stuff")]
    SpriteRenderer spriteRenderer;
    [SerializeField] Color damageColor = Color.red;
    [SerializeField] Color noDamageColor = Color.white;

    Coroutine damageRoutine;

    public void ResetGame()
    {
        //lowHealthUI.SetActive(false);
        gameOverUI.SetActive(false);
        playerHealth = maxPlayerHealth;
        healthBar.value = healthBar.maxValue;
        amIDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerHealth = maxPlayerHealth;
        //Setting the default value of the healthbar
        healthBar.maxValue = maxPlayerHealth;
        healthBar.value = maxPlayerHealth;
        spriteRenderer.color = noDamageColor;
        ResetGame();
    }

    public void DamagePlayer(int damage)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damage;
            //I want a flashing indicator on screen to communicate to user that they have been hit
            if (damageRoutine != null) StopCoroutine(damageRoutine);
            damageRoutine = StartCoroutine(ChangeColor());
        }
        else
        {
            //They have run out of health and should be dead
            amIDead = true;
            //Tell the gameManager to reset the game now, bruh
            game.EndGame(); //Resetting everything else
        }
        healthBar.value = playerHealth;
        UIChecks();
    }

    public void HealPlayer(int manna)
    {
        if (playerHealth < (maxPlayerHealth - manna)) playerHealth += manna;
        UIChecks();
    }

    void UIChecks()
    {
        //if (playerHealth <= (maxPlayerHealth / 5) && !lowHealthUI.activeSelf) lowHealthUI.SetActive(true);
        if (amIDead) gameOverUI.SetActive(true);

    }
    
    IEnumerator ChangeColor(){
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = noDamageColor;
    }
}
