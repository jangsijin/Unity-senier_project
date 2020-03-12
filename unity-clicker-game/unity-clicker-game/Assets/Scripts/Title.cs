using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//장면의 관리를 참조
public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //해당씬에서 버튼이 눌릴때 작동할 로직을 작성
    public void Press_play()
    {
        //다음씬 장면으로 이동
        SceneManager.LoadScene("Game");
    }
    public void Press_Exit()
    {
        //폰에서만 가능
        Application.Quit();//폰에서 프로그램 작동을중지시킨다.
    }
}
