using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] int doorDirection; // 0 = up, 1 = right, 2 = down, 3 = left
    private static float lastTriggerTime = 0f;
    private static float cooldownDuration = 1f; // 1 second cooldown
    private ProceduralGeneration generator;

    private void Start()
    {
        generator = FindObjectOfType<ProceduralGeneration>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time > lastTriggerTime + cooldownDuration)
        {
            lastTriggerTime = Time.time;
            generator.GenerateNextRoom(doorDirection, transform.position);
            gameObject.GetComponent<Collider2D>().enabled = false;       
        }
    }
}

