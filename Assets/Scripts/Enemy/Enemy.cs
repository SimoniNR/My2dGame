using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    
    
    private EnemyPatrol _enemyPatrol;
    private EnemyMeeleAttack _enemyMeeleAttack;
    
   private void Awake()
   {
       
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        _enemyMeeleAttack = GetComponent<EnemyMeeleAttack>();
    }
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
            //disable enemy movement
            if (_enemyPatrol != null)
               _enemyPatrol.enabled = false;
           //disable enemy attack
           if (_enemyMeeleAttack != null)
               _enemyMeeleAttack.enabled = false;
        }
    }

    void Die()
    {
        Debug.Log("HE IS DEAD!");
        
        animator.SetBool("IsDead", true);// die animation

        
        this.enabled = false; // disable the enemy
       
        //GetComponent<Collider2D>().enabled = false; // disable collider
       

    }

   
}
