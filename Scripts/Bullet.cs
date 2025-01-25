using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet details")]
    [Range(1f, 3f)]
    [SerializeField] float bulletLifeTime;
    [SerializeField] float lifeRemaining;

    void Start(){
        lifeRemaining = bulletLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeRemaining -= Time.deltaTime;
        if(lifeRemaining <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        Debug.Log("We shot someone, yay!");
        Destroy(gameObject);
    }
}
