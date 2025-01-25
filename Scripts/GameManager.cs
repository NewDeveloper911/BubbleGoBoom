using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managed scripts")]
    [SerializeField] HealthManager health;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip[] songs;

    [Header("Waves for basic jam")]
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] GameObject[] spawnableEnemies;
    [Range(1f, 10f)]
    [SerializeField] float waveCooldown;
    [SerializeField] float waveTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        audioManager = FindObjectOfType<AudioManager>();
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
        

    }

    void EndGame(){

    }
}
