using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBehaviour : PlayerMovementBehaviour {

    private bool _hasAbility = false;
    [SerializeField] private float _coyoteTimeThreshold = 0.1f;
    [SerializeField] private float _jumpBufferThreshold = 0.1f;


    [SerializeField] private AudioClip _jumpSound;
    private bool CanUseCoyote => !_playerCollider.IsGrounded && _timeLastOnGround > 0;
    private bool CanJump => _hasAbility && (_playerCollider.IsGrounded || CanUseCoyote || _airJumpsLeft > 0);
    private float _timeJumpBuffer = 0.0f;
    private float _timeLastOnGround = 0.0f;
    private int _airJumpsLeft;
    private bool _isJumping = false;

    private void OnEnable() {
        BodyPartManager.OnAbiltiesChanged += SetAbility;
    }

    private void OnDisable() {
        BodyPartManager.OnAbiltiesChanged -= SetAbility;
    }

    void Update()
    {
        if (!Active) return;
        _playerCollider.DoGroundCheck();

        //update buffer timers
        _timeLastOnGround -= Time.deltaTime;
        _timeJumpBuffer -= Time.deltaTime;
        
        if (_playerCollider.IsGrounded) {
            _isJumping = false;
            _timeLastOnGround = _coyoteTimeThreshold;
            _airJumpsLeft = _stats.AirJumps;
            if (_timeJumpBuffer > 0) OnJumpInputDown();
        }

        // perform jumps
        if (Input.GetKeyDown(KeyCode.Space)) OnJumpInputDown();
        if (Input.GetKeyUp(KeyCode.Space)) OnJumpInputUp();
    }


    /// <summary>
    /// Resets jumpbuffer and performs jump if allowed
    /// </summary>
    private void OnJumpInputDown() {
        _timeJumpBuffer = _jumpBufferThreshold;
        if (CanJump) {
            PerformJump();
            if (!_playerCollider.IsGrounded && !CanUseCoyote) {
                _airJumpsLeft--;
            }
        }
    }

    /// <summary>
    /// Performs the jump cut, the vertical velocity of the player gets decreased
    /// </summary>
    private void OnJumpInputUp() {
        if (_isJumping) {
            _isJumping = false;
            //if (_player.RigidBody.velocity.y > 0) _player.RigidBody.AddForce(Vector2.down * _player.RigidBody.velocity.y * (1-_jumpCutGravityModifier), ForceMode2D.Impulse); // this method results in buggy jump behaviour when on slope
            if (_rigidBody.velocity.y > -_stats.JumpCutModifier * _stats.JumpVelocity) _rigidBody.velocity -= new Vector2(0, _stats.JumpVelocity * _stats.JumpCutModifier);
            Debug.Log("Jump cut");
        }
    }

    /// <summary>
    /// Performs the jump
    /// </summary>
    private void PerformJump() {
        _isJumping = true;
        //_player.RigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); // this method results in buggy jump behaviour when on slope
        AudioManager.Instance.PlaySFX(_jumpSound);
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _stats.JumpVelocity);
        Debug.Log("Jumped");
    }

    /// <summary>
    /// Sets the _hasABility bool member when the players abilites change
    /// </summary>
    /// <param name="abilities">Array with player abilites, accesible using (int)PlayerAbilities.AbilityName</param>
    private void SetAbility(bool[] abilities) {
        if (abilities.Length == Enum.GetNames(typeof(PlayerAbilities)).Length) {
            _hasAbility = abilities[(int)PlayerAbilities.Jump];
        } else {
            Debug.Log("ERROR setting jumping ability, array passed by bodypartManager has incorrect size");
        }
    }
}
