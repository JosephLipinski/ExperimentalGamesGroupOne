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

    public LevelManager levelman;

    public InBetween inBetween;


	// Use this for initialization
	void Start () {

        levelman = GameObject.FindObjectOfType<LevelManager>();

        mats = new Dictionary<int, UnityEngine.Material>();
        mats.Add(0, none);
        mats.Add(1, first);
        mats.Add(2, second);
        mats.Add(3, third);
        mats.Add(4, dark);

        nextLevel();

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

    void nextLevel()
    {
        levelman.upLevel();
        inBetween.levelnum.text = levelman.currentLevel.ToString();
        inBetween.gameObject.SetActive(true);
        inBetween.StartCountdown();

        //generate new maze
        TileSelector start = startingTile.GetComponent<TileSelector>();

        start.ClearBoard();
        start.SetupBoard();


        showTiles(player.GetComponent<PlayerScript>().front);
        player.gameObject.transform.position = new Vector3(startingTile.transform.position.x, transform.position.y, startingTile.transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("WIN");

            nextLevel();

        }
    }
}
