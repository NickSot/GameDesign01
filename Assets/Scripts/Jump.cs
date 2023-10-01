using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Jump : MonoBehaviour
{
    public bool Active = true;

    private PlayerManager _player;

    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _jumpForce = 1f;
    //[SerializeField] private float _jumpHeight = 3f;
    //[SerializeField] private float _jumpTimeToApex = 0.3f;
    [SerializeField] private float _jumpCutModifier = 0.5f;
    [SerializeField] private float _fallGravityMultiplier = 1.5f;
    [SerializeField] private int _airJumps = 0;

    private float _jumpVelocity = 10;
           
    [SerializeField] private float _coyoteTimeThreshold = 0.1f;
    [SerializeField] private float _jumpBufferThreshold = 0.1f;

    private bool CanUseCoyote => !_player.IsGrounded && _timeLastOnGround > 0;
    private bool CanJump => /*_player.PartManager.CanJump &&*/ (_player.IsGrounded || CanUseCoyote || _airJumpsLeft > 0);
    private float _timeJumpBuffer = 0.0f;
    private float _timeLastOnGround = 0.0f;
    private int _airJumpsLeft;
    private bool _isJumping = false;

    public void Start() {
        _player = GetComponent<PlayerManager>();
    }


    public void Update() {
        //if(_player.PartManager.CanJump) Debug.Log("We should be able to jump");

        // update our timers which enable mroe responsive jumping
        if (!Active) return;
        _timeLastOnGround -= Time.deltaTime;
        _timeJumpBuffer -= Time.deltaTime;
        if (_player.IsGrounded) {
            _timeLastOnGround = _coyoteTimeThreshold;
            _airJumpsLeft = _airJumps;
            if (_timeJumpBuffer > 0) OnJumpInputDown();
        }

        // add extra gravity when falling, makes character feel less floaty
        if (_player.RigidBody.velocity.y < 0) _player.RigidBody.gravityScale = _gravityScale * _fallGravityMultiplier;
        else _player.RigidBody.gravityScale = _gravityScale;

        // perform jumps
        if (Input.GetKeyDown(KeyCode.Space)) OnJumpInputDown();
        if (Input.GetKeyUp(KeyCode.Space)) OnJumpInputUp();
    }

    private void OnJumpInputDown() {
        _timeJumpBuffer = _jumpBufferThreshold;
        if (CanJump) {
            //_isJumping = true;
            PerformJump();
            if (!_player.IsGrounded && !CanUseCoyote) { 
                _airJumpsLeft--;
            }
        }
    }

    private void OnJumpInputUp() {
        if (_isJumping) {
            _isJumping = false;
            //if (_player.RigidBody.velocity.y > 0) _player.RigidBody.AddForce(Vector2.down * _player.RigidBody.velocity.y * (1-_jumpCutGravityModifier), ForceMode2D.Impulse); // this method results in buggy jump behaviour when on slope
            if (_player.RigidBody.velocity.y > -_jumpCutModifier * _jumpForce) _player.RigidBody.velocity -= new Vector2(0, _jumpVelocity * _jumpCutModifier);
            Debug.Log("Jump cut");
        }
    }

    private void PerformJump() {
        _isJumping = true;
        //_player.RigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); // this method results in buggy jump behaviour when on slope
        _player.RigidBody.velocity = new Vector2(_player.RigidBody.velocity.x,_jumpVelocity);
        Debug.Log("Jumped");
    }


}
