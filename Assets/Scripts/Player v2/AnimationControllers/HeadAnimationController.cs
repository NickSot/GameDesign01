using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimationController : PlayerAnimationController
{
    private bool _isWalking => _rigidBody.freezeRotation;
    private bool _isRolling => !_isWalking;
    [SerializeField] private Vector3 _walkLocationHead = Vector3.zero;
    [SerializeField] private Vector3 _rollLocationHead = Vector3.zero;

    public override void Start() {
        base.Start();
        BodyPartManager.OnAbiltiesChanged += SetLocation;
        if (_isRolling) transform.localPosition = _rollLocationHead;
        if (_isWalking) transform.localPosition = _walkLocationHead;
        _partType = BodyPartType.Head;
        _renderer.enabled = GetComponentInParent<PlayerManager>().PartManager.HasBodyPart[(int)_partType];
        _animator.enabled = GetComponentInParent<PlayerManager>().PartManager.HasBodyPart[(int)_partType];
    }

    protected override void OnDisable() {
        base.OnDisable();
        BodyPartManager.OnAbiltiesChanged -= SetLocation;
    }

    public override void Update() {
        if (_isWalking) { //if we are walking
            HandleWalking();
        } 
        if (_isRolling) { //if we are rolling
            HandleRolling();
        }
        _currentState = GetState();
        if (_currentState != _lastState) _animator.CrossFade(_currentState, 0, 0);
        _lastState = _currentState;
    }

    private void HandleWalking() {
        SetSpriteDirection();
    }

    private void HandleRolling() {

    }

    private void SetLocation(bool[] abilities) {
        if (abilities[(int)PlayerAbilities.Walk]) {
            transform.localPosition = _walkLocationHead;
        } else {
            transform.localPosition = _rollLocationHead;
        }
    }

    protected override int GetState() {
        if (_walking) return Walk;
        return Idle;
    }

    private static readonly int Walk = Animator.StringToHash("Head_walkcycle");
    private static readonly int Idle = Animator.StringToHash("Head_idle");
    private bool _walking => _isWalking && _playerCollisions.IsGrounded && Input.GetAxisRaw("Horizontal") != 0;
}
