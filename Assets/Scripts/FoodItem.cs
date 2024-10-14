using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    private PowerUps powerUpsController;
    private ScoreController scoreController;
    public bool isGrowth;
    float foodTimer = 0;
    float destroyTimer = 6f;

    private void Awake()
    {
        
    }

    private void Update()
    {
        foodTimer = foodTimer + Time.deltaTime;

        if(foodTimer > destroyTimer )
        {
            Destroy(this.gameObject);
            foodTimer = 0;
        }
    }
private void OnTriggerEnter2D(Collider2D other)
{
    SnakeController snakeController = other.GetComponent<SnakeController>();
    GameObject powerup = GameObject.Find("PowerUp");
     GameObject score = GameObject.Find("UI");
     scoreController = score.GetComponent<ScoreController>();
    powerUpsController =powerup.GetComponent<PowerUps>();

    if (snakeController != null && isGrowth)
    {
        SoundManager.Instance.SoundEffect(Sounds.SNAKEGROW);
        snakeController.Grow();
        if (powerUpsController.boostActivated)
        {
            scoreController.UpdatePlayerScore(snakeController.playerType, snakeController._score * 2);
        }
        else
        {
            scoreController.UpdatePlayerScore(snakeController.playerType, snakeController._score);
        }

        snakeController.score.text = snakeController.player + scoreController.GetPlayerScore(snakeController.playerType).ToString();

        Destroy(gameObject);
    }
    else
    {
        SoundManager.Instance.SoundEffect(Sounds.SNAKEREDUCE);
        snakeController.Reduce();
        Destroy(gameObject);
    }
}

}
