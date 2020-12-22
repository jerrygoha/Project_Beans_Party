using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    protected static LoadingManager instance;
    public static LoadingManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }


    [SerializeField]
    private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField]
    private Image _progressBar;

    private string loadSceneName;

    public static LoadingManager Create()
    {
        var LoadingManagerPrefab = Resources.Load<LoadingManager>("SceneLoader");
        return Instantiate(LoadingManagerPrefab);
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

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));

            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    } 

    private IEnumerator Load(string sceneName)
    {
        _progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        while(!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if(op.progress < 0.9f)
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, op.progress, timer);
                if(_progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount,1f, timer);

                if(_progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}