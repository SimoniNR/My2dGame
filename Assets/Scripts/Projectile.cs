using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime >5) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(25);
        }
        
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScalex = transform.localScale.x;
        if (Mathf.Sign(localScalex) != _direction)
            localScalex = -localScalex; //flip projectile

        transform.localScale = new Vector3(localScalex, transform.localScale.y, transform.localScale.z);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
