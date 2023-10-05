using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]       //needed for movement
[RequireComponent(typeof(CircleCollider2D))] //needed for the rigidbody
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]  //needed for visuals

public class PlayerManager : MonoBehaviour
{
    public BodyPartManager PartManager;
    public Rigidbody2D RigidBody;

    private CircleCollider2D headCollider;
    private BoxCollider2D bodyCollider;

    public bool IsGrounded { get; private set; }
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;


    void Start() {
        RigidBody = GetComponent<Rigidbody2D>();
        //PartManager.AddBodyPart(BodyPartType.RightLeg);
        headCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        _groundLayer = LayerMask.GetMask("Environment");
        BodyPartManager.OnAbiltiesChanged += handleChangeColliders;
        PartManager.ResetBodyParts();
        PartManager.AddBodyPart(BodyPartType.LeftLeg);
        //PartManager.AddBodyPart(BodyPartType.RightLeg);
        Debug.Log("Started Player Manager");
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

    private void OnDisable() {
        Debug.Log("Disabled Player Manager");
        BodyPartManager.OnAbiltiesChanged -= handleChangeColliders;
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



    // Behaviour
    /*   Head: just roll back and forth
     *   Head and leg: roll, or jump
     *   Head and two legs: Run and jump
     *   Head and arm: roll and push
     *   Head and two arms: roll, push and pull
     *   Head and leg
     */


    /*
     Different behaviours
        horizontal behaviour: head, two legs
        push behaviour
        pull behaviour

     */



    void handleChangeColliders() {
        Debug.Log("abiltiesChanged");
        if (PartManager.HasAbility[(int)PlayerAbilities.Roll]) {
            headCollider.enabled = true;
            bodyCollider.enabled = false;
            RigidBody.freezeRotation = false;
            _groundCheckSize = new Vector2(0.5f, 0.1f);
            RigidBody.sharedMaterial.friction = 0.0f;
        }
        if (PartManager.HasAbility[(int)PlayerAbilities.Walk]) {
            //if(bodyCollider.enabled) // handle setting the height of sprites of the player to correct position
            headCollider.enabled = false;
            bodyCollider.enabled = true;
            transform.eulerAngles = Vector3.zero;
            RigidBody.freezeRotation = true;
            _groundCheckSize = new Vector2(0.95f, 0.1f);
            RigidBody.sharedMaterial.friction = 0.1f;
        }
    }
}

[Serializable]
public enum PlayerState {
    Starting, 
    Idle,
    Moving,
    Jumping
}
