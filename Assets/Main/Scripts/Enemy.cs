using UnityEngine;

namespace Bubble
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private Vector2 dashDir;
        [SerializeField] private float dashForce;

        private void Update()
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Dash();
            }

            //agent.SetDestination(target.position);
        }

        private void Dash()
        {
            print("Dash!");
            rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
        }

        private void Movement()
        {
            Vector3 moveDir = target.position - transform.position;

            moveDir.Normalize();

            rb.linearVelocity = moveDir * speed;
        }
    }
}
