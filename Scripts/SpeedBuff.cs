using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    [SerializeField] float speedDuration = 10f;
    [SerializeField] float timer =0f;
    bool picked_up = false;
    [SerializeField] GameObject player;
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            picked_up = true;
            print("in");
            FindObjectOfType<PlayerMovement>().playerSpeed *= 2;
            GetComponent<Transform>().position = new Vector3(0,1000,0);
            }
    }

    void Update(){
        if (picked_up){
            timer += Time.deltaTime;
            if (timer> speedDuration){
                player.GetComponent<PlayerMovement>().playerSpeed /= 2;
                Destroy(this.GameObject());}
        }
    }
}
