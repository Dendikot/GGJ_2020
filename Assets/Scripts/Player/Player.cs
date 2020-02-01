using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public
    public Vector2 moveInput;
    public bool interactKey = false;
    
    //Check for tag Machine

    //Private
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float gravityScale = 25;

    private Rigidbody2D rigidBody;

    private bool stairsKey = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D> ( );
        rigidBody.gravityScale = gravityScale;
    }
    
    //FixedUpdate - check how you customise it
    private void FixedUpdate ( )
    {
        movement ( );

    }

    private void Update ( )
    {

        if ( Input.GetKeyDown ( KeyCode.LeftArrow ) )
        {
            moveInput = new Vector2 ( -1 , 0 );
        }
        if ( Input.GetKeyUp ( KeyCode.LeftArrow ) )
        {
            moveInput = default;
        }

        if ( Input.GetKeyDown ( KeyCode.RightArrow ) )
        {
            moveInput = new Vector2 ( 1 , 0 );
        }
        if ( Input.GetKeyUp ( KeyCode.RightArrow ) )
        {
            moveInput = default;
        }

        if ( Input.GetKeyDown ( KeyCode.UpArrow ) && stairsKey)
        {
            moveInput = new Vector2 ( 0 , 1 );
        }
        if ( Input.GetKeyUp ( KeyCode.UpArrow ) )
        {
            moveInput = default;
        }

        if ( Input.GetKeyDown ( KeyCode.DownArrow ) && stairsKey )
        {
            moveInput = new Vector2 ( 0 , -1 );
        }
        if ( Input.GetKeyUp ( KeyCode.DownArrow ) )
        {
            moveInput = default;
        }
    }

    private void movement()
    {
        if ( interactKey )
        {
            rigidBody.velocity = new Vector2 ( 0 , rigidBody.velocity.y );
        }
        else
        {
            rigidBody.velocity = moveInput * speed;
        }
    }

    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if ( collision.tag == "Stairs")
        {
            stairsKey = true;
            rigidBody.gravityScale = 0;
        }
        //On trigger enter disables gravity and allows to move up and down 
    }

    private void OnTriggerExit2D ( Collider2D collision )
    {
        if ( collision.tag == "Stairs")
        {
            stairsKey = false;
            rigidBody.gravityScale = gravityScale;
        }
    }

    //private void simpleJump()
    //{
    //    if ( grounded )
    //    {
    //        rigidBody.velocity += Vector2.up * 50f;
    //        grounded = false;
    //    }
    //}
}
