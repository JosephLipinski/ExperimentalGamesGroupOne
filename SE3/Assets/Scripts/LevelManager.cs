using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int currentLevel = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    public void startGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {

            Application.Quit();

        }
	}

    public void upLevel()
    {

        currentLevel += 1;

        if(currentLevel == 100)
        {

            //victory screen

        }

    }
}
