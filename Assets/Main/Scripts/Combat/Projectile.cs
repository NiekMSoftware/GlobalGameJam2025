using Bubble;
using Bubble.Enemies;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Owner;
    public bool isEnemyBullet;
    public int enemyBulletLayer;
    public int playerBulletLayer;
    public float damage;

    [Tooltip("Pwefwab nweeded :3")]
    public GameObject Particles; 

    [SerializeField] private float BulletLifeTime = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<GenericAhEnemy>().Die();
            collision.gameObject.GetComponent<Enemy>().Die();
            collision.gameObject.GetComponent<TP_Enemy>().Die();
        }
        catch { }
        Destroy(gameObject);
    }

    public void Start()
    {
        StartCoroutine(KillBullet());
    }

    IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(BulletLifeTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Particles != null)
            Instantiate(Particles, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), transform.rotation);
    }

}
