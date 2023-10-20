using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerColliderController))]
[RequireComponent(typeof(StatsController))]
public abstract class PlayerMovementBehaviour : MonoBehaviour
{
    protected StatsController _stats;
    protected Rigidbody2D _rigidBody;
    protected PlayerColliderController _playerCollider;
    public bool Active = true;
    public void Start()
    {
        _stats = GetComponent<StatsController>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<PlayerColliderController>();
        Setup();
    }

    protected virtual void Setup() { }
}
