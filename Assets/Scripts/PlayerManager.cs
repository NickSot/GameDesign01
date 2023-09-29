using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //needed for movement
[RequireComponent(typeof(CircleCollider2D))] //needed for the rigidbody
[RequireComponent(typeof(SpriteRenderer))] //needed for visuals

[RequireComponent(typeof(Gravity))]


public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D _rigidBody; 
    private BodyPartManager _partManager;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public enum PlayerState {
    IdleRoll,
    IdleStanding,
    Rolling,
    Walking,
    Jumping,
    Pushing
}