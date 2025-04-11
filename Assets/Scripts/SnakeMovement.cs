using UnityEngine;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject segmentPrefab;
    public GameObject foodPrefab;
    public float gridSize = 1f;
    public float moveInterval = 0.2f;
    public float minMoveInterval = 0.1f;
    
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    private Vector2 lastDirection;
    private bool canChangeDirection = true;
    private int score = 0;
    private float moveTimer;
    private float currentMoveInterval;
    
    void Start()
    {
        transform.position = new Vector3(-5f, 0f, 0f);
        segments.Add(transform);
        SpawnFood();
        currentMoveInterval = moveInterval;
        moveTimer = currentMoveInterval;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && lastDirection != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && lastDirection != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lastDirection != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastDirection != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        
        if (moveTimer <= 0)
        {
            moveTimer = currentMoveInterval;
            
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            Vector3 newPosition = transform.position + (Vector3)(direction * gridSize);
            transform.position = newPosition;
            lastDirection = direction;
        }
    }

    public void Grow()
    {
        GameObject newSegment = Instantiate(segmentPrefab, segments[segments.Count - 1].position, Quaternion.identity);
        segments.Add(newSegment.transform);
        score += 10;
        
        currentMoveInterval = Mathf.Max(minMoveInterval, moveInterval - (score / 100f) * 0.1f);
        
        Debug.Log($"Scor: {score}, Viteza: {1/currentMoveInterval} mișcări pe secundă");
        SpawnFood();
    }

    void SpawnFood()
    {
        float x = Mathf.Round(Random.Range(-8f, 8f));
        float y = Mathf.Round(Random.Range(-8f, 8f));
        Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        Debug.Log($"Joc terminat! Scor final: {score}");
        Time.timeScale = 0;
    }

    public int GetScore()
    {
        return score;
    }
}

