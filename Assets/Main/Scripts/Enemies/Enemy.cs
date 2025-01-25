using Bubble.Enemies;
using UnityEngine;
using UnityEngine.InputSystem.Android;

namespace Bubble
{
    public class Enemy : GenericAhEnemy
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 dashDir;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashTime;
        [SerializeField] private float deathDelay = 1;
        [SerializeField] private GameObject dashEffect;

        private float dashTimer;
        private bool isDashing;

        private GameObject spawnedDashEffect;

        private void OnEnable()
        {
            dashTimer = dashTime;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.CompareTag("Bullet") && !collision.GetComponent<Projectile>().hasHitEnemy)
            {
                Dash();
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            //base.OnCollisionEnter2D(collision);
            
            if (collision.gameObject.CompareTag("Bullet") && !collision.gameObject.GetComponent<Projectile>().isEnemyBullet 
                && !collision.gameObject.GetComponent<Projectile>().hasHitEnemy)
            {
                Invoke(nameof(Death), deathDelay);
            }
        }

        private void Death()
        {
            if (spawnedDashEffect) Destroy(spawnedDashEffect);
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
                    agent.speed = 3.5f;
                    agent.acceleration = 8f;
                    //agent.autoBraking = true;

                    print("Nah dashing");
                    dashTimer = dashTime;
                    isDashing = false;
                    Destroy(spawnedDashEffect);
                }
            }

            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    Dash();
            //}
        }

        protected override void FixedUpdate()
        {
            if (!IsTargetInView()) return;
            
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
            //agent.enabled = false;
            agent.speed = 100;
            //agent.autoBraking = false;
            agent.acceleration = 1000;
            agent.SetDestination((Vector2)transform.position + (dashDir * dashForce));
            rb.linearVelocity = dashDir * dashForce;
            //agent.enabled = true;

            var angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;

            spawnedDashEffect = Instantiate(dashEffect, (Vector2)transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
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
