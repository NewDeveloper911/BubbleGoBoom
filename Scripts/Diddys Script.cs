using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiddysScript : MonoBehaviour
{
    [SerializeField] float speedDuration = 10f;
    [SerializeField] float timer =0f;
    bool picked_up = false;
    [SerializeField] GameObject player;
    void OnTriggerEnter2D(Collider2D other){
        if (other == player.GetComponent<Collider2D>()){
            picked_up = true;
            print("in");
            player.GetComponent<PlayerMovement>().playerSpeed *= 2;
            this.GetComponent<Transform>().position = new Vector3(0,1000,0);
            }
    }

    void FixedUpdate(){
        if (picked_up){
            timer += Time.deltaTime;
            if (timer> speedDuration){
                player.GetComponent<PlayerMovement>().playerSpeed /= 2;
                Destroy(this.GameObject());}
        }
    }
}
