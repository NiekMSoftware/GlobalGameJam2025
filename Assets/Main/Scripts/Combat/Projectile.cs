using Bubble;
using Bubble.Enemies;
using System.Collections;
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
            Instantiate(Particles, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
    }

}
