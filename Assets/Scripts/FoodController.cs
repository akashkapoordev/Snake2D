using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    //[SerializeField] private SnakeController controller;
    [SerializeField] private List<Transform> foodList;
    [SerializeField] private Transform massBuner;
    [SerializeField] private ScoreController scoreController;

    float foodTimer = 0;
    float foodSpawnInterval = 5f;

    float massBurnerTimer = 0f;
    float massBurnerSpawnInterval = 10f;




    private void Update()
    {
        foodTimer += Time.deltaTime;
        massBurnerTimer += Time.deltaTime;

        if (foodTimer >= foodSpawnInterval)
        {
            RandomFood();

            foodTimer = 0f;
        }
        if (massBurnerTimer >= massBurnerSpawnInterval &&  (scoreController.GetPlayerOneScore() > 5 || scoreController.GetPlayerTwoScore() > 5))
        { 
            SpawnMassBuner();
            massBurnerTimer = 0f;
        }

    }


    public Vector2 GenerateRandomNumer(float xAxis , float yAxis)
    {
        float x = UnityEngine.Random.Range(-xAxis, xAxis);
        float y = UnityEngine.Random.Range(-yAxis, yAxis);
        return new Vector2(x, y);
    }


    void RandomFood()
    {
        if (foodList.Count > 0)
        {
            int random_food = UnityEngine.Random.Range(0, foodList.Count);
            Debug.Log(random_food);

            Transform food_prefab = Instantiate(foodList[random_food]);
            food_prefab.position = GenerateRandomNumer(7, 4);
        }
    }

    void SpawnMassBuner()
    {
        if (scoreController.GetPlayerOneScore() > 5 || scoreController.GetPlayerTwoScore() > 5)
        {
            float x = UnityEngine.Random.Range(-7, 7);
            float y = UnityEngine.Random.Range(-4, 4);
            Transform massBunner = Instantiate(massBuner);
            massBuner.position = GenerateRandomNumer(7, 4);
        }

    }


}
