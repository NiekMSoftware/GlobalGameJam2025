using Bubble;
using Bubble.Enemies;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isEnemyBullet;

    public int enemyBulletLayer;
    public int playerBulletLayer;

    public float damage;

    private bool hasHitEnemy;

    [SerializeField] private float BulletLifeTime = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            //collision.gameObject.GetComponent<AI>().Die();
        }
        catch { }

        if (!hasHitEnemy && (collision.transform.TryGetComponent(out Player _) && isEnemyBullet) || (collision.transform.TryGetComponent(out GenericAhEnemy _) && !isEnemyBullet))
        {
            print("Bullet destroyed: " + collision.collider.name);

            Destroy(gameObject);

            hasHitEnemy = true;
        }      
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
