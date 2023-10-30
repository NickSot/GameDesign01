using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private class StatsController { 
        public float GravityScale { get; private set; } = 1;
        public float MoveSpeed { get; private set; } = 5f;
        public float Acceleration { get; private set; } = 1f;
        public float Deceleration { get; private set; } = 3f;
        public float VelocityPower { get; private set; } = 0.5f;
        public float Friction { get; private set; } = 0.0000f;
        public float Grip { get; private set; } = 0.1f;
    }

    public GameObject player;
    public bool IsActive;
    private bool Activated = false;

    private StatsController _stats;
    private Rigidbody2D _rigidBody;
    private PlayerColliderController _playerCollider;

    // Start is called before the first frame update
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _stats = new StatsController();
        if (_rigidBody.sharedMaterial == null) _rigidBody.sharedMaterial = new PhysicsMaterial2D();
        _rigidBody.sharedMaterial.friction = _stats.Grip;
    }
    
    void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _stats = new StatsController();
        if (_rigidBody.sharedMaterial == null) _rigidBody.sharedMaterial = new PhysicsMaterial2D();
        _rigidBody.sharedMaterial.friction = _stats.Grip;
    }

    // Update is called once per frame
    void Update()
    {

        // disgusting, I know.
        if (player.transform.position.y >= transform.position.y - 0.1f && player.transform.position.y <= transform.position.y + 0.1f) {
            Activated = true;
        }
        
        if (IsActive && Activated) {
            float targetSpeed = _stats.MoveSpeed;
            float speedDifference = targetSpeed - _rigidBody.velocity.x;
            float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _stats.Acceleration : _stats.Deceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _stats.VelocityPower) * Mathf.Sign(speedDifference);

            if (targetSpeed == 0 && _playerCollider.IsGrounded)
            {
                float amount = Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(_stats.Friction));
                amount *= Mathf.Sign(_rigidBody.velocity.x);
                _rigidBody.AddForce(Vector2.left * amount, ForceMode2D.Impulse);
            }

            if (transform.position.x - player.transform.position.x < 0)
                _rigidBody.AddForce(movement * Vector2.right);
            else
                _rigidBody.AddForce(movement * Vector2.left);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
