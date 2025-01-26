using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] int collisionDamage;
    [SerializeField] float damageCooldown = 1.0f;  // Cooldown time in seconds

    private Transform player;
    private bool canDamage = true;  // Tracks if damage can be applied

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.Log("Player not found");
            Application.Quit();
        }

        player = playerObject.transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.z = 0f;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D victim)
    {
        if (victim.gameObject.CompareTag("Player") && canDamage)
        {
            Debug.Log("Damaging player...");
            PlayerHealthManager playerHealth = victim.gameObject.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.Damage(collisionDamage);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
