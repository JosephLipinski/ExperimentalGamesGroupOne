using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    LevelManager _lm;
    Rigidbody _rb;

    public GameObject tallChar;
    public GameObject medChar;
    public GameObject shortchar;

    Vector3 lead;
    Vector3 left;
    Vector3 right;

    public BoardManager level;
    public GameObject respawnPoint;
    Vector3 respawnLocation;

    public float cool = 0.25f;
    public float coolTimer;
    public bool canMove = true;

    public float swapCool = 0.1f;
    public float swapTimer = 0;
    public bool canSwap;

    public float tooLong = 4;
    public float tooLongTimer = 4;

    bool atSpawn = true;


    public int front = 2;

    private void Awake()
    {
        _lm = GameObject.FindObjectOfType<LevelManager>();
        _rb = GetComponent<Rigidbody>();
        respawnLocation = respawnPoint.GetComponent<Transform>().position;
        transform.position = respawnLocation;
        coolTimer = 0;

        left = tallChar.transform.localPosition;
        right = shortchar.transform.localPosition;
        lead = medChar.transform.localPosition;
    }

    void switchCharsLeft()
    {
        if (front == 1)
        {
            front = 2;
            tallChar.transform.localPosition = left;
            medChar.transform.localPosition = lead;
            shortchar.transform.localPosition = right;

        }
        else if (front == 2)
        {

            front = 3;
            tallChar.transform.localPosition = right;
            medChar.transform.localPosition = left;
            shortchar.transform.localPosition = lead;

        }
        else if (front == 3)
        {

            front = 1;
            tallChar.transform.localPosition = lead;
            medChar.transform.localPosition = right;
            shortchar.transform.localPosition = left;
        }

    }

    void switchCharsRight()
    {

        if (front == 1)
        {


            front = 3;
            tallChar.transform.localPosition = right;
            medChar.transform.localPosition = left;
            shortchar.transform.localPosition = lead;

        }
        else if (front == 2)
        {
            front = 1;
            tallChar.transform.localPosition = lead;
            medChar.transform.localPosition = right;
            shortchar.transform.localPosition = left;

        }
        else if (front == 3)
        {

            front = 2;
            tallChar.transform.localPosition = left;
            medChar.transform.localPosition = lead;
            shortchar.transform.localPosition = right;


        }


    }

    IEnumerator Start(){
        while(true){
            if(atSpawn){
                //Debug.Log("HERE");
                if(Input.GetKeyDown(KeyCode.W)){
                    transform.position = new Vector3(level.startingTile.transform.position.x, transform.position.y, level.startingTile.transform.position.z);
                    atSpawn = false;
                }
                
            }
            else{
                if (Input.GetKeyDown(KeyCode.W) && transform.position.z + .98f < 16.5f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.98f);
                }
                else if (Input.GetKeyDown(KeyCode.A) && transform.position.x - .98f > -8.5f)
                {
                    transform.position = new Vector3(transform.position.x - .98f, transform.position.y, transform.position.z);
                }
                else if (Input.GetKeyDown(KeyCode.S) && transform.position.z - .98f > 0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .98f);
                }
                else if (Input.GetKeyDown(KeyCode.D) && transform.position.x + .98f < 8f)
                {
                    transform.position = new Vector3(transform.position.x + .98f, transform.position.y, transform.position.z);
                }

                checkTile();

                level.showTiles(front);

                coolTimer = cool;
            }

            if (!canSwap)
            {
                swapTimer -= Time.deltaTime;
                if (swapTimer <= 0)
                {

                    canSwap = true;

                }

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (canSwap)
                {
                    switchCharsLeft();
                    canSwap = false;
                    swapTimer = swapCool;
                }

            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (canSwap)
                {
                    switchCharsRight();
                    canSwap = false;
                    swapTimer = swapCool;
                }

            }
            
            yield return null;

        }
        yield return null;
    }

    IEnumerator SwapCooldown(){
        yield return new WaitForSeconds(0.1f);
        canSwap = true;
        yield return null;
    }

    void checkTile()
    {
        tooLongTimer = tooLong;
        Ray r = new Ray(transform.position, Vector3.down);

        RaycastHit rHit = new RaycastHit();
        Physics.Raycast(r, out rHit);

        TileSelector tS = rHit.transform.gameObject.GetComponent<TileSelector>();
        if (tS != null && !tS.isSafe){
            Die();
        }
    }

    void Die()
    {
        transform.position = respawnLocation;
        atSpawn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "KillPlane"){
            Die();
        }
        
    }
}
