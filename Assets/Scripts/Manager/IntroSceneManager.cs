using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    private FadeController fader;

    void Start()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        fader.FadeIn(3f);
        yield return new WaitForSeconds(3f);
        fader.FadeOut(2f, () => {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("1_StartScene");

        });
    }
}
