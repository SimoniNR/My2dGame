using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [Header("Components")] 
    public Animator animator;
    [SerializeField]private Behaviour[] components;
   
    [Header("Health")] 
    public int maxHealth = 100;
    int currentHealth;
    

    [Header("Audio")]
    [SerializeField] private AudioClip[] _clip;


  
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("YOU HIT HIM!");
        currentHealth -= damage;
        animator.SetTrigger("Hurt");// play hurt animation

        if (currentHealth <= 0)
        {
            Die();
            //disable all classes
            foreach (Behaviour component in components ) 
            {
                component.enabled = false;
            }
        }
    }

    void Die()
    {
        Debug.Log("HE IS DEAD!");
        
        animator.SetBool("IsDead", true);// die animation
        soundManager.Instance.PlaySound(_clip[0]); //play melee audio

        
        this.enabled = false; // disable the enemy
       
        GetComponent<Collider2D>().enabled = false; // disable collider
      
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
