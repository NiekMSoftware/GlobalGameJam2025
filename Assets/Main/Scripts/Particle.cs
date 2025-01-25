using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestoryParticle());
    }

    IEnumerator DestoryParticle()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().main.duration + 0.5f);
        Destroy(gameObject);
    }

}
