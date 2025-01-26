using UnityEngine;

namespace Bubble
{
    public class LevelScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] levels;
        [SerializeField] private Transform[] levelSpots;
        [SerializeField] private Transform cam;
        [SerializeField] private LevelReferences currentLevel;
        int current = 0;
        void Start()
        {
            SpawnLevel();
        }
        void Update()
        {
            if(currentLevel.enemyParent.transform.childCount < 1)
            {
                if (current < levels.Length)
                {
                    KillLevel();
                    SpawnLevel();
                }
                else
                {
                    print("I hate you.");
                }
            }
        }

        public void SpawnLevel()
        {
            GameObject lvl = Instantiate(levels[current], levelSpots[current].position,Quaternion.identity);
            currentLevel = lvl.GetComponent<LevelReferences>();
            cam.position = new Vector3(levelSpots[current].position.x, levelSpots[current].position.y, -10);
            current++;
        }

        public void KillLevel()
        {
            Destroy(currentLevel.gameObject);
        }
    }
}
