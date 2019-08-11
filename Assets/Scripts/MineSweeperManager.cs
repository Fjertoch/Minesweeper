using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineSweeperManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    
    public bool gameOver {
        get;
        protected set;
    }
    // Start is called before the first frame update
    void Start(){
        NewGame();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void NewGame() {
        gameOver = false;
        gameUI.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void ShowGameOverWindow() {
        gameUI.SetActive(false);
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void GameOver() {
        gameOver = true;
        ShowGameOverWindow();
    }

    public void OpenPauseMenu() {
        gameUI.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu() {
        gameUI.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void StartNewGame() {
        SceneManager.LoadScene("MainScene");
    }
}
