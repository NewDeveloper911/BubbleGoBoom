using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] int doorDirection; // 0 = up, 1 = right, 2 = down, 3 = left
    private ProceduralGeneration generator;

    private void Start()
    {
        generator = FindObjectOfType<ProceduralGeneration>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generator.GenerateNextRoom(doorDirection, transform.position);
            Destroy(gameObject); // Remove trigger so it doesn't spawn again
        }
    }
}

