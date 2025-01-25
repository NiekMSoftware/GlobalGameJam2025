using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isEnemyBullet;

    public int enemyBulletLayer;
    public int playerBulletLayer;

    public float damage;

    [SerializeField] private float BulletLifeTime = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            //collision.gameObject.GetComponent<AI>().Die();
        }
        catch { }

        print("Bullet destroyed: " + collision.collider.name);

        Destroy(gameObject);
    }

    public void Start()
    {
        gameObject.layer = isEnemyBullet ? enemyBulletLayer : playerBulletLayer;

        StartCoroutine(KillBullet());
    }

    IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(BulletLifeTime);
        Destroy(gameObject);
    }
}
