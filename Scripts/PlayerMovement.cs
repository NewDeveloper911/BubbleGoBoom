using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    [Range(1.0f,400.0f)]
    [SerializeField] float playerSpeed;
    [SerializeField] float userRight, userForward;
    [SerializeField] Rigidbody2D rb;

    [Header("Camera settings")]
    [SerializeField] Camera main_Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement
        userRight = Input.GetAxis("Horizontal");
        userForward = Input.GetAxis("Vertical");
        
    }

    void FixedUpdate(){
        //Moving the player
        rb.AddForce(new Vector2(userRight*playerSpeed*Time.deltaTime, userForward*playerSpeed*Time.deltaTime));
    }

    //Should handle collisions with enemy
    void onCollisionEnter(){

    }
}
