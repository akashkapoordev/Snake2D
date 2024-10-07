using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField] private SnakeController controller;
    [SerializeField] private List<Transform> food;
    [SerializeField] private Transform massBuner;

    float timer = 0;
    float spwanintervalTimer = 5f;

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spwanintervalTimer)
        {
            RandomFood();
          
            timer = 0;
        }
    }


    void RandomFood()
    {
        if (food.Count > 0)
        {
            int random_food = UnityEngine.Random.Range(0, food.Count);
            Debug.Log(random_food);
            float x = UnityEngine.Random.Range(-7, 7);
            float y = UnityEngine.Random.Range(-4, 4);
            Transform food_prefab = Instantiate(food[random_food]);
            food_prefab.position = new Vector2(x, y);
        }
    }

    void MassBuner()
    {
        if (controller.snakeSegments.Count > 5)
        {
            float x = UnityEngine.Random.Range(-7, 7);
            float y = UnityEngine.Random.Range(-4, 4);
            Transform massBunner = Instantiate(massBuner);
            massBuner.position = new Vector2(x, y);
        }

    }

}
