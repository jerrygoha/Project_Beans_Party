using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChage : MonoBehaviour
{
    public void ToLoby()
    {
        LoadingManager.Instance.LoadScene("2_LobyScene");
    }

}
