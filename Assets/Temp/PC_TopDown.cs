using UnityEngine;

namespace Bubble.Temp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PC_TopDown : MonoBehaviour
    {
        private Vector2 input;
        private Rigidbody2D rb;

        public float speed;

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
            rb.AddForce(input * speed, ForceMode2D.Force);
        }
        
        private void GetMovement()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
