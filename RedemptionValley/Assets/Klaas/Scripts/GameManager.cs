using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static event HandleItemPickup OnPickUp;
    public delegate void HandleItemPickup(int Amount, ItemType itemType);


    [Header("Player Info")]
    [SerializeField] private bool isPOneReady;
    [SerializeField] private bool isPTwoRady;
    [SerializeField] private string readyButtonPOne;
    [SerializeField] private string readyButtonPTwo;
    public int points = 0;

    [Header("Enemy's")]
    public GameObject[] allEnemys;
    public Transform[] enemySpawners;
    [SerializeField] private GameObject[] spawnedEnemies;

    [Header("Wave Info")]
    [SerializeField] private int currentWave = 1;
    // [SerializeField] private int enemyWaveCount = 6;
    public bool spawningFinished = false;

    [Header("Wave Spawning")]
    [SerializeField] float spawnDelay = 3;
    public float breakTimer = 6;
    public float breakMaxTimer = 60;
    public TextMeshProUGUI breakText;
    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Death UI")]
    [SerializeField] private Image deathImage;
    [SerializeField] private TextMeshProUGUI waveSurvivedText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Button restartButton;

    [Header("Wave Rewards")]
    public int moneyReward = 100;
    public int skillPointReward = 1;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += PlayerDeath;
        Enemy.OnEnemyDeath += AddPoints;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= PlayerDeath;
        Enemy.OnEnemyDeath -= AddPoints;
    }

    private void Awake()
    {

        try
        {
            if (PlayerPrefs.GetInt("hasBeenSaved") != 1)
            {
                SaveBaseEnemyStats();
            }
            else
            {
                SetBaseEnemyStats();
            }
        }
        catch { SaveBaseEnemyStats(); }


    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnEnemy());
    }

    // bool hasEnemyBeenScaled = false;

    // Update is called once per frame
    private void Update()
    {
        if (spawningFinished == true && areSpawnersEmpty() == true)
        {

            //if(!hasEnemyBeenScaled)
            //{
            //    for (int i = 0; i < allEnemys.Length; i++)
            //    {
            //        allEnemys[i].GetComponent<Enemy>().ScaleEnemy();
            //    }
            //    hasEnemyBeenScaled=true;
            //}
            if (!breakTimerFadedIn)
            {
                CheckCurrentWave();
                StartCoroutine(FadeInBreakTimer());
            }
            breakText.text = Mathf.Round(breakTimer).ToString();
            breakTimer -= Time.deltaTime;
            if (breakTimer <= 0 || isPOneReady)
            {
                StartCoroutine(FadeBreakTimer());

                points += 10;
                OnPickUp?.Invoke(moneyReward, ItemType.Gold);
                OnPickUp?.Invoke(skillPointReward, ItemType.SkillPoint);
                // hasEnemyBeenScaled = false;
                isPOneReady = false;
                isPTwoRady = false;
                currentWave++;
                waveToUpgrade = currentWave + 2;
                CheckCurrentWave();
                StartCoroutine(SpawnEnemy());
                breakTimer = breakMaxTimer;

            }
        }

        pauseSpawning();

        if (Input.GetKeyDown(readyButtonPOne))
        {
            isPOneReady = true;
        }
        if (Input.GetKeyDown(readyButtonPTwo))
        {
            isPTwoRady = true;
        }

    }
    [Header("Break Timer UI")]
    [SerializeField] private Image breakImageOne;
    [SerializeField] private Image breakImageTwo;

    bool breakTimerFadedIn;
    public IEnumerator FadeBreakTimer()
    {
        breakTimerFadedIn = false;


        float duration = 1.0f;
        float elapsedTime = 0f;


        Color startColorOne = breakImageOne.color;
        Color startColorTwo = breakImageTwo.color;
        Color startTextColor = breakText.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);


            breakImageOne.color = new Color(startColorOne.r, startColorOne.g, startColorOne.b, alpha);
            breakImageTwo.color = new Color(startColorTwo.r, startColorTwo.g, startColorTwo.b, alpha);


            breakText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, alpha);

            yield return null;
        }

        breakImageOne.color = new Color(startColorOne.r, startColorOne.g, startColorOne.b, 0f);
        breakImageTwo.color = new Color(startColorTwo.r, startColorTwo.g, startColorTwo.b, 0f);
        breakText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, 0f);

    }
    public IEnumerator FadeInBreakTimer()
    {
        breakTimerFadedIn = true;

        float duration = 1.0f;
        float elapsedTime = 0f;

        Color startColorOne = breakImageOne.color;
        Color startColorTwo = breakImageTwo.color;
        Color startTextColor = breakText.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);


            breakImageOne.color = new Color(startColorOne.r, startColorOne.g, startColorOne.b, alpha);
            breakImageTwo.color = new Color(startColorTwo.r, startColorTwo.g, startColorTwo.b, alpha);


            breakText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, alpha);

            yield return null;
        }


        breakImageOne.color = new Color(startColorOne.r, startColorOne.g, startColorOne.b, 1f);
        breakImageTwo.color = new Color(startColorTwo.r, startColorTwo.g, startColorTwo.b, 1f);
        breakText.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, 1f);


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
        //CheckCurrentWave();

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
        else { return false; }
    }
    [Header("Wave Based Info")]
    [SerializeField] int waveToUpgrade;
    [SerializeField] GameObject[] shopArrows;
    [SerializeField] Tilemap walls;
    [SerializeField] Vector3Int[] doorPositions;
    public void CheckCurrentWave()
    {
        switch (currentWave)
        {
            case 1:
                try
                {
                    for (int i = 0; i < doorPositions.Length; i++) { walls.SetTile(doorPositions[i], null); }

                    for (int i = 0; i < shopArrows.Length; i++) { shopArrows[i].SetActive(true); }
                }
                catch { }
                break;
            case 5: maxChance = 70;
                spawnDelay = 2;
                ScaleEnemysPerRounds();
                break;
            case 8: maxChance = 95;
                spawnDelay = 0.5f;
                ScaleEnemysPerRounds();
                break;
            case 12: maxChance = 100;
                ScaleEnemysPerRounds();
                spawnDelay = 0.2f;
                waveToUpgrade = currentWave + 2;

                break;
        }

        if (waveToUpgrade == currentWave)
        {
            ScaleEnemysPerRounds();
        }
    }
    public void ScaleEnemysPerRounds()
    {
        for (int i = 0; i < allEnemys.Length; i++)
        {
            allEnemys[i].GetComponent<Enemy>().ScaleEnemy();
        }
    }
    public bool areSpawnersEmpty()
    {
        if (enemySpawners[0].childCount == 0 && enemySpawners[1].childCount == 0 && enemySpawners[2].childCount == 0)
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
        //  print("Reached");
        points += pointsAdded;
    }

    public void PlayerDeath()
    {
        /*
         * Add Death Animation
         * 
         */
        GameObject.FindObjectOfType<EventSystem>().SetSelectedGameObject(restartButton.gameObject);
        pointsText.text += points;
        waveSurvivedText.text += currentWave -= 1;
        deathImage.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void SaveBaseEnemyStats()
    {
        for (int i = 0; i < allEnemys.Length; i++)
        {
            //  allEnemys[i].
            PlayerPrefs.SetFloat(allEnemys[i].name + "-damge", allEnemys[i].GetComponent<Enemy>().damage);
            PlayerPrefs.SetFloat(allEnemys[i].name + "-health", allEnemys[i].GetComponent<Enemy>().health);

            PlayerPrefs.SetInt("hasBeenSaved", 1);

            Debug.Log($"{allEnemys[i].name} Damage: {PlayerPrefs.GetFloat(allEnemys[i].name + "-damge")}\n {allEnemys[i].name} Health: {PlayerPrefs.GetFloat(allEnemys[i].name + "-health")}");
        }
    }
    public void SetBaseEnemyStats()
    {
        for (int i = 0; i < allEnemys.Length; i++)
        {
            //  allEnemys[i].
            allEnemys[i].GetComponent<Enemy>().damage = PlayerPrefs.GetFloat(allEnemys[i].name + "-damge");

            allEnemys[i].GetComponent<Enemy>().health = PlayerPrefs.GetFloat(allEnemys[i].name + "-health");

        }

    }

    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(int scene)
    {
       SceneManager.LoadScene(scene);
    }

}
