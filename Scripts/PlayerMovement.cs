using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    [Range(1.0f,100.0f)]
    public float playerSpeed;
    float userRight, userForward;
    [SerializeField] Rigidbody2D rb;
    Animator anim;
    //bool running = false;

    [Header("Dashing mechanic")]
    [SerializeField] bool isDashing = false;
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
    [SerializeField] float offsetMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        /*
            Empty object is parent of camera
            Movement should move the empty object
                The mouse position should directly move the camera object directly
        */
        anim = GetComponent<Animator>();

        main_Camera = FindObjectOfType<Camera>();
        main_Camera.transform.LookAt(rb.gameObject.transform);
        //For orthographic cameras if calculating manually  
        cameraHeight = Camera.main.orthographicSize * 2f;  // Total height of the camera's view
        cameraWidth = cameraHeight * Camera.main.aspect;   // Total width of the camera's view based on the aspect ratio

        Cursor.lockState = CursorLockMode.Confined;
        dashTimer = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement
        userRight = Input.GetAxis("Horizontal");
        userForward = Input.GetAxis("Vertical");
        anim.SetBool("run", (userRight!=0||userForward!=0));
        if(dashTimer > 0) dashTimer -= Time.deltaTime;

        //Implement player dash
       if(Input.GetButtonDown("Jump") && dashTimer <= 0){
            //We need to get a cooldown and a layer for projectiles
                //Shouldn't avoid damage from projectiles if dashing
            isDashing = true;
            dashForce = rb.velocity * dashPower;
            dashTimer = dashCooldown;
        }
        
        // Calculate new camera target position
        Vector3 playerTransform = gameObject.transform.position;

        // Get mouse position and screen details
        Vector3 mousePos =Input.mousePosition;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        // Treat x and y values as cartesian coordinates
        float x = (mousePos.x - centerX) / (Screen.width / 2);
        //Flipping the sprite to face the direction it's facing
        if(x > 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
        float y = (mousePos.y - centerY) / (Screen.height / 2);

        // Determine offse amount
        Vector3 offset = new Vector3(x, y, 0f) *  offsetMultiplier;
        offset.z = -1f;


        main_Camera.transform.position = newCameraTarget; // Snap to the target if too far

        // Set new target to offset
        newCameraTarget = playerTransform  + offset;

        // Clamp the camera position so the player is always in view
        newCameraTarget.x = Mathf.Clamp(newCameraTarget.x, playerTransform.x - cameraWidth * screenPercentage, playerTransform.x + cameraWidth * screenPercentage);
        newCameraTarget.y = Mathf.Clamp(newCameraTarget.y, playerTransform.y - cameraHeight * screenPercentage, playerTransform.y + cameraHeight * screenPercentage);
      
        // Update camera position using Lerp for smooth movement
        main_Camera.transform.position = Vector3.Lerp(main_Camera.transform.position, newCameraTarget, cameraSpeed * Time.deltaTime);

        
    }

    void FixedUpdate(){
        //Moving the player
        if(isDashing){
            rb.velocity = dashForce;
            isDashing = false;
        }
        else rb.velocity = new Vector2(userRight*playerSpeed, userForward*playerSpeed);
    } 
}
