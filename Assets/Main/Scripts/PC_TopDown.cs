using Bubble.Utils;
using UnityEngine;

namespace Bubble.Temp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PC_TopDown : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 input;
        private PIM playerInput;
        
        public float speed;
        [Range(1f, 15f)] public float maxVelocity = 10f;
        [Range(0.1f, 1f)] public float dragForce;

        private void OnEnable()
        {
            playerInput = GetComponent<PIM>();
            
            playerInput.MoveEvent += RetrieveMovement;
        }

        private void OnDisable()
        {
            playerInput.MoveEvent -= RetrieveMovement;
        }

        private void OnDestroy()
        {
            playerInput.MoveEvent -= RetrieveMovement;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
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
        
        private void RetrieveMovement(Vector2 i)
        {
            input = i;
        }
    }
}
