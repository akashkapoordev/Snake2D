using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform segmentPrefab; 
    [SerializeField] private float segmentSpacing = 0.5f; 
    public List<Transform> snakeSegments = new List<Transform>(); 
    private Vector2 direction = Vector2.right;  
    private List<Vector2> previousPositions = new List<Vector2>();  

    private void Start()
    { 
        snakeSegments.Add(this.transform);
        previousPositions.Add(transform.position);
    }

    private void Update()
    {
        SnakeDirection();
        WrapSnake(transform);
        MoveHead();
    }

    private void FixedUpdate()
    {
        MoveSegments();
    }

    private void MoveHead()
    {
        Vector2 newPosition = new Vector2(
            transform.position.x + direction.x * speed * Time.deltaTime,
            transform.position.y + direction.y * speed * Time.deltaTime
        );

        transform.position = newPosition;

        previousPositions.Insert(0, newPosition);

        if (previousPositions.Count > (snakeSegments.Count * segmentSpacing / Time.deltaTime))
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }


    void SnakeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }
    private void MoveSegments()
    {
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Transform segment = snakeSegments[i];
            segment.position = previousPositions[Mathf.Min(i * (int)(segmentSpacing / Time.deltaTime), previousPositions.Count - 1)];
            WrapSnake(segment);
        }
    }


    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    public void Reduce()
    {
        Transform lastSegment = snakeSegments[snakeSegments.Count - 1];
        snakeSegments.RemoveAt(snakeSegments.Count-1);
        Destroy(lastSegment.gameObject);

    }

    void WrapSnake(Transform snakePart)
    {
        float xBoundary = 10f;
        float yBoundary = 7f;

        if (snakePart.position.x > xBoundary)
        {
            snakePart.position = new Vector2(-xBoundary + 1, snakePart.position.y);
        }
        else if (snakePart.position.x < -xBoundary)
        {
            snakePart.position = new Vector2(xBoundary - 1, snakePart.position.y);
        }

        if (snakePart.position.y > yBoundary)
        {
            snakePart.position = new Vector2(snakePart.position.x, -yBoundary + 1);
        }
        else if (snakePart.position.y < -yBoundary)
        {
            snakePart.position = new Vector2(snakePart.position.x, yBoundary - 1);
        }
    }
}
