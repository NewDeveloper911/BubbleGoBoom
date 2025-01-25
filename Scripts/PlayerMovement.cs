using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    [Range(1.0f,50.0f)]
    [SerializeField] float playerSpeed;
    float userRight, userForward;
    [SerializeField] Rigidbody2D rb;

    [Header("Dashing mechanic")]
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    Vector3 dashForce;
    [Range(1f,5f)]
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTimer;
    [SerializeField] LayerMask projectileToAvoid; //all projectiles should be of this layermask - check in onCollisionEnter()

    [Header("Camera settings")]
    [SerializeField] GameObject cameraParent;
    [SerializeField] float cameraSpeed, cameraParentSpeed;
    [SerializeField] Camera main_Camera;
    [SerializeField] int cameraWidth, cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        main_Camera = FindObjectOfType<Camera>();
        main_Camera.transform.LookAt(rb.gameObject.transform);
        cameraHeight = main_Camera.scaledPixelHeight;
        cameraWidth = main_Camera.scaledPixelWidth;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement
        userRight = Input.GetAxis("Horizontal");
        userForward = Input.GetAxis("Vertical");

        //Implement player dash
        if(Input.GetButtonDown("Space") && dashTimer > 0){
            //We need to get a cooldown and a layer for projectiles
                //Shouldn't avoid damage from projectiles if dashing
            isDashing = true;
            dashForce = rb.velocity * dashPower;
            dashTimer -= Time.deltaTime;
        }
        else {
            //resets the cooldown when not dashing
            dashTimer = dashCooldown;
            isDashing = false;
        } 

        //Getting the mouse position for camera movement
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -1f; // zero z
        /*
            Empty object is parent of camera
            Movement should move the empty object
                The mouse position should directly move the camera object directly
        */

        //Camera should stay away from the next room to hide the room

        //Should limit how far away the camera can move from the player

        //Moving the empty parent to follow the player's position
        Vector3 playerPos = rb.transform.position;
        playerPos.z = -1f;
        //With lerping
        cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, playerPos, cameraParentSpeed*Time.deltaTime);

        //Moving the camera towards the cursor
        //With lerping
        main_Camera.transform.position = Vector3.Lerp(main_Camera.transform.position, mouseWorldPos, cameraSpeed*Time.deltaTime);
    }

    void FixedUpdate(){
        //Moving the player
        rb.velocity = new Vector2(userRight*playerSpeed, userForward*playerSpeed);
        if(isDashing){
            rb.velocity = dashForce;
        }
    }

    //Should handle collisions with enemy
    void onCollisionEnter(){

    }
}
