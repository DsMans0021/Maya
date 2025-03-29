using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
 public GameObject gameOverUI;
 public GameObject Player;

    void Start()
    {
    gameOverUI.SetActive(false);
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }
    }


 public void gameOver()
 {
    gameOverUI.SetActive(true);
 }
 public void restart()
 {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Debug.Log("Restart");
 }

 public void quit()
 {
    Application.Quit();
    Debug.Log("Quit");
 } 

 public void mainMenu()
 {
    SceneManager.LoadScene(0);
    Debug.Log("Main Menu");
 }


}
