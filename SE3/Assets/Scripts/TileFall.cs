using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFall : MonoBehaviour
{

    LevelManager _lm;
    Transform _transform;
    Vector3 initialPosition;
    Quaternion initialRotation;
    Rigidbody _rb;
    float speed = 1.0f;
    float intensity = 0.0f;
    float shakeTime;


    private void Awake(){
        _lm = GameObject.FindObjectOfType<LevelManager>();
        _transform = this.gameObject.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        initialPosition = _transform.position;
        initialRotation = _transform.localRotation;
    }

    IEnumerator Start(){
        
        while(true){
            yield return new WaitForSeconds(0.05f);
            if (_transform.localRotation.z > 0)
            {
                //Debug.Log("if");
                _transform.rotation = Quaternion.AngleAxis(intensity * -1, Vector3.forward);
            }
            else
            {
                //Debug.Log("else");
                _transform.rotation = Quaternion.AngleAxis(intensity, Vector3.forward);
            }
        }
       
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            //Debug.Log("Hit the player");
            StartCoroutine(ShakeTile());
        }
        else if (other.gameObject.tag == "KillPlane"){
            ReplaceTile();
        }
    }


    void ReplaceTile(){
        //Debug.Log("here");
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _transform.position = initialPosition;
        StopCoroutine(ShakeTile());
        intensity = 0.0f;
    }

    IEnumerator ShakeTile(){
        float staticScale = 12.0f;
        shakeTime = _lm.GetShakeTime();
        float increaseBy = staticScale / shakeTime;
        intensity = 0;
        while(shakeTime >= 0.0f){
            yield return new WaitForSeconds(1.0f);
            shakeTime -= 1.0f;
            intensity += increaseBy;
        }
        intensity = 0;
        _transform.rotation = initialRotation;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        yield return null;
    }

}