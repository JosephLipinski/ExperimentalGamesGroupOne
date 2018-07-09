using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int currentLevel = 0;
    
    public float tileShakeTime;

    private ColorChanger colors;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        tileShakeTime = 12.0f;
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
        if(colors == null)
        {


            colors = GetComponent<ColorChanger>();

        }
        colors.incrementLevel();
        if(currentLevel % 5 == 0 && tileShakeTime > 4){
            tileShakeTime -= 1;
        }



        if(currentLevel == 101)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        }

    }

    public float GetShakeTime(){
        return tileShakeTime;
    }

}
