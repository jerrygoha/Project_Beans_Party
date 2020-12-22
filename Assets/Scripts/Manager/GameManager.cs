using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController Player;
    public GameObject GameOver;
    public GameObject TileMap;
    public GameObject ReGame;
    bool end = true;

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    private int score = 0;
    public bool isGameover { get; private set; }
    bool bPaused;

    private void Update()
    {
        if (Player.isDead&&end)
        {
            Debug.Log("End");
            if (Player.getScore() > DataController.instance.getScore())
            {
                DataController.instance.setScore(Player.getScore());
                DBManager.Instance.UpdateUserSQL(DataController.instance.getUserName(), DataController.instance.getScore());
            }
            EndGame();
            end = false;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            bPaused = true;
            PauseGame();
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    public void EndGame()
    {
        
         GameOver.SetActive(true);
        TileMap.SetActive(false);
        PauseGame();
    }

    public void ReTryGame()
    {
        GameOver.SetActive(false);
        ReGame.SetActive(true);
    }

    public void RestartGame()
    {
        ContinueGame();
        Player.isDead = false;
        SceneManager.LoadScene(3);
    }
    public int GetSceneIndex()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }
    public void BackToLoby()
    {
        ContinueGame();
        SceneManager.LoadScene(2);
    }
}
