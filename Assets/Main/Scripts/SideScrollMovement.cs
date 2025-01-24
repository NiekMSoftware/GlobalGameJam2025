using UnityEngine;

namespace Bubble
{
    public class SideScrollMovement : MonoBehaviour
    {
        public static SideScrollMovement instance;

        [SerializeField] private Rigidbody2D rb;
        [Header("Basic variables")]
        [Header("Walking")]
        [SerializeField] private float walkSpeed = 3;
        [SerializeField] private float ttMaxSpeed = 3;
        private float runningTime;
        [SerializeField] private float maxSpeedMultiplier = 4;


        [SerializeField] private float jumpPower = 3;
        public bool canJump = true;
        public bool canMove = true;
        public GameObject canvas;

        public int facing = 1;

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float value = 0;
            if (Input.GetKey(KeyCode.D))
            {
                value = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                value = -1;
            }
            if (value != 0 && runningTime < ttMaxSpeed + 0.5f)
            {
                runningTime += Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            //Movement
            //Reduce speed mid air
            //Hold space for more jump cuz das cool
            //Stun on falling for too long
            if (canMove)
            {
                float sideInput = 0;
                float upInput = 0;
                if (Input.GetKey(KeyCode.Space) && canJump)
                {
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, 0.2f);
                    if (hit)
                    {
                        upInput = 300 * jumpPower;
                        canJump = false;
                    }
                }
                if (Input.GetKey(KeyCode.D))
                {
                    sideInput = 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    sideInput = -1;
                }
                float totalSide = sideInput;

                if (runningTime != 0)
                {
                    float actualSpBuff = totalSide;
                    actualSpBuff *= (runningTime / ttMaxSpeed) * maxSpeedMultiplier;
                    totalSide += actualSpBuff;
                }

                if (sideInput != 0 || upInput != 0)
                {
                    if ((int)sideInput != 0)
                    {
                        facing = (int)sideInput;
                    }
                    rb.AddForce(new Vector2((totalSide * 10) * walkSpeed, upInput));
                }
                else
                {
                    runningTime = 0.5f;
                }

            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, 0.2f);
            if (hit)
            {
                print(hit.collider.gameObject.name);
                canJump = true;
            }
        }
    }
}
