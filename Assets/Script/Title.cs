using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnPressdPlayButton()
    {
        Main.Instance.ChangeScene(false);

        GameManager.Instance.Init();
    }
}
