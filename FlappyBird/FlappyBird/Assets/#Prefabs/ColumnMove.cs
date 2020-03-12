using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnMove : MonoBehaviour {

    float makeTime;
	// Use this for initialization
	void Start () {
        makeTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - makeTime > 5f)
            Destroy(gameObject);
	}
}
