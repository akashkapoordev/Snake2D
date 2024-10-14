using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public bool shield;
    private PowerUps Item;

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        SnakeController snakeController = other.GetComponent<SnakeController>();
        if (other.CompareTag("Snake"))
        {
            Debug.Log("Power Ups");
            GameObject Powerup = GameObject.Find("PowerUp");
            Item = Powerup.GetComponent<PowerUps>();
            SoundManager.Instance.SoundEffect(Sounds.POWERUPS);
            if (Powerup.activeInHierarchy && shield)
            {
              
                Debug.Log("PowerActivate");
                Item.ActivateShield(snakeController);
                Destroy(this.gameObject);
            }

            else
            {
                Debug.Log("PowerActivate");

                Item.BoostScore(snakeController);
                Destroy(this.gameObject);
            }
        }
    }
}
