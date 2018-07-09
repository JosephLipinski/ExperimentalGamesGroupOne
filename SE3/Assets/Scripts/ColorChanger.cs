using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public ParticleSystem[] flames;

    public Material wallMat;

    public int level = 0;

    public float r;
    public float g;
    public float b;

	// Use this for initialization
	void Start () {

        //DontDestroyOnLoad(gameObject);
       // incrementLevel();
	}

    private void Update()
    {
      
    }

    public void incrementLevel()
    {
        level += 1;

        float x = (2 * Mathf.PI * level)/100;

        r = (Mathf.Sin(x + (Mathf.PI / 2)) + 1) / 2;

        g = (Mathf.Sin(x + (Mathf.PI * 3 / 2)) + 1) / 2;

        b = 0;

        if (level >= 40)
        {
            b = (Mathf.Sin(x + Mathf.PI) + 1) / 2;
        }

        Color c = new Color(r, g, b, 1);

        foreach(ParticleSystem p in flames)
        {
            if (p)
            {
                p.startColor = c;
            }

        }

        //wallMat.color = c;
        wallMat.SetColor("_EmissionColor", c);

    }
}
