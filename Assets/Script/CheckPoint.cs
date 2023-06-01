using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class CheckPoint : MonoBehaviour{
    // Start is called before the first frame update
    private GameContorller gc;
    private bool isRecycle = false;

    private void OnEnable() {
        isRecycle = false;
    }

    private void Awake() {
        gc = FindObjectOfType<GameContorller>();
    }

    private void OnTriggerEnter (Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("box");
            if(isRecycle){
                return;
            }else{
                Debug.Log("2");
                isRecycle = true;
                gc.Retrieve();
                gc.Recycle(gameObject.transform.parent.gameObject);
            }
        }
    }
}
