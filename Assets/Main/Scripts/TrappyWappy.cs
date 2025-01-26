using Bubble.Enemies;
using UnityEngine;

namespace Bubble
{
    public class TrappyWappy : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            print("Something hit trappy wappy");
            try
            {
                other.gameObject.GetComponent<GenericAhEnemy>().Die();
                other.gameObject.GetComponent<Enemy>().Die();
                other.gameObject.GetComponent<TP_Enemy>().Die();
            }
            catch { }
        }
    }
}
