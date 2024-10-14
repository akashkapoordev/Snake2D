using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    float sheildTimer = 0;
    float disableSheildTimer = 5;
    float boostTimer = 0;
    float disableBoostTimer = 10;
    bool sheildActivated = false;
   public  bool boostActivated = false;
    [SerializeField] private List<Transform> powerUps;
    //[SerializeField] private SnakeController controller;
    [SerializeField] private FoodController foodController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private Image shield;
    [SerializeField] private Image boost;
    private SnakeController snakeController;

    float powerUpTimer = 0f;
    float powerUpSpawnInterval;
    float minPowerUpSpawnInterval = 5f;
    float maxPowerUpSpwanInterval = 15f;

    private void Start()
    {
        powerUpTimer = Random.Range(minPowerUpSpawnInterval, maxPowerUpSpwanInterval);
    }

    private void Update()
    {
        powerUpTimer += Time.deltaTime;
        if (powerUpTimer >= powerUpSpawnInterval)
        {
            SpawnPowerUp();
            powerUpTimer = 0f;
            powerUpSpawnInterval = Random.Range(minPowerUpSpawnInterval, maxPowerUpSpwanInterval);
        }

        if(sheildActivated)
        {
            Debug.Log("Shield Deactivated bool");
            sheildTimer += Time.deltaTime;
            if(sheildTimer>disableSheildTimer)
            {
                Debug.Log("Sheild Deactiavted");
                shield.color = new Color(1f, 1f, 1f, 60f / 255f); 
                DeactivateShield();
            }
        }

        if(boostActivated)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer>disableBoostTimer)
            {
                boost.color = new Color(1f, 1f, 1f, 60f / 255f);
                DisableBoostScore();
            }
        }
    }


    void SpawnPowerUp()
    {
        if(scoreController.GetPlayerOneScore() > 5 || scoreController.GetPlayerTwoScore() > 5)
        {
            Debug.Log("Power Ups");
            int random_powerup = UnityEngine.Random.Range(0, powerUps.Count);
            float x = UnityEngine.Random.Range(-7, 7);
            float y = UnityEngine.Random.Range(-4, 4);
            Transform powerUp = Instantiate(powerUps[random_powerup]);
            powerUp.position = foodController.GenerateRandomNumer(7, 4);
        }
        
        
    }

    public void ActivateShield(SnakeController controller)
    {
        snakeController = controller;
        shield.color = new Color(1f, 1f, 1f, 1f);

        if(scoreController.GetPlayerScore(controller.playerType) >= 0)
        {
            sheildActivated = true;
            sheildTimer = 0f;
        }
    }


    public void DeactivateShield()
    {
      

        if (scoreController.GetPlayerScore(snakeController.playerType) >= 0)
        {
           
            sheildActivated = false;
        }
    }


    public void BoostScore(SnakeController controller)
    {
        snakeController = controller;
        boost.color = new Color(1f, 1f, 1f, 1f);
        boostActivated = true;
        boostTimer = 0f;
    }

    public void DisableBoostScore()
    {

        snakeController.scoreController.UpdatePlayerScore(snakeController.playerType,snakeController._score);
        boostActivated = false;
    }


}
