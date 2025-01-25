using Bubble.Enemies;
using UnityEngine;

namespace Bubble
{
    public class Enemy : GenericAhEnemy
    {
        [SerializeField] private float speed;
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

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.CompareTag("Bullet"))
            {
                Dash();
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet") && !collision.gameObject.GetComponent<Projectile>().isEnemyBullet)
            {
                Invoke(nameof(Death), deathDelay);
            }
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        protected override void Update()
        {
            base.Update();

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

        protected override void FixedUpdate()
        {
            if (!isDashing)
            {
                MoveToTarget();
                ClampVelocity();
            }

            //agent.SetDestination(target.position);
        }

        private void Dash()
        {
            isDashing = true;
            print("Dash!");
            rb.linearVelocity = dashDir * dashForce;
        }

        //private void Movement()
        //{
        //    if (!target) return;

        //    Vector3 moveDir = target.position - transform.position;

        //    //moveDir.Normalize();

        //    rb.linearVelocity = moveDir * speed;
        //}
    }
}
