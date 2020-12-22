using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        UIManager.Instance.PlayButton();
    }

    public void MenuButton()
    {
        UIManager.Instance.MenuButton();
    }

    public void RankingButton()
    {
        UIManager.Instance.RankingButton();
    }
}
