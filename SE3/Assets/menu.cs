using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{

    public LevelManager lm;


    private void Start()
    {
        
    }

    public void startGame()

    {
        lm = GameObject.FindObjectOfType<LevelManager>();
        lm.startGame();


    }
}
