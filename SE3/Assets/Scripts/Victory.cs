﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public void exit()
    {
        Application.Quit();
    }

    public void Menu()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
