using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class LeftLegAnimationController : PlayerAnimationController
{
    [SerializeField] private Vector3 _location = Vector3.zero;
    public override void Start() {
        base.Start();
        _partType = BodyPartType.LeftLeg;
        _renderer.enabled = GetComponentInParent<PlayerManager>().PartManager.HasBodyPart[(int)_partType];
        _animator.enabled = GetComponentInParent<PlayerManager>().PartManager.HasBodyPart[(int)_partType];
    }

    public override void Update() {
        if (!_animator.enabled) return;

        base.Update();
        transform.localPosition = _location;
    }

    protected override int GetState() {
        if (_walking) return Walk;
        return Idle;
    }


    private static readonly int Walk = Animator.StringToHash("Left_leg_walkcycle");
    private static readonly int Idle = Animator.StringToHash("Left_leg_idle");
    private bool _walking => _playerCollisions.IsGrounded && Input.GetAxisRaw("Horizontal") != 0;
}
