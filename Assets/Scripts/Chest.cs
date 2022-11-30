using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Chest : MonoBehaviour
{
    [Header("Components")] 
    public Animator animator;

    public GameManagerScript gameManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
       

        if (other.gameObject.CompareTag("Player"))
        {
            
            animator.SetTrigger("Open");
            
        }
            
    }

    private void complete ()
    {
       gameManager.CompleteLevel();
    }
}
