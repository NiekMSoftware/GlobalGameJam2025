using UnityEngine;

namespace Bubble
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector2 dashDir;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashTime;
        [SerializeField] private float deathDelay = 1;

        private float dashTimer;
        private bool isDashing;

        private void OnEnable()
        {
            dashTimer = dashTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("Gaming");
            if (collision.CompareTag("Bullet"))
            {
                print("More Gaming");
                Dash();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Invoke(nameof(Death), deathDelay);
            }
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            //print(isDashing);

            if (isDashing)
            {
                dashTimer -= Time.deltaTime;

                if (dashTimer <= 0)
                {
                    print("Nah dashing");
                    dashTimer = dashTime;
                    isDashing = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Dash();
            }
        }

        private void FixedUpdate()
        {
            if (!isDashing)
            {
                Movement();

                rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
            }

            //agent.SetDestination(target.position);
        }

        private void Dash()
        {
            isDashing = true;
            print("Dash!");
            rb.linearVelocity = dashDir * dashForce;
        }

        private void Movement()
        {
            if (!target) return;

            Vector3 moveDir = target.position - transform.position;

            //moveDir.Normalize();

            rb.linearVelocity = moveDir * speed;
        }
    }
}
