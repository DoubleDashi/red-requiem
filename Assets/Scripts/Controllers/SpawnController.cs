using System;
using System.Collections;
using System.Collections.Generic;
using Configs.Events;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SpawnController : MonoBehaviour
    {
        public List<GameObject> enemies;
        public List<GameObject> spawnPoints;
        public int enemiesPerWave = 5;
        public int currentWave = 0;
        public int enemiesSpawned = 0;
        public bool wavePause = false;
        public TMP_Text waitText;

        private void OnEnable()
        {
            KitingEnemyEventConfig.OnDeath += EnemyDefeated;
            MeleeEnemyEventConfig.OnDeath += EnemyDefeated;
        }
        
        private void OnDisable()
        {
            KitingEnemyEventConfig.OnDeath -= EnemyDefeated;
            MeleeEnemyEventConfig.OnDeath -= EnemyDefeated;
        }
        
        private void Start()
        {
            StartNextWave();
            waitText.enabled = false;
        }

        private void Update()
        {
            // Check if all enemies are defeated to start the next wave
            if (enemiesSpawned == 0 && wavePause == false)
            {
                StartCoroutine(WavePause());
                wavePause = true;
            }
        }

        private void StartNextWave()
        {
            currentWave++;
            enemiesPerWave += 1;
            enemiesSpawned = enemiesPerWave * currentWave;
            SpawnEnemies(enemiesSpawned);
        }

        private void SpawnEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Count);
                int enemyIndex = Random.Range(0, enemies.Count);
                Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
            }
        }

        public void EnemyDefeated(Guid guid)
        {
            enemiesSpawned--;
        }

        private IEnumerator WavePause()
        {
            waitText.enabled = true;
            for (int i = 5; i > 0; i--)
            {
                waitText.text = "Next wave in " + i.ToString() + " second(s)";
                yield return new WaitForSeconds(1);
            }
            waitText.enabled = false;
            
            StartNextWave();
            wavePause = false;
        }
    }
}