using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Ranges")]
    [Range(1, 200)]
    [SerializeField] int chaseRange;
    [Range(1, 200)]
    [SerializeField] int attackRange;
    //A value of 6 for this will be great for challenging levels or at least speedy bois
    [Range(1f, 20f)] [SerializeField] float playerChaseSpeed;

    [Header("Attacks")]
    [Range(1, 50)]
    public float damage;
    [SerializeField] GameObject projectile;
    [Range(25, 200)]
    public int maxhealth;
    [SerializeField] int currentHealth;


    [Header("Miscellaneous")]
    [SerializeField] bool isRanged;
    [SerializeField] PolygonCollider2D enemyCollider;
    [SerializeField] LayerMask playerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if we have a target to home in on
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), chaseRange, playerLayerMask);
        if(player != null){
            //Need to get the transform position of the collider which triggered this and start homing in on them
            transform.position = Vector3.Lerp(transform.position, player.transform.position, playerChaseSpeed*Time.deltaTime);
            //Checking if we are close enough to start attacking them
            if(Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), attackRange, playerLayerMask)){
                //Start coding the attacking and damage towards the player
                if(isRanged){
                    //Should shoot a projectile designed to hurt the player, otherwise, the player takes damage
                }
            }
        }
    }
}
