using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _frictionAmount;
    private PlayerManager _player;

    void Start()
    {
        _player = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float targetSpeed = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float speedDifference = targetSpeed - _player.RigidBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _velocityPower) * Mathf.Sign(speedDifference);
        _player.RigidBody.AddForce(movement * Vector2.right);

        //Friction
        if(targetSpeed == 0 && _player.IsGrounded) {
            float amount = Mathf.Min(Mathf.Abs(_player.RigidBody.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_player.RigidBody.velocity.x);
            _player.RigidBody.AddForce(Vector2.left * amount, ForceMode2D.Impulse);
        }
    }
}
