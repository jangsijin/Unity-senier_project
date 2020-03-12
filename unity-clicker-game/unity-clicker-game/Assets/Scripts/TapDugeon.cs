using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapDugeon : MonoBehaviour
{

    public string DugeonType = "";

    public void onClickButton()
    {
        if (DugeonType == "Dugeon0")
        {
            Time.timeScale = 1;
            Application.LoadLevel("Dungeon");
        }
    }
}
