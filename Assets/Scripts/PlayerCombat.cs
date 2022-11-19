using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    [Header("Meele Attack")]
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    
    [Header("Ranged Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Audio")] 
    [SerializeField] private AudioClip[] _clip;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }  
        }
        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown)
            RangedAttack();
        cooldownTimer += Time.deltaTime;

    }

    private void RangedAttack()
    {
        animator.SetTrigger("SpellCast");
        soundManager.Instance.PlaySound(_clip[0]); //play ranged attack audio
        cooldownTimer = 0;
        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");// play animation
        soundManager.Instance.PlaySound(_clip[1]); //play meele audio
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // detect enemies in range of attack

        foreach (Collider2D enemy in hitEnemies) //damage them
        {
           enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    { 
        if (attackPoint == null)
            return;
       Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    
}
