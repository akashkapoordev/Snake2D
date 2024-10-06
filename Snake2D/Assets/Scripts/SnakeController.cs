using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xBoundary = 10f;
    [SerializeField] private float yBoundary = 7f;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        StopDiagonalMovement(horizontal, vertical);
        snakeMovement(horizontal, vertical);
        WrapSnake();
    }


    //Snake Movement
    void snakeMovement(float horizontal, float vertical)
    {
        Vector2 snakePosition = transform.position;

        // Horizontal Movement
        snakePosition.x += horizontal * speed * Time.deltaTime;

        // Vertical Movement
        snakePosition.y += vertical * speed * Time.deltaTime;

        // Changing Direction
        if (vertical < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 180);  // Face downward
        }
        else if (vertical > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);  // Face upward
        }
        else if (horizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -90);  // Face right
        }
        else if (horizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 90);  // Face left
        }

        // Apply movement
        transform.position = snakePosition;
    }

    //Stoping Diagonal Movement
    void StopDiagonalMovement(float horizontal,float vertical)
    {

        if (Mathf.Abs(horizontal) > 0)
        {
            vertical = 0;
        }
    }


    void WrapSnake()
    {
        if (transform.position.x > xBoundary )
        {
            transform.position = new Vector2(-xBoundary+1,transform.position.y);
        }
        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector2(xBoundary-1, transform.position.y);
        }
        if (transform.position.y > yBoundary)
        {
            transform.position = new Vector2(transform.position.x, -yBoundary+1);
        }
        if (transform.position.y < -yBoundary)
        {
            transform.position = new Vector2(transform.position.x, yBoundary-1);
        }
    }
}
