using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    private Animator anim;
    [Header("Movement")]
    public float speed = 10f;
    //public int jumpHeight = 5; //NAOISE JUMP
    public float jumpSpeed = 15f; //new jump
    
    [Header("Component")]
    public Rigidbody2D body;
    public LayerMask groundLayer;
    [SerializeField]private Behaviour[] components;
    
    //private bool grounded;
    private bool facingRight;

    [Header("Physics")] 
    public float gravity = 1;
    public float fallMultiplier = 5;
    public float linearDrag = 4f;
    
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth; 
    public HealthBar healthBar;

    [Header("VFX")]
    public ParticleSystem dust;

    [Header("Collision")]
    public bool onGround = false; //new jump
    public float groundLength = 0.6f; //new jump

    [Header("Audio")] 
    [SerializeField] private AudioClip[] _clip;
    
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
   
    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer); //new jump
       if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
            CreateDust();
        }
        
        if(Input.GetKeyDown(KeyCode.Tab)){
            TakeDamage(20);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        anim.SetTrigger("Hurt");
        soundManager.Instance.PlaySound(_clip[2]); //play hurt audio
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
            GetComponent<characterMovement>().enabled = false;

        }
    }
    void Die()
    {
        Debug.Log("You died!");
        
        anim.SetBool("IsDead", true);// die animation
        soundManager.Instance.PlaySound(_clip[1]); //play die audio
        
        foreach (Behaviour component in components ) 
        {
            component.enabled = false;
        }
        

    }
    
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if (horizontalInput > 0 && facingRight)
        {
            Flip();
            if (onGround)
                CreateDust();
        }
        else if (horizontalInput < 0 && !facingRight)
        {
            Flip();
            if (onGround)
                CreateDust();
        }
        anim.SetBool("Walk", horizontalInput!=0);
        modifyPhysics();
    }
    
    void modifyPhysics()
    {
        if (onGround)
        {
            body.gravityScale = 0;
        }
        else
        { 
            body.gravityScale = gravity;
            body.drag = linearDrag * 0.15f; 
            if (body.velocity.y < 0)
            {
                body.gravityScale = gravity * fallMultiplier;
            }
            else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                body.gravityScale = gravity * (fallMultiplier / 2);
            }
               
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 0); //new jump
        body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);//new jump
        
       
        anim.SetTrigger("Jump");
        soundManager.Instance.PlaySound(_clip[0]); //play jump audio
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //grounded = true;
            onGround = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            //rounded = true;
            onGround = true;
        }

        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = other.transform;
            //grounded = true;
            onGround = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
            anim.SetTrigger("Hurt");
            onGround = true;
        }
            
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //grounded = false;
            onGround = false;
        }
        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = null;
            //grounded = false;
            onGround = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.transform.parent = null;
            //grounded = false;
            onGround = false;
        }
        
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
        
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}