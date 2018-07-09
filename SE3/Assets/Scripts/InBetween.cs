using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InBetween : MonoBehaviour {

    public Text levelnum;
    

    public void StartCountdown()
    {

        StartCoroutine(destroyRoutine());


    }

    public IEnumerator destroyRoutine()
    {

        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);


    }

}
