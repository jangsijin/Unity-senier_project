using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton: MonoBehaviour {

    // Use this for initialization
    public GameObject menuContainer;
    public int Health = 25;
    public int currentHealth;

    public int Dmg = 2;
    public Slider hpSlider;

    void Start()
    {
        currentHealth = Health;
    }


    public void onClick()
    {
        onDamage();        
    }


    void Update()
    {
        hpSlider.maxValue = 25;
        hpSlider.value = Health;
    }



    public void onDamage()
    {
        Health = Health - Dmg;
        if (Health <= 0)
            onDie();

    }

    public void onDie()
    {
        Debug.Log("고블린이 죽었어요");
        Time.timeScale = 0;
        gameObject.GetComponent<Animator>().Play("GameClearApear");
        menuContainer.SetActive(true);
    }

    public void Press_ClearButton()
    {
        SceneManager.LoadScene("Game");
    }

}
