using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum PlayerType{
    PLAYERONE,
    PLAYERTWO
}
public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform segmentPrefab; 
    [SerializeField] private float segmentSpacing = 0.5f; 
    public List<Transform> snakeSegments = new List<Transform>(); 
    private Vector2 direction = Vector2.right;  
    private List<Vector2> previousPositions = new List<Vector2>();
    [SerializeField] private GameObject restartScreen;
    private Canvas Canvas;
    public ScoreController scoreController;
    public PlayerType playerType;
    public TextMeshProUGUI score;
    public string player;
    public int _score;

    private void Start()
    { 
        if(UIController.multiplayer)
        {
            if(playerType== PlayerType.PLAYERONE)
        {
            player = "Player 1 Score : ";
        }
        else
        {
            player = "Player 2 Score : ";
        }
        }
        else
        {
            player = "Your Score : ";
        }


        

        snakeSegments.Add(this.transform);
        previousPositions.Add(transform.position);
        GameObject CanvasGameObject = GameObject.Find("Canvas");
        Canvas = CanvasGameObject.GetComponent<Canvas>();
        GameObject UI = GameObject.Find("UI");
        scoreController = UI.GetComponent<ScoreController>();
        Canvas.sortingOrder = 0;
        score.text = player + scoreController.GetPlayerScore(playerType).ToString();
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
        //SoundManager.Instance.SoundEffect(Sounds.SNAKEMOVE);


        transform.position = newPosition;

        previousPositions.Insert(0, newPosition);

        if (previousPositions.Count > (snakeSegments.Count * segmentSpacing / Time.deltaTime))
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }


    void SnakeDirection()
    {
if (playerType == PlayerType.PLAYERONE)
{
    // Handle movement for Player One (WASD keys)
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
else if (playerType == PlayerType.PLAYERTWO)
{
    // Handle movement for Player Two (Arrow keys)
    if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
    {
        direction = Vector2.up;
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
    {
        direction = Vector2.down;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
    {
        direction = Vector2.left;
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
    {
        direction = Vector2.right;
    }
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
    _score++;
    Transform segment = Instantiate(segmentPrefab);
    segment.position = snakeSegments[snakeSegments.Count - 1].position;
    if(snakeSegments.Count >= 5)
    {
        segment.AddComponent<CircleCollider2D>();
    }
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("SnakeBody"))
        {
            SoundManager.Instance.SoundEffect(Sounds.DIE);
              scoreController.UpdateHighScore();
           GameObject canvas = GameObject.Find("Canvas");
            restartScreen = canvas.GetComponentInChildren<Transform>().Find("Restart").gameObject;
            restartScreen.SetActive(true);
           this.gameObject.SetActive(false);
           Canvas.sortingOrder = 1;

        }
    }



 
}
