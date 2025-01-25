using UnityEngine;

namespace Bubble
{
    public class Melee : MonoBehaviour
    {
        [SerializeField] private Transform MeleePos;
        [SerializeField] private Vector2 MeleeSize;
        [SerializeField] private GameObject MeleeEffect;
        [SerializeField] private float MeleeEffectTime;

        private float MeleeEffectTimer;

        private GameObject spawnedMeleeEffect;

        private void OnEnable()
        {
            MeleeEffectTimer = MeleeEffectTime;
        }

        public void MeleeAttack()
        {
            if (spawnedMeleeEffect) Destroy(spawnedMeleeEffect);

            print(transform.position);
            spawnedMeleeEffect = Instantiate(MeleeEffect, transform.position, Quaternion.identity);
            spawnedMeleeEffect.transform.position = transform.position;
            Physics.CheckBox(transform.position, MeleeSize, Quaternion.identity);
        }

        private void Update()
        {
            MeleePos.position = transform.position;

            if (spawnedMeleeEffect)
            {
                MeleeEffectTimer -= Time.deltaTime;

                if (MeleeEffectTimer <= 0)
                {
                    Destroy(spawnedMeleeEffect);
                }
            }
            else
            {
                MeleeEffectTimer = MeleeEffectTime;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(MeleePos.position, MeleeSize);
        }
    }
}
