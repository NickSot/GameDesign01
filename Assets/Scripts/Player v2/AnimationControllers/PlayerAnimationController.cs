using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimationController : MonoBehaviour {
    protected Animator _animator;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rigidBody;
    [SerializeField] protected BodyPartType _partType;
    public bool IsActive { get; protected set; }

    protected virtual void Start() {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        BodyPartManager.OnBodyPartAdded += SetActive;
        BodyPartManager.OnBodyPartRemoved += SetInactive;
    }

    protected virtual void OnDisable() {
        BodyPartManager.OnBodyPartAdded -= SetActive;
        BodyPartManager.OnBodyPartRemoved -= SetInactive;
    }

    public virtual void Update() {
        SetSpriteDirection();
        _currentState = GetState();
        if (_currentState != _lastState) _animator.CrossFade(_currentState, 0, 0);
        _lastState = _currentState;
    }

    /// <summary>
    /// Turns the renderer on if the part removed equals the bodypart of this renderer
    /// </summary>
    /// <param name="type"></param>
    protected void SetActive(BodyPartType type) {
        if (type == _partType) _renderer.enabled = true;
    }

    /// <summary>
    /// Turns the renderer off if the part removed equals the bodypart of this renderer
    /// </summary>
    /// <param name="type"></param>
    protected void SetInactive(BodyPartType type) {
        if (type == _partType) _renderer.enabled = false;
    }

    /// <summary>
    /// Makes sure the sprite is facing the direction you are moving
    /// </summary>
    protected void SetSpriteDirection() {
        if (_rigidBody.velocity.x < 0) {
            _renderer.flipX = true;
        } else if (_rigidBody.velocity.x > 0) {
            _renderer.flipX = false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>The desired animation</returns>
    virtual protected int GetState() {
        return _currentState;
    }


    /// <summary>
    /// keep track of different animation states, possible animations can be added below
    /// </summary>
    protected int _currentState;
    protected int _lastState;
    //private static readonly int ANIMATION = Animator.StringTOHash("ANIMATIONNAME");
}

