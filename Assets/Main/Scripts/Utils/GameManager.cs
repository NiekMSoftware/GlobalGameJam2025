using System.Collections.Generic;
using Bubble.Enemies;
using UnityEngine;

namespace Bubble.Utils
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GenericAhEnemy> enemies;
        [SerializeField] private PlayerShoot playerShoot;
        
        [field: SerializeField] public float Tick { get; private set; }
        private float _tickTimer;

        [SerializeField] private int shotsFired;

        [Header("Score schtuff.")] 
        [SerializeField] private GameObject starContainer;
        [SerializeField] private GameObject[] goldenStars;
        [SerializeField] private GameObject[] whiteStars;
        
        [SerializeField] private int perfectScore;
        [SerializeField] private int mediumScore;
        [SerializeField] private int lowScore;

        [Header("Endless Properties")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float usualOffset = 15f;
        [field: SerializeField] public bool IsEndless { get; private set; }
        [SerializeField] private float spawnInterval = 2.1f;
        [SerializeField] private List<GameObject> enemyPrefabs;

        private bool _gameOver;
        private float _timer;

        private void Start()
        {
            enemies = new List<GenericAhEnemy>(
                FindObjectsByType<GenericAhEnemy>((FindObjectsSortMode)FindObjectsInactive.Exclude));

            playerShoot = FindFirstObjectByType<PlayerShoot>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            TickGame();
        }
        
        private void TickGame()
        {
            _tickTimer += Time.deltaTime;
            
            if (_tickTimer >= Tick && !_gameOver)
            {
                _tickTimer -= Tick;
                CheckEnemies();
                CheckCondition();
                CheckShots();
                SpawnBoisEndlessly();
            }
        }

        private void CheckEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void CheckCondition()
        {
            if (enemies.Count == 0 && !IsEndless)
            {
                starContainer.SetActive(true);
                ShowScore();
                _gameOver = true;
            }
        }

        private void CheckShots()
        {
            shotsFired = playerShoot.ShotsFired;
        }

        public void ShowScore()
        {
            foreach (GameObject star in goldenStars)
            {
                star.SetActive(false);
            }
            
            foreach (GameObject t in whiteStars)
            {
                t.SetActive(false);
            }
            
            if (shotsFired <= perfectScore)
            {
                foreach (GameObject star in goldenStars)
                {
                    star.SetActive(true);
                }
            }
            else if (shotsFired <= mediumScore)
            {
                goldenStars[0].SetActive(true);
                goldenStars[2].SetActive(true);
                
                whiteStars[1].SetActive(true);
            }
            else if (shotsFired <= lowScore)
            {
                goldenStars[0].SetActive(true);
                
                whiteStars[1].SetActive(true);
                whiteStars[2].SetActive(true);
            }
            else
            {
                foreach (GameObject t in whiteStars)
                {
                    t.SetActive(true);
                }
            }
        }

        void SpawnBoisEndlessly()
        {
            if (!IsEndless) return;

            _timer += Time.deltaTime;

            if (_timer >= spawnInterval)
            {
                // Find the main camera's position and size
                Vector2 cameraPosition = mainCamera.transform.position;
                float cameraHeight = 2f * mainCamera.orthographicSize;
                float cameraWidth = cameraHeight * mainCamera.aspect;

                // Generate a random position outside the camera view
                float spawnX, spawnY;

                // Randomly decide which edge of the screen to spawn on (top, bottom, left, or right)
                int edge = Random.Range(0, 4); // 0: Top, 1: Bottom, 2: Left, 3: Right
                switch (edge)
                {
                    case 0: // Top
                        spawnX = Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2);
                        spawnY = cameraPosition.y + cameraHeight / 2 + usualOffset;
                        break;
                    case 1: // Bottom
                        spawnX = Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2);
                        spawnY = cameraPosition.y - cameraHeight / 2 - usualOffset;
                        break;
                    case 2: // Left
                        spawnX = cameraPosition.x - cameraWidth / 2 - usualOffset;
                        spawnY = Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2);
                        break;
                    case 3: // Right
                        spawnX = cameraPosition.x + cameraWidth / 2 + usualOffset;
                        spawnY = Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2);
                        break;
                    default:
                        spawnX = 0;
                        spawnY = 0;
                        break;
                }

                Vector2 spawnPosition = new Vector2(spawnX, spawnY);

                // Randomly select an enemy prefab from the list
                if (enemyPrefabs.Count > 0) 
                {
                    int randomIndex = Random.Range(0, enemyPrefabs.Count);
                    GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];

                    // Instantiate the enemy prefab at the calculated spawn position
                    Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
                }

                _timer = 0f;
            }
        }
        
        public int GetBulletsShot() => shotsFired;
    }
}
