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

    [Range(0f, 20f)]
    [SerializeField] float cursorDistance, maxCursorDistance;
    [Range(0f, 1f)]
    [SerializeField] float screenPercentage; //controls how tightly one can view their player on screen
    [SerializeField] Vector3 newCameraTarget;
    [SerializeField] float cameraSpeed;
    [SerializeField] Camera main_Camera;
    [SerializeField] float cameraWidth, cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        /*
            Empty object is parent of camera
            Movement should move the empty object
                The mouse position should directly move the camera object directly
        */

        main_Camera = FindObjectOfType<Camera>();
        main_Camera.transform.LookAt(rb.gameObject.transform);
        //For orthographic cameras if calculating manually  
        cameraHeight = Camera.main.orthographicSize * 2f;  // Total height of the camera's view
        cameraWidth = cameraHeight * Camera.main.aspect;   // Total width of the camera's view based on the aspect ratio

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement
        userRight = Input.GetAxis("Horizontal");
        userForward = Input.GetAxis("Vertical");

        //Implement player dash
        if(Input.GetButtonDown("Jump") && dashTimer > 0){
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

         // Get mouse position in world space
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -1f; // zero z for 2D game

        // Calculate new camera target position
        Vector3 playerTransform = gameObject.transform.position;

        // Optionally, ensure the camera position doesn’t move too far from the player in case of large movements
        // Adding the mouse distance condition might still be useful to create a smooth camera-follow effect
        
        float distanceofCursor = Vector3.Distance(main_Camera.transform.position, mouseWorldPos);

        if(distanceofCursor > cursorDistance && distanceofCursor < maxCursorDistance) {
            main_Camera.transform.position = newCameraTarget; // Snap to the target if too far

            newCameraTarget = mouseWorldPos;

            // Clamp the camera position so the player is always in view
            newCameraTarget.x = Mathf.Clamp(newCameraTarget.x, playerTransform.x - cameraWidth * screenPercentage, playerTransform.x + cameraWidth * screenPercentage);
            newCameraTarget.y = Mathf.Clamp(newCameraTarget.y, playerTransform.y - cameraHeight * screenPercentage, playerTransform.y + cameraHeight * screenPercentage);
        }
        else{
            newCameraTarget = playerTransform + new Vector3(0f,0f,-1f);
        }
        // Update camera position using Lerp for smooth movement
        main_Camera.transform.position = Vector3.Lerp(main_Camera.transform.position, newCameraTarget, cameraSpeed * Time.deltaTime);

        
    }

    void FixedUpdate(){
        //Moving the player
        rb.velocity = new Vector2(userRight*playerSpeed, userForward*playerSpeed);
        if(isDashing){
            rb.velocity = dashForce;
        }
    }

    //Should handle collisions with enemy
    
    void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag == "Enemy"){
            //Should take damage if making contact with enemies
            FindObjectOfType<HealthManager>().DamagePlayer(other.gameObject.GetComponent<Enemy>().damage);
            Debug.Log("Hit an enemy accidentally");
        }
    }
    
    
}
