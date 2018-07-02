using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int currentLevel = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        LevelManager[] lvlMan = GameObject.FindObjectsOfType<LevelManager>();
        if(lvlMan.Length > 1)
        {
            Destroy(gameObject);

        }
	}

    public void startGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        currentLevel = 0;
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

        if(currentLevel == 11)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        }

    }
}
