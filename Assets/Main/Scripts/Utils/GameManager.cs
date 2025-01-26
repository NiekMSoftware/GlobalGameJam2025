using System.Collections;
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

        [SerializeField] private int shotsFired = 0;

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
            
            if (_tickTimer >= Tick)
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
                Debug.Log("Woah you won!");
            }
        }

        private void CheckShots()
        {
            shotsFired = playerShoot.ShotsFired;
        }
    }
}
