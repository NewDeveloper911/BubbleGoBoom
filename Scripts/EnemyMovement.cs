using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float collisionDamage;

    
    
    Transform player;

    void Start()
    {
        GameObject palyerObject = GameObject.FindGameObjectWithTag("Player");
        if(palyerObject == null) {
            Debug.Log("Player not found");
            Application.Quit();
        };

        player = palyerObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.z = 0f;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D victim) {
        if(victim.gameObject.tag == "Player") {
            Debug.Log("We hit sumwon");
        }
    }
}
