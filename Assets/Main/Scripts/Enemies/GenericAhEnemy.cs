using Bubble.Temp;
using UnityEngine;
using UnityEngine.AI;

namespace Bubble.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CircleCollider2D))]
    public class GenericAhEnemy : MonoBehaviour
    {
        public Transform lookDir;

        [SerializeField] protected NavMeshAgent agent;
        protected Rigidbody2D rb;
        protected BoxCollider2D box;
        protected CircleCollider2D circle;
        
        [SerializeField] protected Transform target;
        [SerializeField] protected float fieldOfView;
        [SerializeField] protected float socialDistance;
        [Space, SerializeField] protected float moveSpeed;
        [SerializeField] protected float maxSpeed;

        protected virtual void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
            circle = GetComponent<CircleCollider2D>();
            
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            
            circle.isTrigger = true;

            target = GetTarget();
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
            circle = GetComponent<CircleCollider2D>();
            agent = GetComponent<NavMeshAgent>();
            
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            
            circle.isTrigger = true;
            
            target = GetTarget();
        }

        protected virtual void Update()
        {
            if (!IsTargetInView())
            {
                print("Not finna move man");
                return;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (!IsTargetInView()) return;
            
            MoveToTarget();
            ClampVelocity();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) { }


        /// <summary>
        /// Generic method to move to target.
        /// </summary>
        protected void MoveToTarget()
        {
            if (target == null) return;
            
            agent.SetDestination(target.position + (Vector3)StopHuggingTarget());

            Vector2 direction = (target.position - transform.position).normalized;
            
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;

            lookDir.rotation = Quaternion.Euler(0, 0, -targetAngle);
            
            // rb.linearVelocity = (direction + StopHuggingTarget()) * moveSpeed;
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

        protected bool IsTargetInView()
        {
            return Vector2.Distance(transform.position, target.position) < fieldOfView;
        }
        
        protected Vector2 StopHuggingTarget()
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= socialDistance)
            {
                // Return a vector that moves away from the target
                return (transform.position - target.position).normalized;
            }
            return Vector2.zero;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
