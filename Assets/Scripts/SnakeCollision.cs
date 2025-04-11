using UnityEngine;

public class SnakeCollision : MonoBehaviour
{
    private SnakeMovement snakeMovement;

    void Start()
    {
        snakeMovement = GetComponent<SnakeMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Coliziune cu: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
        
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            snakeMovement.Grow();
        }
        else if (collision.CompareTag("Wall") || collision.CompareTag("SnakeSegment"))
        {
            snakeMovement.GameOver();
        }
    }
}
