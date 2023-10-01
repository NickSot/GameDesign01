using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]       //needed for movement
[RequireComponent(typeof(CircleCollider2D))] //needed for the rigidbody
[RequireComponent(typeof(SpriteRenderer))]  //needed for visuals


public class PlayerManager : MonoBehaviour
{
    public BodyPartManager PartManager;
    public Rigidbody2D RigidBody; 
    public bool IsGrounded { get; private set; }
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;


    void Start() {
        RigidBody = GetComponent<Rigidbody2D>();
        PartManager.AddBodyPart(BodyPartType.RightLeg);
        _groundLayer = LayerMask.GetMask("Environment");
    }

    void FixedUpdate()
    {
        DoGroundCheck();
    }
    
    /// <summary>
    /// Updates the IsGrounded variable. Checks for a collision below.
    /// </summary>
    private void DoGroundCheck() {
        if(Physics2D.BoxCast(transform.position, _groundCheckSize, 0, -Vector2.up, _groundCheckDistance, _groundLayer)) {
            IsGrounded = true;
        } else {
            IsGrounded = false;
        }
    }

    /// <summary>
    /// Function which draws the groudncheck box on screen when gizmos are enabled. For debugging purposes only
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - Vector3.up * _groundCheckDistance, _groundCheckSize);
    }

    //public static event Action<PlayerState> OnPlayerStateChanged;
    //public PlayerState State {  get; private set; }
    //public void ChangeState(PlayerState newState) {
    //    if (State == newState) return;
    //    State = newState;

    //    switch (State) {
    //        case PlayerState.Starting:
    //            handleStarting();
    //            break;
    //        case PlayerState.Idle:
    //            handleIdle();
    //            break;
    //        case PlayerState.Moving:
    //            handleMoving();
    //            break;
    //        case PlayerState.Jumping:
    //            handleJumping();
    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    //    }
    //    OnPlayerStateChanged?.Invoke(newState);
    //    return;

    //    void handleStarting(){

    //    }

    //    void handleIdle() {

    //    }

    //    void handleMoving() {

    //    }

    //    void handleJumping() {

    //    }
    //}
}

[Serializable]
public enum PlayerState {
    Starting, 
    Idle,
    Moving,
    Jumping
}
