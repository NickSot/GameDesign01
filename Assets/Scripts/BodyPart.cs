using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private BodyPartType _type;

    private bool _isActive = true;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 7) {
            if (_isActive) {
                _isActive = false;
                collision.gameObject.GetComponent<PlayerManager>().PartManager.AddBodyPart(_type);
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 7) {
            _isActive = true;
        }
    }
}
