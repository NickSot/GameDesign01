using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class TorsoAnimationController : PlayerAnimationController {

    [SerializeField] private Vector3 _location = Vector3.zero;
    public override void Start() {
        base.Start();
        _animator.enabled = false; //TODO REMOVE THIS LINE WHEN ANIMATIONS ARE IMPLEMENTED
        _partType = BodyPartType.Torso;
        _renderer.enabled = GetComponentInParent<PlayerManager>().PartManager.HasBodyPart[(int)_partType];

    }

    public override void Update() { 
        base.Update();

        transform.localPosition = _location;
    }
}
