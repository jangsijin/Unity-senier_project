using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {

    public GameObject menuContainer;

    GameObject timerText;
    float time = 5.0f;
    private bool isPause = true;

    // Use this for initialization
    void Start () {
        this.timerText = GameObject.Find("Time");
	}
	
	// Update is called once per frame
	void Update () {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        if(this.time <= 0)
        {
            this.time = 0;
            if(this.time == 0)
            {
                Time.timeScale = 0;
                gameObject.GetComponent<Animator>().Play("GameOverApear");
                menuContainer.SetActive(true);
            }
        }
	}

    public void Press_OverButton()
    {
        //다음씬 장면으로 이동
        SceneManager.LoadScene("Game");
    }

}
