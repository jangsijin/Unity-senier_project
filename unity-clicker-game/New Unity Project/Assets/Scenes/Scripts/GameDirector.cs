using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public Text timeLabel;
    public float timeCount = 0;

    void Awake()
    {
        timeCount = 5;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (timeCount > 0) timeCount -= Time.deltaTime;
        timeLabel.text = string.Format("{0:N2}", timeCount);
	}
}
