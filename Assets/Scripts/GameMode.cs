using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMode : MonoBehaviour
{


    [SerializeField] private GameObject greenSnakePrefab;  
    [SerializeField] private GameObject yellowSnakePrefab;
    
    [SerializeField] private GameObject playerOnePrefab;
    [SerializeField] private GameObject playerTwoPrefab;

    public Transform canvasParent;

    void Start()
    {
        if(UIController.multiplayer)
        {
            SetupMultiplayer();
        }
        else
        {
            SinglePlayer();
        }

    }

   public void SetupMultiplayer()
    {
      
            // Instantiate Player 1 (green snake)
            GameObject playerOne = Instantiate(greenSnakePrefab, new Vector3(-2, 0, 0), Quaternion.identity);
             playerOne.GetComponent<SnakeController>().playerType = PlayerType.PLAYERONE;

             //score
             //GameObject playerOneScore = Instantiate(playerOnePrefab,canvasParent);

      

            GameObject playerTwo = Instantiate(yellowSnakePrefab, new Vector3(2, 0, 0), Quaternion.identity);
            playerTwo.GetComponent<SnakeController>().playerType = PlayerType.PLAYERTWO;

             //GameObject playerTwoScore = Instantiate(playerTwoPrefab,canvasParent);




        
        
    }

    public void SinglePlayer()
    {
        GameObject playerOne = Instantiate(greenSnakePrefab, new Vector3(-2, 0, 0), Quaternion.identity);
    
        playerOne.GetComponent<SnakeController>().playerType = PlayerType.PLAYERONE;
        Debug.Log(playerOne.transform.position);

         //GameObject playerOneScore = Instantiate(playerOnePrefab,canvasParent);
    }
}
