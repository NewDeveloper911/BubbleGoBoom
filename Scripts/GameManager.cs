using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int gameScore = 1;

    [Header("Managed scripts")]
    [SerializeField] HealthManager health;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip[] songs;



    [Header("Dynamic wave spawning around the player")]
    [SerializeField]    Transform player;

    [SerializeField] int minDist;
    [SerializeField] int maxDist;
    [SerializeField] BoxCollider2D spawnBoundBox;




    [Header("Enemy prefabs")]
    [SerializeField] GameObject Suicider;
    [SerializeField] GameObject RegularGoon;
    [SerializeField] GameObject Shooter;
    [SerializeField] GameObject Mitosis;
    [SerializeField] GameObject KingOlay;




    [Header("General UI")]
    [SerializeField] TMP_Text scoreText;

    public int waves = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreText = FindObjectOfType<TMP_Text>();

        waves = 23;
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave(){
        waves++;
        if(waves % 24 == 0) {
            BossWave();
        }else{
        int noOfEnemies = Mathf.RoundToInt(4 * Mathf.Log10(waves + 8));
        SpawnEmenies(noOfEnemies, waves);
        yield return new WaitForSeconds(10f);
        StartCoroutine(SpawnWave());
        }
    }

    void BossWave(){
        SpawnEmenies(1, waves, true);
    }
    // Update is called once per frame

    void SpawnEmenies(int number, int waves, bool isBoss = false){
        if(isBoss == false){
        int spawned = 0;
        int attempts = 0;

        while (spawned < number){
            attempts++;

            GameObject enemy;
            int minChance = 0;
            int maxChance = 100;

            int randomNum = UnityEngine.Random.Range(minChance, maxChance);


            if(randomNum < 50){
                enemy = RegularGoon;
            }else if(50<= randomNum && randomNum < 75){
                enemy = Shooter;
            } else {
                enemy = Mitosis;
            }


            float angle = UnityEngine.Random.Range(0f, 2f * (float)Math.PI );
            float distance = UnityEngine.Random.Range(minDist, maxDist);

            Vector2 randomPosition = new Vector2(player.position.x + Mathf.Cos(angle) * distance, player.position.y + Mathf.Sin(angle) * distance);

            if (spawnBoundBox.OverlapPoint(randomPosition)){

                Instantiate(enemy, randomPosition, Quaternion.identity);
                spawned++;
            }
            if(attempts > 1000) break;

            }
        } else if(isBoss == true){
            Debug.Log("Spawning boss");
            Vector2 randomPosition = new Vector2(player.position.x + UnityEngine.Random.Range(minDist, maxDist), player.position.y + UnityEngine.Random.Range(minDist, maxDist));
            Instantiate(KingOlay, randomPosition, Quaternion.identity);
        }
    }

    void LateUpdate(){
        scoreText.text = "Score: " + gameScore.ToString();
    }

    void EndGame(){
        // Debug.Log("You lost the game bro, fr");
        scoreText.text = "Final score: " + gameScore.ToString();
        Invoke("ResetGame", 5f); //reset the game after 5 second if there isn't any user input, i guess
    }

    public void ResetGame(){
        health.ResetGame();
        gameScore = 0;
    }

    public void ChangeScore(int amount){
        gameScore += amount;
    }
}
