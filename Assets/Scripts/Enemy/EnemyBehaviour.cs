using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 1f;
    
    
    private Animator anim;
    
    Rigidbody2D myRigidbody;

    private Enemy isDead;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

  
    void Update()
    {
        if (IsFacingRight())
        {
            // move right
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            anim.SetBool("Walk", true);
            
            
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);//move left
            anim.SetBool("Walk", true);
        }

        
    }
    
    private void OnDisable()
    {
        anim.SetBool("Walk", false);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon; //if its is greater than a very small value
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((!collision.gameObject.CompareTag("Player")) && (!collision.gameObject.CompareTag("EnemySpell")) && (!collision.gameObject.CompareTag("Enemy")))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
        } //turn
        
    }
    
}
