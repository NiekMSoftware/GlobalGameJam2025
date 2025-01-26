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

        private bool _gameOver;

        private void Start()
        {
            enemies = new List<GenericAhEnemy>(
                FindObjectsByType<GenericAhEnemy>((FindObjectsSortMode)FindObjectsInactive.Exclude));

            playerShoot = FindFirstObjectByType<PlayerShoot>();
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
            if (enemies.Count == 0)
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
        
        public int GetBulletsShot() => shotsFired;
    }
}
