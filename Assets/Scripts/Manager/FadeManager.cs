using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private FadeController fader;


    public void LobyToJump()
    {
        gameObject.SetActive(true);
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return null;
        fader.FadeIn(1f, () => {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("3_GameScene");
            gameObject.SetActive(false);
        });
    }
}
