using Bubble.Enemies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Bubble
{
    public class Enemy : GenericAhEnemy
    {


        [SerializeField] private Vector2[] dashDir;
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
            base.OnTriggerEnter2D (collision);
            if (collision.CompareTag("Bullet"))
            {
                isDashing = true;
                Dash();
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                print($"I collided with: {collision.gameObject.name}");
                Die();
            }
        }

        //private void Death()
        //{
        //    Destroy(gameObject);
        //}

        protected override void Update()
        {
            base.Update();


            if (isDashing)
            {
                //agent.enabled = false;
                dashTimer -= Time.deltaTime;

                if (dashTimer <= 0)
                {
                    agent = GetComponentInChildren<NavMeshAgent>();
                    agent.enabled = true;
                    agent.speed = 3.5f;
                    agent.acceleration = 8f;
                    dashTimer = dashTime;
                    isDashing = false;
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (!IsTargetInView()) return;
            
            if (!isDashing)
            {
                base.FixedUpdate();
                //Movement();
                //ClampVelocity();
            }
        }

        private void Dash()
        {
                if (!agent) return;

                agent.enabled = false;
                agent.radius = 0.1f;
                agent.height = 0.1f;
                agent = null;

                rb.AddForce(dashDir[0] * dashForce, ForceMode2D.Impulse);

                var angle = Mathf.Atan2(dashDir[0].y, dashDir[0].x) * Mathf.Rad2Deg;
                spawnedDashEffect = Instantiate(dashEffect, (Vector2)transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                StartCoroutine(SecondDash());
        }


        IEnumerator SecondDash()
        {
                yield return new WaitForSeconds(0.2f); // cant be lower i dunno why
                isDashing = true;
                agent.enabled = false;
                agent.radius = 0.1f;
                agent.height = 0.1f;
                agent = null;

                rb.AddForce(dashDir[1] * dashForce, ForceMode2D.Impulse);
                var angle = Mathf.Atan2(dashDir[1].y, dashDir[1].x) * Mathf.Rad2Deg;
                spawnedDashEffect = Instantiate(dashEffect, (Vector2)transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                StopCoroutine(SecondDash());
        }

        private void Movement()
        {
            if (!agent || !target) return;
            agent.SetDestination(target.position);
            if (rb)
            {
                float s = speed * lerpSpeed;
                Vector2 newPosition = Vector2.Lerp(rb.position, agent.transform.position, Time.deltaTime * s);
                rb.MovePosition(newPosition);
            }
            else
            {
                // Fallback: directly interpolate the parent's position
                transform.position = Vector2.Lerp(transform.position, agent.transform.position, Time.deltaTime * speed);
            }
        }
    }
}
