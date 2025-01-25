using Bubble;
using Bubble.Enemies;
using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Projectile : MonoBehaviour
{
    public GameObject Owner;
    public bool isEnemyBullet;

    public int enemyBulletLayer;
    public int playerBulletLayer;

    public float damage;
    public bool hasHitEnemy;

    [Tooltip("Pwefwab nweeded :3")]
    public GameObject Particles; 

    [SerializeField] private float BulletLifeTime = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(hasHitEnemy);

        if (!hasHitEnemy && collision.TryGetComponent(out Player _) && isEnemyBullet)
        {
            //hasHitEnemy = true;

            print("Bullet uneffective: " + collision.name);

            //Destroy(gameObject);
            //GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            //collision.gameObject.GetComponent<AI>().Die();
        }
        catch { }

        if (true/*!hasHitEnemy && ((collision.transform.TryGetComponent(out Player _) && isEnemyBullet) || (collision.transform.TryGetComponent(out GenericAhEnemy _) && !isEnemyBullet))*/)
        {
            print("Bullet destroyed: " + collision.collider.name);

            Destroy(gameObject);
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

    private void OnDestroy()
    {
        try
        {
            Instantiate(Particles, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
        }
        catch { }
    }

}
