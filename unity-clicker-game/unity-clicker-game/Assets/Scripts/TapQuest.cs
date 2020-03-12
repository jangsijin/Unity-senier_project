using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapQuest : MonoBehaviour
{

    public string QuestType = "";

    public void onClickButton()
    {
        if (QuestType == "SubQuest0")
        {
            Time.timeScale = 1;
            Application.LoadLevel("Flappy");
        }
    }
}
