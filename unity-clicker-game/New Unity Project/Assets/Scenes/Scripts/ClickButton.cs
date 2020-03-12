using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton: MonoBehaviour {

    // Use this for initialization

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
    }

}
