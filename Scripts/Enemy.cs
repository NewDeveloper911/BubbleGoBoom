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
    [SerializeField] float attackTimer;
    [Range(0f,2f)]
    [SerializeField] float attackCooldown;

    [Header("Attacks")]
    [Range(1, 50)]
    public int damage;
    [SerializeField] GameObject projectile;
    [Range(1,20f)] [SerializeField] float shootforce;
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
        currentHealth = maxhealth;
        attackTimer = attackCooldown;
    }

    public void Damage(int bulletDamage){
        currentHealth -= bulletDamage;
        if(currentHealth < 0) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0) attackTimer -= Time.deltaTime;

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
                    Vector3 bulletDirection = player.transform.position - transform.position;
                    //Instantiate the bullet and force of it

                    //Implementing the cooldown
                    if(attackTimer <= 0){
                        GameObject curBullet = Instantiate(projectile, transform.position, Quaternion.identity);
        
                        //Shoot the bullet
                        Bullet bullet = curBullet.GetComponent<Bullet>();
                        bullet.targetEnemyMask = playerLayerMask;
                        bullet.bulletDamage = damage;
                        curBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection.normalized * shootforce, ForceMode2D.Impulse);

                        attackTimer = attackCooldown;
                    } 

                }
            }
        }
    }
}
