using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityBehaviour : PlayerMovementBehaviour
{
    [SerializeField] private float _fallGravityMultiplier = 1.5f;
    [SerializeField] private float _maxFallSpeed = 50f;
    [SerializeField] private bool _asHead = true;

    private void OnEnable() {
        BodyPartManager.OnAbiltiesChanged += SetGravityMode;
    }

    private void OnDisable() {
        BodyPartManager.OnAbiltiesChanged -= SetGravityMode;
    }

    private void SetGravityMode(bool[] abilities) {
        _asHead = !abilities[(int)PlayerAbilities.Walk];
    }

    void FixedUpdate()
    {
        //if (_playerCollider.IsGrounded) _rigidBody.gravityScale = 0;
        //else if (_rigidBody.velocity.y < 0) _rigidBody.gravityScale = _stats.GravityScale * _fallGravityMultiplier;
        //else _rigidBody.gravityScale = _stats.GravityScale;

        //if(_rigidBody.velocity.y<-_maxFallSpeed) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -_maxFallSpeed);
        //if (_playerCollider.IsGrounded &&) return;
        _rigidBody.gravityScale = 0;
        if (!_asHead && _playerCollider.IsGrounded) return;

        if (_rigidBody.velocity.y < 0) _rigidBody.AddForce(9.81f * _stats.GravityScale * _fallGravityMultiplier * Vector2.down);
        else _rigidBody.AddForce(9.81f * _stats.GravityScale * Vector2.down);

        if (_rigidBody.velocity.y < -_maxFallSpeed) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -_maxFallSpeed);
    }
}
