using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemisolidPlatform : MonoBehaviour
{
    EdgeCollider2D ec; 
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EdgeCollider2D>(); 
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionExit2D(Collision2D other){
        ec.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            GameObject player = col.gameObject;
            float playerMaxFallSpeed = player.GetComponent<Player>().maxFallSpeed;
           float playerBottomEdge = col.bounds.center.y - col.bounds.extents.y + (Time.deltaTime * playerMaxFallSpeed);  
           if(playerBottomEdge > ec.bounds.center.y){
            ec.isTrigger = false;
           }
            //var = ec.bounds.y - (ec.bounds.y/2);
        }
    }
}
