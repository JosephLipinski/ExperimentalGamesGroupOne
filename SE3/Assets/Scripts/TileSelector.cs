using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public bool isSafe = false;
    public bool isStartingTile;
    public float currentWeight;
    public GameObject goalRow;

    public int color;

    public List<GameObject> adjancentTiles = new List<GameObject>();
    int layerMask = 1 << 9;

    void Awake(){
        // isSafe = false;
        currentWeight = 0;
        SetupBoard();
    }

    public void SetupBoard(){
        if (isStartingTile){
            color = Random.Range(1, 4);
            GameObject.FindObjectOfType<BoardManager>().startingTile = gameObject;
            isSafe = true;
            ShootRayCasts();
            currentWeight += 1;
            int choice = Random.Range(0, adjancentTiles.Count);
            GameObject next = adjancentTiles[choice];
            next.GetComponent<TileSelector>().SelectNextSafe(goalRow);
        }
    }

    public void ClearBoard(){
        GameObject Board = GameObject.Find("Board");
        foreach(Transform child in Board.transform){
            if(child.GetComponent<BoardManager>() == null){
                foreach(Transform grandchild in child.transform){
                    TileSelector _ts = grandchild.GetComponent<TileSelector>();
                    if(_ts != null){
                        _ts.isSafe = false;
                        _ts.color = 0;
                        _ts.currentWeight = 0;
                    }
                }
            }
        }
        
    }

    void SelectNextSafe(GameObject _goalRow){
        //Debug.Log(this.gameObject.name);
        isSafe = true;
        color = Random.Range(1, 4);
        currentWeight += 1;
        if (!this.gameObject.transform.IsChildOf(_goalRow.transform)){
            ShootRayCasts();
            float minWeight = 1000;
            List<GameObject> possible = new List<GameObject>();
            /*
			foreach(GameObject gameObject in adjancentTiles){
				TileSelector _ts = gameObject.GetComponent<TileSelector>();
				if (_ts.currentWeight == minWeight){
					minWeight = _ts.currentWeight;
					possible.Add(gameObject);
				}
				else if(_ts.currentWeight < minWeight){
					possible = new List<GameObject>();
					minWeight = _ts.currentWeight;
					possible.Add(gameObject);
				}
			}
			int choice = Random.Range(0, possible.Count);
			GameObject next = possible[choice];
			next.GetComponent<TileSelector>().SelectNextSafe(_goalRow);
			 */
            int choice = Random.Range(0, adjancentTiles.Count);
            GameObject next = adjancentTiles[choice];
            next.GetComponent<TileSelector>().SelectNextSafe(_goalRow);
        }
        else{
            Debug.Log("Done");
        }
    }
    
    void ShootRayCasts()
    {
        Vector3 start = Vector3.zero;
        Vector3 direction = Vector3.forward;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (!hit.collider.gameObject.GetComponent<TileSelector>().isSafe)
            {
                adjancentTiles.Add(hit.collider.gameObject);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layerMask))
        {
            if (!hit.collider.gameObject.GetComponent<TileSelector>().isSafe)
            {
                adjancentTiles.Add(hit.collider.gameObject);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
        {
            if (!hit.collider.gameObject.GetComponent<TileSelector>().isSafe)
            {
                adjancentTiles.Add(hit.collider.gameObject);
            }
        }
    }
}

