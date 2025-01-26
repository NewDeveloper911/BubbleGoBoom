using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float health;
    [SerializeField] int scoreValue;
    SpriteRenderer spriteRenderer;
    [SerializeField] Color damageColor = Color.red;
    [SerializeField] Color noDamageColor = Color.white;

    [Header("Fields here cos im lazy to test")]
    [SerializeField] Boolean isSuicider;
    [SerializeField] GameObject suicider;
    [SerializeField] ItemDrop itemSpawner;


    Coroutine changeColorCoroutine;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            Debug.Log("No sprite renderer found");
            Application.Quit();
        }
        spriteRenderer.color = noDamageColor;

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(gameManager == null){
            Debug.Log("No game manager found");
            Application.Quit();
        }

        itemSpawner = FindObjectOfType<ItemDrop>();

    }

    // Update is called once per frame
    void Update()
    {
        if(itemSpawner == null) itemSpawner = FindObjectOfType<ItemDrop>();
    }

    public void Damage(float damage){
        health -= damage;

        if(changeColorCoroutine != null) StopCoroutine(changeColorCoroutine);

        changeColorCoroutine = StartCoroutine(ChangeColor());

        if(health <= 0) Death();
    }

    void Death(){

        gameManager.ChangeScore(scoreValue);
        //Here, we can spawn an item and decide which one that is
        GameObject itemToSpawn = itemSpawner.dropItem();
        //Need to instantiate this item where the enemy was
        Instantiate(itemToSpawn, transform.position, Quaternion.identity);

        Destroy(gameObject);

        if(isSuicider){
            int maxAmount = 8;
            for(int i = 0; i < UnityEngine.Random.Range(1, maxAmount + 1); i++){
                Instantiate(suicider, transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0), transform.rotation);
            }
        }
    }

    IEnumerator ChangeColor(){
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = noDamageColor;
    }
}
