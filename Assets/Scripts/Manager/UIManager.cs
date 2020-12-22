using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;     //C#의 데이터 테이블 때문에 사용

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private InputField inputName;
    private int UI_level = 0;

    public GameObject Background;
    public GameObject Fade;
    public FadeController fader;
    public GameObject Menu;
    public GameObject Exit;
    public GameObject Pause;
    public GameObject Copy;
    public GameObject Register;
    public GameObject RegisterErrorMsg;
    public GameObject Ranking;

    protected static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<UIManager>();
                if (obj != null)
                {
                    instance = obj;
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeButton();
            }
        }
    }

    public void EscapeButton()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                switch (UI_level)
                {
                    case 0: ExitButton(); break;
                    case 1: UIReset(); if (!DataController.instance.isLogin()) RegisterButton(); break;
                    case 2: ExitButton(); break;
                }
                break;
            case 2:
                switch (UI_level)
                {
                    case 0: ExitButton(); break;
                    case 1: UIReset(); break;
                    case 2: UIReset(); break;
                    case 3: UIReset(); MenuButton(); break;
                }
                break;
            case 3:
                switch (UI_level)
                {
                    case 0: PasueButton(); break;
                    case 1: UIReset();
                        GameManager.instance.ContinueGame(); break;
                }
                break;
        }
       
    }

    public void PlayButton()
    {
        Fade.SetActive(true);
        StartCoroutine(Activate());
    }
    IEnumerator Activate()
    {
        yield return null;
        fader.FadeIn(1f, () => {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("3_GameScene");
            Fade.SetActive(false);
        });
    }
    public void ExitButton()
    {
        UI_level = 1;
        Background.SetActive(true);
        Exit.SetActive(true);
    }
    public void MenuButton()
    {
        UI_level = 2;
        Background.SetActive(true);
        Menu.SetActive(true);
    }
    public void PasueButton()
    {
        UI_level = 1;
        Background.SetActive(true);
        Pause.SetActive(true);
        GameManager.instance.PauseGame();
    }

    public void CopyButton()
    {
        UI_level = 3;
        Menu.SetActive(false);
        Copy.SetActive(true);
    }

    public void RegisterButton()
    {
        UI_level = 2;
        Background.SetActive(true);
        Register.SetActive(true);
    }


    public void RankingButton()
    {
        UI_level = 2;
        UpdateRanking();
        Background.SetActive(true);
        Ranking.SetActive(true);

    }

    public void UIReset()
    {
        UI_level = 0;
        Background.SetActive(false);
        Menu.SetActive(false);
        Exit.SetActive(false);
        Pause.SetActive(false);
        Copy.SetActive(false);
        Register.SetActive(false);
        Ranking.SetActive(false);
    }
    public void UserRegister()
    {
        string user_name = inputName.text;
        if (DBManager.Instance.IdCheckSQL(user_name))
        {
            DataController.instance.UserRegister(user_name);
            UIReset();
        }
        else
        {
             RegisterErrorMsg.SetActive(true);
        }
    }

    private void UpdateRanking()
    {
        for(int i = 0; i < 10; i++)
        {
            Text name = Ranking.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<Text>();
            Text score = Ranking.transform.GetChild(2).GetChild(i).GetChild(1).GetComponent<Text>();

            DataTable dt = DBManager.Instance.GetRankingSQL();

            name.text = Convert.ToString(dt.Rows[i][0]);
            score.text = Convert.ToString(dt.Rows[i][1]) + "점";
        }

        Text myName = Ranking.transform.GetChild(2).GetChild(10).GetChild(0).GetComponent<Text>();
        Text myRanking = Ranking.transform.GetChild(2).GetChild(10).GetChild(1).GetComponent<Text>();
        Text myScore = Ranking.transform.GetChild(2).GetChild(10).GetChild(2).GetComponent<Text>();

        myName.text = DataController.instance.getUserName();
        myScore.text = Convert.ToString(DataController.instance.getScore()) + "점";
        myRanking.text = DBManager.Instance.GetRankingSQL(myName.text) + "등";
    }

    public void ExitGame()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
