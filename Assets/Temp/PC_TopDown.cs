using UnityEngine;

namespace Bubble.Temp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PC_TopDown : MonoBehaviour
    {
        private Vector2 input;
        private Rigidbody2D rb;

        public float speed;
        [Range(1f, 15f)] public float maxVelocity = 10f;
        [Range(0.1f, 1f)] public float dragForce;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        private void Update()
        {
            GetMovement();
        }

        private void FixedUpdate()
        {
            if (input == Vector2.zero)
            {
                rb.linearVelocity -= rb.linearVelocity.normalized * dragForce;
                if (rb.linearVelocity.magnitude < 0.1f)
                {
                    rb.linearVelocity = Vector2.zero;
                }

                return;
            }
            
            rb.AddForce(input * speed, ForceMode2D.Impulse);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
        
        private void GetMovement()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
