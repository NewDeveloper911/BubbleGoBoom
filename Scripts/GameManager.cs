using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managed scripts")]
    [SerializeField] HealthManager health;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip[] songs;

    [Header("Waves for basic jam")]
    public int gameScore = 0;
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] GameObject[] spawnableEnemies;
    [Range(1f, 10f)]
    [SerializeField] float waveCooldown;
    [SerializeField] float waveTimer;

    [Header("General UI")]
    [SerializeField] TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreText = FindObjectOfType<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waveTimer > 0) waveTimer -= Time.deltaTime; //resetting the timer
        if(waveTimer <= 0){
            int randomSpawnIndex = Random.Range(0, spawnpoints.Length); //generating a random spawnpoint
            int randomEnemyIndex = Random.Range(0, spawnableEnemies.Length); //generating a random spawnpoint
            Vector3 enemySpawnPosition = spawnpoints[randomSpawnIndex].position;

            //Spawning the enemy at this position
                //Need a timer for this
            Instantiate(spawnableEnemies[randomEnemyIndex], enemySpawnPosition, Quaternion.identity);
            waveTimer = waveCooldown;
        }
        if(health.amIDead) EndGame();

    }

    void LateUpdate(){
        scoreText.text = "Score: " + gameScore.ToString();
    }

    void EndGame(){
        Debug.Log("You lost the game bro, fr");
        scoreText.text = "Final score: " + gameScore.ToString();
        Invoke("ResetGame", 5f); //reset the game after 5 second if there isn't any user input, i guess
    }

    public void ResetGame(){
        health.ResetGame();
        gameScore = 0;
    }
}
