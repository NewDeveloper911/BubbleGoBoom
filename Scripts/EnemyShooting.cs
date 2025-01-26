using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    void Start(){
        if(bulletPrefab == null) {
            Debug.Log("No bullet prefab found");
            Application.Quit();
        };
    }

public void Shoot(int amount, float speed, float angle = 69727)
{
    int amountToShoot = amount;
    bool isPointAtPlayer = angle == 69727;
    float bulletSpeed = speed;

    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

    Vector3 directionToPlayer = player.position - transform.position;
    float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    float currentAngle = transform.rotation.eulerAngles.z;

    float angleIncrement = 360f / amountToShoot;

    for (int i = 0; i < amountToShoot; i++)
    {
        if (isPointAtPlayer)
        {
            currentAngle = angleToPlayer;
        }

        if (amountToShoot > 1)
        {
            currentAngle += angleIncrement;
        }

        // Correct bullet rotation
        Quaternion bulletRotation = Quaternion.Euler(0, 0, currentAngle);
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
        
        // Set velocity using Quaternion direction
        Vector2 bulletDirection = bulletRotation * Vector2.up;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }
}

}
