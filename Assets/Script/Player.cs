using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    Animator ani;
    public float speed;
    float movement;
    #region Jumping
    bool jump = false;
    public float jumpHeight;

    float jumpDirection;

    bool isGrounded = false;
    public LayerMask groundLayerMask;

    

     int playerLayer = 7;

     int semiSolidLayerMask = 8;

    
    public Vector2 boxSize;

    public float rotation;
    public Color debugColor = Color.blue;

    public Vector3 offset;

    Vector2 boxPosition;
    int airJumps = 0;
    const int maxAirJumps = 1;

    public float maxFallSpeed;
    #endregion 


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();

       ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {
        if(ani.GetCurrentAnimatorStateInfo(0).IsName("Crouch")){
        Debug.Log("True");
        }
        
        boxPosition = transform.position + offset; 
        movement = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapBox(boxPosition, boxSize, rotation, groundLayerMask);
        if(isGrounded == true){
            if(Input.GetAxisRaw("Vertical")< 0){
                ani.SetBool("Crouching", true);
            }
            else{
                ani.SetBool("Crouching", false);
            }
            ResetJumps();
        }
        if(Input.GetAxis("Vertical") < 0){
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }
        if(Input.GetButtonDown("Jump")){
            if(isGrounded == true){
                jump = true;
            }
            else if(isGrounded == false && airJumps > 0){
                jump = true;
                airJumps = airJumps - 1;

            }
            if(rb.velocity.y < 0){
            rb.gravityScale = 2;
        
        }
        else{
            rb.gravityScale = 1;
        }
        if(rb.velocity.y < -maxFallSpeed){
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }

        }
        if(rb.velocity.y > 0){
        // Make sure player layer isn't colliding with the semi solid layer

        Physics2D.IgnoreLayerCollision(playerLayer, semiSolidLayerMask, true);

        
        }
        else{
            Physics2D.IgnoreLayerCollision(playerLayer, semiSolidLayerMask, false);
        }

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        if(jump == true){
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight); 
        }
        
    }
    void ResetJumps(){
        airJumps = maxAirJumps;

    }
    void OnDrawGizmosSelected(){
        Gizmos.color = debugColor;
        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
   
}
