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

    if (isPointAtPlayer)
    {
        currentAngle = angleToPlayer - 90;  // Correcting Unity's coordinate system
    }
    else
    {
        currentAngle = angle;  // Use provided angle if not targeting player
    }

    // Spread bullets evenly in a full circle
    float angleIncrement = 360f / amountToShoot;

    for (int i = 0; i < amountToShoot; i++)
    {
        float bulletAngle = currentAngle + (angleIncrement * i);
        Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletAngle);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);

        // Apply correct velocity for circular spread effect
        Vector2 bulletDirection = bulletRotation * Vector2.up;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }
}

}

