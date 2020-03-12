using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

    public static PlayerStat instance;

    public int hp;
    public int mp;

    public int atk;
    public int def;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
