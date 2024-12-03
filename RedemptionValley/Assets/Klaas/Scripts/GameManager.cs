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
    public int points = 0;

    [Header("Enemy's")]
    public GameObject[] allEnemys;
    public Transform[] enemySpawners;
    [SerializeField] private GameObject[] spawnedEnemies;

    [Header("Wave Info")]
    [SerializeField] private int currentWave =1;
   // [SerializeField] private int enemyWaveCount = 6;
    public bool spawningFinished = false;

    [Header("Wave Spawning")]
    [SerializeField] int spawnDelay = 3;
    public float breakTimer = 6;
    public float breakMaxTimer = 60;

    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Death UI")]
    [SerializeField] private Image deathImage;
    [SerializeField] private TextMeshProUGUI waveSurvivedText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += PlayerDeath;
        Enemy.OnEnemyDeath += AddPoints;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= PlayerDeath;
    }
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
            points += 10;
           breakTimer -= Time.deltaTime;
            if(breakTimer <= 0 || isPOneReady)
            {
                isPOneReady = false;
                currentWave++;
                StartCoroutine(SpawnEnemy());
                breakTimer = breakMaxTimer;
            }
        }

        pauseSpawning();

        if(Input.GetKeyDown(KeyCode.K))
        {
            isPOneReady=true;
        }

    }

    public IEnumerator SpawnEnemy()
    {
        UpdateWaveText();
       
 
        Debug.Log($"Current Wave= {currentWave}");
        spawningFinished = false;
       // spawnedEnemies.
        for (int i = 0; i < maxEnemyCount; i++)
        {
            int rand2 = Random.Range(0, enemySpawners.Length);
            // Instantiate(allEnemys[rand], enemySpawners[rand2]);         
                    int enemyChance = Random.Range(0, maxChance);
                    switch (enemyChance)
                    {
                        case int k when k <= 45:
                            //Debug.LogWarning($"Number:{enemyChance} - Zombie");
                            Instantiate(allEnemys[0], enemySpawners[rand2]);
                            // Zombie
                            break;

                        case int k when k <= 65:
                           //Debug.LogWarning($"Number:{enemyChance} - Mage");
                        Instantiate(allEnemys[1], enemySpawners[rand2]);
                        break;

                        case int k when k <= 80:
                          // Debug.LogWarning($"Number:{enemyChance} - Ghoul");
                        Instantiate(allEnemys[2], enemySpawners[rand2]);
                        break;

                        case int k when k <= 90:
                           // Debug.LogWarning($"Number:{enemyChance} - Bezerk");
                        Instantiate(allEnemys[3], enemySpawners[rand2]);
                        break;

                        case int k when k <= 100:
                          //  Debug.LogWarning($"Number:{enemyChance} - Shadow Walker");
                        Instantiate(allEnemys[4], enemySpawners[rand2]);
                        break;

                        default:
                            Instantiate(allEnemys[0], enemySpawners[rand2]);
                            Debug.LogWarning($"Number:{enemyChance} - Zombie");
                            break;

                    }
            spawnedInEnemys++;
            yield return new WaitForSeconds(spawnDelay);
          
           // yield return new WaitWhile(() => spawnedInEnemys >);
          //  yield return toManyEnemys;
        }
        spawnedInEnemys = 0;
        Debug.Log("Spawning ended");
        maxEnemyCount = maxEnemyCount * 3;
        Debug.Log(maxEnemyCount);
        spawningFinished = true;
    }
    int spawnedInEnemys;

    int maxEnemyCount = 6;
   // bool toManyEnemys = false; 

    public int maxChance = 50;

    public bool pauseSpawning()
    {
        if (spawnedInEnemys > 150)
        {
           return true;

        }
        else {return false; }
    }

    public void CheckCurrentWave()
    {
        switch(currentWave)
        {
            case 5: maxChance = 70; 
                break;
            case 8: maxChance = 95;
                break;
            case 12: maxChance = 100;
                break;
        }
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

    public void AddPoints(int pointsAdded)
    {
        print("Reached");
        points += pointsAdded;
    }

    public void PlayerDeath()
    {
        /*
         * Add Death Animation
         * 
         */
        pointsText.text += points;
        waveSurvivedText.text += currentWave -= 1;
        deathImage.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
}
