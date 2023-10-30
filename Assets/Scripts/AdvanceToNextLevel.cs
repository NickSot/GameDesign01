using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class AdvanceToNextLevel : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField] private string _nextLevelName;

    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
        
        //SceneManager.GetSceneByName
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 7) {
            LevelManager.Instance.LoadScene(_nextLevelName);
        }
    }
}
