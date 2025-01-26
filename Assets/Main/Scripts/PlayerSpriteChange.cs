using UnityEngine;

namespace Bubble
{
    public class PlayerSpriteChange : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer player;
        [SerializeField] private Sprite[] faces;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //print(rb.linearVelocity + "Hi");

            if(rb.linearVelocity != Vector2.zero)
            {
                ChangeSprite();
            }
        }

        private void ChangeSprite()
        {
            if(rb.linearVelocity.x < -3)
            {
                player.sprite = faces[0];
            }
            if (rb.linearVelocity.x > 3)
            {
                player.sprite = faces[1];
            }
            if (rb.linearVelocity.y < -3)
            {
                player.sprite = faces[2];
            }
            if (rb.linearVelocity.y > 3)
            {
                player.sprite = faces[3];
            }
        }
    }
}
