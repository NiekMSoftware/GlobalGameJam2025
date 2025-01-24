using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private float BulletLifeTime = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            //collision.gameObject.GetComponent<AI>().Die();
        }
        catch { }
        Destroy(gameObject);
    }

    public void Start()
    {
        print(GetComponent<Rigidbody2D>().linearVelocity);

        StartCoroutine(KillBullet());
    }

    IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(BulletLifeTime);
        Destroy(gameObject);
    }
}
