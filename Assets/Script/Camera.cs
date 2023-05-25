using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour{
    private bool hasPlayer;
    private Transform player;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(!hasPlayer){
            player = UnityEngine.GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if(player.gameObject == null){
                return;
            }else{
                hasPlayer = true;
            }
        }else{
            transform.position = new Vector3(player.transform.position.x - 4, 3.5f, player.transform.position.z - 0.1f);
        }
    }
}
