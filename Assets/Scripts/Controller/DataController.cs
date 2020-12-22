using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    private string user_name;
    private int score;

    private void Awake()
    {
        //UserReset();
        if (PlayerPrefs.GetInt("isRegister", 0) == 0)
            UIManager.Instance.RegisterButton();
        else
        {
            user_name = PlayerPrefs.GetString("username");
            score = PlayerPrefs.GetInt("score");
        }
    }

    public void UserRegister(string name)
    {
        PlayerPrefs.SetInt("isRegister", 1);
        user_name = name;
        score = 0;
        SetUserName();
        SetScore();
        UpdateDB();
    }

    public void UserReset()
    {
        PlayerPrefs.SetInt("isRegister", 0);
    }

    public void UpdateDB()
    {
        DBManager.Instance.UpdateUserSQL(user_name, score);
    }
    public void SetUserName()
    {
        PlayerPrefs.SetString("username", user_name);
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt("score", score);
    }

    public bool isLogin()
    {
        if (PlayerPrefs.GetInt("isRegister", 0) == 0)
            return false;
        return true;
    }

    public string getUserName()
    {
        return user_name;
    }
    public int getScore()
    {
        return score;
    }
    public void setScore(int score)
    {
        this.score =  score;
        SetScore();
    }
}
