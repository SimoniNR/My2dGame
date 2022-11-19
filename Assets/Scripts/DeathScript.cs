using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [Header("Components")] 
    public GameObject startPoint;
    public GameObject Player;
    
    [Header("Audio")] 
    [SerializeField] private AudioClip[] _clip;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = startPoint.transform.position;
            soundManager.Instance.PlaySound(_clip[0]); //play death audio
            
        }   
    }
}
