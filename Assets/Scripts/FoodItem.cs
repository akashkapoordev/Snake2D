using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public bool isGrowth = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SnakeController snakeController = other.GetComponent<SnakeController>();
        if (other.gameObject.name == "Snake")
        {
            if (snakeController != null && isGrowth)
            {
                snakeController.Grow();
                Destroy(gameObject);
            }
            else
            {
                snakeController.Reduce();
                Destroy(gameObject);
            }
        }

    }
}
