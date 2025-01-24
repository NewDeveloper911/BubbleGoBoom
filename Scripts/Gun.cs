using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun variables")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    //could do bulletSpeed here too
    [Range(5,50)]
    [SerializeField] int bulletRate;
    [SerializeField] float bulletTimer; 

    [Header("Aiming, i guess")]
    [SerializeField] Gameobject bulletPrefab;
    [SerializeField] Transform playerTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletTimer > 0) bulletTimer -= Time.deltaTime;
        //Checking if the user has attempted to fire
        if(Input.GetButtonDown("Fire1")){
            bulletTimer = 1.0f / bulletRate; //resetting the firing timer
            //Creating a bullet
            Instantiate(bulletPrefab, playerTransform, Quaternion.identity);
        }
    }
}
