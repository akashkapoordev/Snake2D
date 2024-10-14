using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScore;

    public float playerOneScore;
    public float playerTwoScore;

    public static float highScore;
    public float _score;

    private void Start()
    {
        _score = 0;
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        if (_highScore != null)
        {
            _highScore.text = "Your Score : " + highScore.ToString();
        }
    }

    public void UpdateHighScore()
    {
        if(UIController.multiplayer)
        {
            if(playerOneScore>playerTwoScore)
            {
                _highScore.text = "Player One Wins";
            }
            else
            {
                _highScore.text = "Player Two Wins";
            }
        }

        else
        {
            highScore = _score;
            _highScore.text = "Your HighScore : "+ highScore.ToString();
            PlayerPrefs.SetFloat("HighScore",highScore);
            PlayerPrefs.Save();
        }

    }


    public void UpdatePlayerScore(PlayerType playerType, int score)
    {
        if (playerType == PlayerType.PLAYERONE)
        {
            playerOneScore += score;
        }
        else if (playerType == PlayerType.PLAYERTWO)
        {
            playerTwoScore += score;
        }
    }

       public float GetPlayerScore(PlayerType playerType)
    {
        if (playerType == PlayerType.PLAYERONE)
        {
            return playerOneScore;
        }
        else if (playerType == PlayerType.PLAYERTWO)
        {
            return playerTwoScore;
        }
        return 0;
    }

  public float GetPlayerOneScore()
    {
        return playerOneScore;
    }

    public float GetPlayerTwoScore()
    {
        return playerTwoScore;
    }
    public void RestarTGame()
    {
        SceneManager.LoadScene(1);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
