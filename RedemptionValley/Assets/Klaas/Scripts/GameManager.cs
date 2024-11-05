using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private bool isPOneReady;
    [SerializeField] private bool isPTwoRady;

    [Header("Enemy's")]
    public GameObject[] allEnemys;
    public Transform[] enemySpawners;
    [SerializeField] private GameObject[] spawnedEnemies;

    [Header("Wave Info")]
    [SerializeField] private int currentWave =1;
    [SerializeField] private int enemyWaveCount = 6;
    public bool spawningFinished = false;

    [Header("Wave Spawning")]
    [SerializeField] int spawnDelay = 3;
    public float breakTimer = 6;
    public float breakMaxTimer = 60;

    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawningFinished == true && areSpawnersEmpty() == true)
         {
           breakTimer -= Time.deltaTime;
            if(breakTimer <= 0)
            {
               
                currentWave++;
                StartCoroutine(SpawnEnemy());
                breakTimer = breakMaxTimer;
            }
         }
         
    }

    public IEnumerator SpawnEnemy()
    {
        UpdateWaveText();
        Debug.Log($"Current Wave= {currentWave}");
        spawningFinished = false;
       // spawnedEnemies.
        for (int i = 0; i < enemyWaveCount; i++)
        {
            int rand = Random.Range(0, allEnemys.Length);
            int rand2 = Random.Range(0, enemySpawners.Length);
            Instantiate(allEnemys[rand], enemySpawners[rand2]);        
              
            yield return new WaitForSeconds(spawnDelay);

        }
        Debug.Log("Spawning ended");
        spawningFinished = true;
    }
  
    public bool areSpawnersEmpty()
    {
        if (enemySpawners[0].childCount == 0 && enemySpawners[1].childCount == 0 && enemySpawners[2].childCount == 0 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateWaveText()
    {
        waveText.text = currentWave.ToString();
    }
}
