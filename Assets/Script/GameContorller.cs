using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContorller : MonoBehaviour{
    public GameObject []envs;
    public GameObject player;

    private GameObject []enable;
    private int envPoolAmount = 20;
    private Vector3 envInterval = new Vector3(60f, 0f, 0f);
    private Vector3 startPos = new Vector3(0f, 0f, 0f);
    private List<GameObject> envPool = new List<GameObject>();


    // Start is called before the first frame update
    void Start(){
        for(int i = 0 ; i < envPoolAmount ; i++){
            GameObject gobj = Instantiate(envs[UnityEngine.Random.Range(0, envs.Length)]);
            gobj.transform.position = new Vector3(0f, 0f, 0f);
            gobj.SetActive(false);
            envPool.Add(gobj);
        }
        ConstructEnv();
        startPlayer();
        
    }

    private void startPlayer(){
        float x = UnityEngine.Random.Range(-8f, 8f);
        float z = UnityEngine.Random.Range(-8f, 8f);
        Instantiate(player, new Vector3(x, 2f, z), Quaternion.identity);
    }

    private void ConstructEnv(){
        
        for (int i = 0 ; i < 10 ; i++){
            int r = UnityEngine.Random.Range(0, envPool.Count);
            GameObject gobj = envPool[r];
            envPool.RemoveAt(r);
            gobj.transform.position = startPos;
            gobj.SetActive(true);
            startPos += envInterval;
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate() {
        float x = player.transform.position.x;
    }

    public void Recycle(GameObject gb){
        gb.SetActive(false);
        envPool.Add(gb);
    }

    public void Retrieve(){
        int j = UnityEngine.Random.Range(0, envPool.Count);
        GameObject gb = envPool[j];
        envPool.RemoveAt(j);
        gb.transform.position = startPos;
        gb.SetActive(true);
        startPos += envInterval;
        //return gb;
    }
}
