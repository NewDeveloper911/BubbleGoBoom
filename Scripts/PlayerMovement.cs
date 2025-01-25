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

    [Header("Camera settings")]
    [SerializeField] GameObject cameraParent;
    [SerializeField] Camera main_Camera;

    // Start is called before the first frame update
    void Start()
    {
        main_Camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get player movement
        userRight = Input.GetAxis("Horizontal");
        userForward = Input.GetAxis("Vertical");
        //Getting the mouse position for camera movement
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -1f; // zero z

        /*
            Empty object is parent of camera
            Movement should move the empty object
                The mouse position should directly move the camera object directly
        */

        //Camera should stay away from the next room to hide the room

        //Moving the empty parent to follow the player's position
        Vector3 playerPos = rb.transform.position;
        playerPos.z = -1f;
        //cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, playerPos, 1.5f);
        //Moving the camera towards the cursor
        main_Camera.transform.position = Vector3.Lerp(main_Camera.transform.position, mouseWorldPos, 1.5f);
    }

    void FixedUpdate(){
        //Moving the player
        rb.velocity = new Vector2(userRight*playerSpeed, userForward*playerSpeed);
    }

    //Should handle collisions with enemy
    void onCollisionEnter(){

    }
}
