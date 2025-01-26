using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPattern_RegularGoon : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] float speed = 10f;
    EnemyShooting enemyShooting;

    // Start is called before the first frame update
    void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        if(enemyShooting == null){
            Debug.Log("No enemy shooting script found");
            Application.Quit();
        }

        InvokeRepeating("Shoot", 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot(){
        enemyShooting.Shoot(amount, speed, 69727);
    }
}

