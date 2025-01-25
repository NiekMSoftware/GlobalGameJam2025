using Bubble.Temp;
using UnityEngine;

namespace Bubble.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CircleCollider2D))]
    public class GenericAhEnemy : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected BoxCollider2D box;
        protected CircleCollider2D circle;
        
        [SerializeField] protected Transform target;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float maxSpeed;

        protected virtual void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
            circle = GetComponent<CircleCollider2D>();
            
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            
            circle.radius = 3f;
            circle.isTrigger = true;

            target = GetTarget();
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
            circle = GetComponent<CircleCollider2D>();
            
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            
            circle.radius = 3f;
            circle.isTrigger = true;
            
            target = GetTarget();
        }

        protected virtual void Update() { }

        protected virtual void FixedUpdate()
        {
            MoveToTarget();
            ClampVelocity();
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other) { }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            // destroy this enemy
            if (other.gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Generic method to move to target.
        /// </summary>
        protected void MoveToTarget()
        {
            if (target == null) return;
            
            Vector2 direction = target.position - transform.position;
            rb.linearVelocity = direction.normalized * moveSpeed;
        }

        /// <summary>
        /// Generic method to clamp velocity.
        /// </summary>
        protected void ClampVelocity()
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }

        /// <summary>
        /// Get the player target from the scene.
        /// </summary>
        /// <returns>Returns the target.</returns>
        protected Transform GetTarget()
        {
            return FindFirstObjectByType<PC_TopDown>().transform;
        }
    }
}
