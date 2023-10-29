using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
    private CircleCollider2D _collider;
    [SerializeField] private BodyPartType _type;

    private readonly KeyCode[] _keycodes = { KeyCode.Alpha0, KeyCode.Alpha5, KeyCode.Alpha4, KeyCode.Alpha3, KeyCode.Alpha2, KeyCode.Alpha1 };
    private float _time = 0;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) {
            PlayerManager manager = player.GetComponent<PlayerManager>();
            if(manager != null) {
                if (manager.PartManager.HasBodyPart[(int)_type]) {
                    Destroy(gameObject);
                }
            } else {
                Debug.Log("COULD NOT FIND SCRIPTABLE OBJECT");
            }
        } else {
            Debug.Log("COULD NOT FIND PLAYER");
        }
        _collider.isTrigger = true;
    }

    private void Update() {
        bool input = Input.GetKey(_keycodes[(int)_type]);
        if (input) {
            _time += Time.deltaTime;
        } else {
            _time = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == 7) {
            if (_time > 0.5f) {
                Debug.Log("Added part");
                collision.gameObject.GetComponent<PlayerManager>().PartManager.AddBodyPart(_type);
                Destroy(gameObject);
            }
        }
    }
}
