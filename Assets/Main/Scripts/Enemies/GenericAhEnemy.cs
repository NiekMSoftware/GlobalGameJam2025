using Bubble.Temp;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

namespace Bubble.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CircleCollider2D))]
    public class GenericAhEnemy : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float lerpSpeed;

        public Transform lookDir;

        [SerializeField] protected NavMeshAgent agent;
        protected Rigidbody2D rb;
        protected BoxCollider2D box;
        protected CircleCollider2D circle;

        protected bool DoneOnce = false;
        protected Vector2 ProjectileVelocity;
        protected Rigidbody2D bulletRB;

        private float projectileTimer = 0.8f; //Zorgt dat je niet twee keer op dezelfde trigger van dezelfde enemy instance een particle kan maken 
        private float projectileTimerCount = 0; 

        [SerializeField] protected GameObject Particles;
        [SerializeField] protected Transform target;
        [SerializeField] protected float fieldOfView;
        [SerializeField] protected float socialDistance;
        [Space, SerializeField] protected float moveSpeed;
        [SerializeField] protected float maxSpeed;
        [SerializeField] private Sprite deadSprite;

        public AudioSource audioSource;

        public AudioClip[] deathSounds;

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
            agent = GetComponentInChildren<NavMeshAgent>();
            
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            
            circle.isTrigger = true;
            
            target = GetTarget();
        }

        protected virtual void Update()
        {
            projectileTimerCount -= Time.deltaTime;
            if (!IsTargetInView())
            {
                return;
            }

        }

        protected virtual void FixedUpdate()
        {
            if (!IsTargetInView()) return;

            MoveToTarget();
            ClampVelocity();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) 
        {
            try //Cut speed in half for the projectile when entering trigger but cannot be cut twice 
            {
                var bullet = other.GetComponent<Projectile>();
                bulletRB = bullet.GetComponent<Rigidbody2D>();

                if (projectileTimerCount < 0) // does not proc twice because projectileTimerCount
                { 

                    Instantiate(bullet.Particles, new Vector2(other.transform.position.x, other.transform.position.y), other.transform.rotation);

                    projectileTimerCount = projectileTimer;
                }

                if (!DoneOnce)
                { 
                    bulletRB.linearVelocity /= 3;
                    ProjectileVelocity = bulletRB.linearVelocity;
                    DoneOnce = false;
                }
                else 
                { 
                    bulletRB.linearVelocity = ProjectileVelocity;
                }
                
            }
            catch { }
        }

        protected virtual void OnTriggerExit2D(Collider2D other) // Return speed of projectile to original
        {
            if (bulletRB != null)
                bulletRB.linearVelocity = ProjectileVelocity * 2;
        }


        /// <summary>
        /// Generic method to move to target.
        /// </summary>
        protected void MoveToTarget()
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
            Destroy(gameObject , 0.5f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<EnemyShooting>().ShootingCooldown = 999f;
            PlayRandomSoundFromList(deathSounds);
        }
        private void OnDestroy()
        {
            if (Particles != null)
                Instantiate(Particles, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
        }
        private void PlayRandomSoundFromList(AudioClip[] list)
        {
            AudioClip audioClip = list[Random.Range(0, list.Length)];
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
