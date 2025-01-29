using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet details")]
    [Range(0f, 3f)]
    [SerializeField] float bulletLifeTime;
    [SerializeField] HealthManager health;
    [SerializeField] float lifeRemaining;

    [SerializeField] bool damagesEnemies;
    public int bulletDamage;
    public LayerMask targetEnemyMask;

    void Start(){
        lifeRemaining = bulletLifeTime;
        health = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health ==  null) health = FindObjectOfType<HealthManager>();
        lifeRemaining -= Time.deltaTime;
        if(lifeRemaining <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D victim) {

        if (victim.CompareTag("Enemy") && damagesEnemies) {
            Destroy(gameObject);
            EnemyHealth healthScript = victim.GetComponent<EnemyHealth>();
            healthScript.Damage(bulletDamage);
        } 

        if(victim.CompareTag("Player") && !damagesEnemies) {
            Destroy(gameObject);
            PlayerHealthManager healthScript = victim.GetComponent<PlayerHealthManager>();
            healthScript.Damage(bulletDamage);
        }
    }

    
}
