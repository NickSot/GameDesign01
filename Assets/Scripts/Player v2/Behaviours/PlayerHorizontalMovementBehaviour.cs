using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerHorizontalMovementBehaviour : PlayerMovementBehaviour {


    private void OnEnable() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _stats = GetComponent<StatsController>();
        if (_rigidBody.sharedMaterial == null) _rigidBody.sharedMaterial = new PhysicsMaterial2D();
        BodyPartManager.OnAbiltiesChanged += ChangeRigidbody;
        _rigidBody.sharedMaterial.friction = _stats.Grip;
        GetComponent<PlayerManager>().PartManager.UpdateEventListeners();

    }
    private void OnDisable() {
        BodyPartManager.OnAbiltiesChanged -= ChangeRigidbody;
    }

    void FixedUpdate()
    {
        if (!Active) return;
        _rigidBody.sharedMaterial.friction = _stats.Grip;
        float targetSpeed = Input.GetAxisRaw("Horizontal") * _stats.MoveSpeed;
        float speedDifference = targetSpeed - _rigidBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _stats.Acceleration : _stats.Deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _stats.VelocityPower) * Mathf.Sign(speedDifference);
        _rigidBody.AddForce(movement * Vector2.right);

        //Friction
        if (targetSpeed == 0 && _playerCollider.IsGrounded) {
            float amount = Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(_stats.Friction));
            amount *= Mathf.Sign(_rigidBody.velocity.x);
            _rigidBody.AddForce(Vector2.left * amount, ForceMode2D.Impulse);
        }
    }

    private void ChangeRigidbody(bool[] abilities) {
        if (abilities.Length == Enum.GetNames(typeof(PlayerAbilities)).Length) {
            if (abilities[(int)PlayerAbilities.Roll]) {
                _rigidBody.freezeRotation = false;
            }
            if (abilities[(int)PlayerAbilities.Walk]) {
                transform.eulerAngles = Vector3.zero;
                _rigidBody.freezeRotation = true;
            }
        } else {
            Debug.Log("ERROR changing rigidbody, array passed by bodypartManager has incorrect size");
        }
    }
}
