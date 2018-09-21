using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Application.LoadLevel(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 20), "Start"))
            SceneManager.LoadScene(1);
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 65, 100, 20), "Exit"))
            Application.Quit();
    }
}
