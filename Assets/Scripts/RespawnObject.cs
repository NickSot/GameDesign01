using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class RespawnObject : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField] private Vector2 _respawnLocation;
    [SerializeField] private GameObject _particles;
    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 7) {
            collision.gameObject.transform.position = _respawnLocation;
            Instantiate(_particles, _respawnLocation + 0.5f*Vector2.up, Quaternion.Euler(0,0,0));
        }
    }
}
