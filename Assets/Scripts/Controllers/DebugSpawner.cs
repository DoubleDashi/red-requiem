using UnityEngine;

namespace Controllers
{
    public class DebugSpawner : MonoBehaviour
    {
        public GameObject stationaryEnemyPrefab;
        public GameObject meleeEnemyPrefab;
        public GameObject kitingEnemyPrefab;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Instantiate(stationaryEnemyPrefab, transform.position, Quaternion.identity);
                } 
                
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Instantiate(meleeEnemyPrefab, transform.position, Quaternion.identity);
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Instantiate(kitingEnemyPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}