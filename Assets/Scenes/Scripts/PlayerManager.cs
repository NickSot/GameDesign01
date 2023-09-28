using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]       //needed for movement
[RequireComponent(typeof(CircleCollider2D))] //needed for the rigidbody
[RequireComponent(typeof(SpriteRenderer))]  //needed for visuals


public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D _rigidBody; 
    [SerializeField] private BodyPartManager _partManager;

    public static event Action<PlayerState> OnPlayerStateChanged;

    public PlayerState State {  get; private set; }



    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ChangeState(PlayerState newState) {
        if (State == newState) return;
        State = newState;

        switch (State) {
            case PlayerState.Starting:
                handleStarting();
                break;
            case PlayerState.Idle:
                handleIdle();
                break;
            case PlayerState.Moving:
                handleMoving();
                break;
            case PlayerState.Jumping:
                handleJumping();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnPlayerStateChanged?.Invoke(newState);
        return;

        void handleStarting(){

        }

        void handleIdle() {

        }

        void handleMoving() {

        }

        void handleJumping() {

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public enum PlayerState {
    Starting, 
    Idle,
    Moving,
    Jumping
}
