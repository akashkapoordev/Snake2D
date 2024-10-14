using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    // private static UIController instance;
    // public static UIController Instance {get{return instance;}}
    [SerializeField] private Button playButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button quit;

    [SerializeField] private TextMeshProUGUI highScoreText;
    public static bool multiplayer;



    private void Awake()
    {

        playButton.onClick.AddListener(PlayButton);
        multiplayerButton.onClick.AddListener(MultiplayerButton);
        quit.onClick.AddListener(QuitButton);
    }


    private void Start()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);  

        if (highScoreText != null)
        {
            highScoreText.text = "High Score : " + highScore.ToString();
        }
    }


    void PlayButton()
    {
        SoundManager.Instance.SoundEffect(Sounds.BUTTONSOUND);
        multiplayer = false;
        SceneManager.LoadScene(1);
    }

    void MultiplayerButton() {
        SoundManager.Instance.SoundEffect(Sounds.BUTTONSOUND);
        multiplayer = true;
        SceneManager.LoadScene(1);

     }


    void QuitButton()
    {
        SoundManager.Instance.SoundEffect(Sounds.BUTTONSOUND);

        Application.Quit();
    }
}
