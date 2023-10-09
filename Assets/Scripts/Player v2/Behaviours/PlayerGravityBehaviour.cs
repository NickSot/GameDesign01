using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityBehaviour : PlayerMovementBehaviour
{
    [SerializeField] private float _fallGravityMultiplier = 1.5f;
    [SerializeField] private float _maxFallSpeed = 50f;
    void FixedUpdate()
    {
        if (_rigidBody.velocity.y < 0) _rigidBody.gravityScale = _stats.GravityScale * _fallGravityMultiplier;
        else _rigidBody.gravityScale = _stats.GravityScale;
        if(_rigidBody.velocity.y<-_maxFallSpeed) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -_maxFallSpeed);
    }
}
