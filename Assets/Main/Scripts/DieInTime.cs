using UnityEngine;

namespace Bubble
{
    public class DieInTime : MonoBehaviour
    {
        [SerializeField] private float deathTime;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Destroy(gameObject,deathTime);
        }
    }
}
