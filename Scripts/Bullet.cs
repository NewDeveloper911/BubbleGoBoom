using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet details")]
    [Range(1f, 3f)]
    [SerializeField] float bulletLifeTime;
    [SerializeField] HealthManager health;
    [SerializeField] float lifeRemaining;
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

    void OnCollisionEnter2D(Collision2D other){
        if(((1 << other.gameObject.layer) & targetEnemyMask) != 0){
            Debug.Log(other.collider.name);
            //Need to get their relevant script and damage them
            var enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                //We hit what we wanted to so let's damage them
                Debug.Log("We shot someone, yay!");
                //if they are an enemy, then let's damage them as one
                enemy.Damage(bulletDamage);
            }
            else{
                //else, they should be a player, so make them pay
                Debug.Log("Enemy is shooting us rn fr");
                health.DamagePlayer(bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}
