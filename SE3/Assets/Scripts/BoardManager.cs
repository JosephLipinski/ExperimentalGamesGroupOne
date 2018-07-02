using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject startingTile;

    public GameObject player;

    public Dictionary<int, UnityEngine.Material> mats;

    public Material dark;
    public UnityEngine.Material none;
    public UnityEngine.Material first;
    public UnityEngine.Material second;
    public UnityEngine.Material third;

	// Use this for initialization
	void Start () {

        mats = new Dictionary<int, UnityEngine.Material>();
        mats.Add(0, none);
        mats.Add(1, first);
        mats.Add(2, second);
        mats.Add(3, third);
        mats.Add(4, dark);

		//foreach(TileSelector g in GameObject.FindObjectsOfType<TileSelector>())
  //      {
  //          g.gameObject.GetComponent<MeshRenderer>().material = mats[g.color];

  //      }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showTiles(int front)
    {
        foreach (TileSelector g in GameObject.FindObjectsOfType<TileSelector>())
        {
            if (Vector3.Distance(g.gameObject.transform.position, player.transform.position) < 2)
            {

                if (g.color == front)
                {
                    g.gameObject.GetComponent<MeshRenderer>().material = mats[g.color];
                }
                else
                {
                    g.gameObject.GetComponent<MeshRenderer>().material = mats[0];

                }
            }

            else
            {

                g.gameObject.GetComponent<MeshRenderer>().material = mats[4];

            }

        }




    }
}
