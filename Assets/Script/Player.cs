using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    private Rigidbody rb;
    private Vector3 speed;
    public float xSpeed = 15f; //前後
    public float zSpeed = 8f;  //左右
    private float accelerated = 20f;
    private float decelerated = 10f;
    private bool isDead = false;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
        speed = new Vector3(xSpeed, 0f, 0f);
        isDead = false;
    }

    private void FixedUpdate(){
        rb.MovePosition(rb.transform.position + speed * Time.fixedDeltaTime);
    }
    // Start is called before the first frame update
    void Start(){

    }

    private void OnTriggerEnter(Collider other) {    
        if(other.CompareTag("CollisionFree") || isDead){
            return;
        }else{
            isDead = true;
            rb.GetComponent<Collider>().isTrigger = false;
            speed = Vector3.zero;
            rb.GetComponent<Rigidbody>().isKinematic = false;
            rb.GetComponent<Rigidbody>().useGravity = true;
            GameObject tank = rb.transform.Find("TankFree_Yel").gameObject;
            tank.GetComponent<Collider>().isTrigger = true;
            rb.AddForce(new(0f, 15f, 0f), ForceMode.Impulse);
            rb.AddTorque(new(45f, 45f, 45f), ForceMode.Impulse);
        }
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)){
            MoveStraight();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)){
            MoveStraight();
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            MoveFast();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)){
            MoveNormal();
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
            MoveSlow();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)){
            MoveNormal();
        }
    }

    private void MoveRight(){
        speed = new Vector3(zSpeed, 0, -zSpeed);
    }

    private void MoveLeft(){
        speed = new Vector3(zSpeed, 0, zSpeed);
    }

    private void MoveFast(){
        speed = new Vector3(accelerated, 0, 0);
    }

    private void MoveSlow(){
        speed = new Vector3(zSpeed, 0, 0);
    }

    private void MoveNormal(){
        speed = new Vector3(xSpeed, 0, 0);
    }

    private void MoveStraight(){
        speed = new Vector3(xSpeed, 0, 0);
    }
    
}
