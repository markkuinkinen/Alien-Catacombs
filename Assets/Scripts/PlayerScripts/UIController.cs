using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    GameController gameController;

    public GameObject PauseMenu;
    public Button ResumeButton;
    public Button exitButton;
    public Button QuitButton;

    public string ZeroScore = "000000";
    public Text scoreText;
    public Text playerLevelText;
    public Text currencyAmountText;

    public GameObject LevelMenu;

    public bool isPaused;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        trackLevelandScore();
        PauseGame();
        trackCurrency();
    }

    void trackLevelandScore()
    {
        playerLevelText.text = "Level " + gameController.playerLevel.ToString();

        if (gameController.totalExp < 100)
        {
            scoreText.text = "000000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 1000)
        {
            scoreText.text = "0000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 10000)
        {
            scoreText.text = "000" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 100000)
        {
            scoreText.text = "00" + gameController.totalExp.ToString();
        }
        else if (gameController.totalExp < 1000000)
        {
            scoreText.text = "0" + gameController.totalExp.ToString();
        }
        else
        {
            scoreText.text = gameController.totalExp.ToString();
        }
    }

    void trackCurrency()
    {
        currencyAmountText.text = gameController.currencyAmount.ToString();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseMenu.SetActive(true);
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            PauseMenu.SetActive(false);
            isPaused = false;
        }
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ExitGame()
    {
        //SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        //Application.Quit();
    }

}
