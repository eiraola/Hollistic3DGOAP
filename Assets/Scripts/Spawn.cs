using UnityEngine;


    public class Spawn : MonoBehaviour
    {
    public GameObject spawnPrefab;
    public int numPatient;
    
        private void Start()
        {
            Invoke("SpawnPatient",5);
        }
        public void SpawnPatient()
        {
            Instantiate(spawnPrefab, gameObject.transform.position, Quaternion.identity);
            Invoke("SpawnPatient", 5);
        }
    }
