using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimationController : PlayerAnimationController
{
    private bool isWalking => _rigidBody.freezeRotation;
    private bool isRolling => !isWalking;
    [SerializeField] private Vector3 _walkLocationHead = Vector3.zero;
    [SerializeField] private Vector3 _rollLocationHead = Vector3.zero;

    public override void Start() {
        base.Start();
        _animator.enabled = false; //TODO REMOVE THIS LINE WHEN ANIMATIONS ARE IMPLEMENTED
        BodyPartManager.OnAbiltiesChanged += SetLocation;
    }

    protected override void OnDisable() {
        base.OnDisable();
        BodyPartManager.OnAbiltiesChanged -= SetLocation;
    }

    public override void Update() {
        if (isWalking) { //if we are walking
            HandleWalking();
        } 
        if (isRolling) { //if we are rolling
            HandleRolling();
        }
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
}
