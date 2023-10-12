using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class MoveableObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        gameObject.layer = 6; // assign the environment

    }



    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 7) {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            if (player != null) {
                if (player.PartManager.HasAbility[(int)PlayerAbilities.Push]) {
                    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                } else {
                    _rigidbody.bodyType = RigidbodyType2D.Static;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == 7) {
            _rigidbody.bodyType= RigidbodyType2D.Static;
        }
    }
}
