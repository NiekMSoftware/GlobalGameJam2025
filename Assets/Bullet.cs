using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime = 0.3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Die when collide
        try
        {
         // collision.gameObject.GetComponent<BubbleMovement>().Die();
        }
        catch { }
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(KillBullet(BulletLifeTime));
    }

    IEnumerator KillBullet(float _lifeTimeIn)
    {
        yield return new WaitForSeconds(_lifeTimeIn);
        Destroy(gameObject);
    }
}
