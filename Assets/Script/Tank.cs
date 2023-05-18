using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour{
    private Rigidbody rb;
    private Vector3 speed;
    private float xSpeed = 15f; //前後
    private float zSpeed = 8f;  //左右
    private float accelerated = 20f;
    private float decelerated = 10f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        speed = new Vector3(xSpeed, 0f, 0f);
    }

    private void FixedUpdate() {
        //rb.MovePosition(rb.transform.position + speed * Time.fixedDeltaTime);
    }
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)){

        }
    }

    private void MoveLeft(){

    }

    private void MoveStraight(){

    }
}
