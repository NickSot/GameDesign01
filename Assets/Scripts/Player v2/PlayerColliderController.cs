using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerColliderController : MonoBehaviour
{
    private CircleCollider2D _headCollider;
    private BoxCollider2D _bodyCollider;

    public bool IsGrounded { get; private set; }
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;

    void Start()
    {
        _headCollider = GetComponent<CircleCollider2D>();
        _bodyCollider = GetComponent<BoxCollider2D>();
        BodyPartManager.OnAbiltiesChanged += ChangeColliders;
        _groundLayer = LayerMask.GetMask("Environment");
    }

    private void OnDisable() {
        BodyPartManager.OnAbiltiesChanged -= ChangeColliders;
    }


    /// <summary>
    /// Changes the active collider based on the abilities of the player.
    /// </summary>
    /// <param name="abilities">Array with player abilites, accesible using (int)PlayerAbilities.AbilityName</param>
    private void ChangeColliders(bool[] abilities) {
        if(abilities.Length == Enum.GetNames(typeof(PlayerAbilities)).Length) {
            if (abilities[(int)PlayerAbilities.Roll]) {
                _headCollider.enabled = true;
                _bodyCollider.enabled = false;
                _groundCheckSize = new Vector2(0.5f, 0.1f);
            }
            if (abilities[(int)PlayerAbilities.Walk]) {
                _headCollider.enabled = false;
                _bodyCollider.enabled = true;
                _groundCheckSize = new Vector2(0.95f, 0.1f);
            }
        } else {
            Debug.Log("ERROR changing colliders, array passed by bodypartManager has incorrect size");
        }
    }

    /// <summary>
    /// Updates the IsGrounded variable. Checks for a collision below.
    /// </summary>
    public void DoGroundCheck() {
        if (Physics2D.BoxCast(transform.position, _groundCheckSize, 0, -Vector2.up, _groundCheckDistance, _groundLayer)) {
            IsGrounded = true;
        } else {
            IsGrounded = false;
        }
    }

    /// <summary>
    /// Function which draws the groudncheck box on screen when gizmos are enabled. For debugging purposes only
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - Vector3.up * _groundCheckDistance, _groundCheckSize);
    }
}
