using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [System.Serializable]
    public struct enemyType
    {
        public GameObject Enemies;
        public float StartSpawnRange;
        public float EndSpawnRange;
    }
    [Header("Bosses To Spawn")]
    public bool SpawnInstantly = true;

    [System.Serializable]
    public struct BossSettings
    {
        public GameObject Boss;
        public int BossHP;
        public bool changeName;
        public string newBossName;
    }
    private int BossesEliminated = 0;
    public List<BossSettings> BossSettingList;
    public List<Transform> CurrentlyAlive;
    //public List<GameObject> BossList;
    //public List<int> BossHP;



    public float TimeToSpawn = 0f;
    public bool specificSpawnPoint = false;
    public int indexOfSpawnPoint = 0;

    [Header("Colony Stats")]

    //public int enemiesFromColony;
    public float colonyHealth;
    public StatusIndicator StaInd;
    public Camera Mcamera;
    public CameraFollow CamFollow;
    public int SpawnCap;
    [HideInInspector]
    public float timeBetweenSpawns;
    [Header("Spawn time setting")]
    [Tooltip("This is in second")]
    public float MaxTbs;
    public float tbsDecreaseRate;
    public float DecreaseAfter;
    public float MinTbs;

    public float Enemy1DieIncrease;
    public float Enemy2DieIncrease;
    public float Enemy3DieIncrease;

    //int enemiesRemainingToSpawn;
    private float nextSpawnTime;
    private float nextDecrease;
    public List<GameObject> spawnPoints;

    public List<float> DistToSpawnFromPlayer;
    public List<float> spawnPointDistances;

    public enemyType[] EnemyTypes;

    public Animator[] SpawnPointAnim;
    private bool SPAnimReset = false;
    private int ChosenSP;
    private bool bossDeath = false;
    //public int remain;

    bool isWaiting = false;
    bool cutSceneFlag = false;
    private Player player;
    private List<GameObject> SpawnedMobs = new List<GameObject>();

    [Header("Cut Scene Settings")]
    public float xPos;
    public float yPos;

    [Header("Big EnemyUI Settings")]
    public bool enableEnemyUI = false;
    private bool startUILogic = false;
    public float showBigUI = 5;
    [HideInInspector]
    public float BigUITimer = 0;

    // Survival Mode variables
    public bool survivalMode = false;

    public void cleanBossList()
    {
        Debug.Log("CLEANING UP BOSS LIST");
        for (var i = CurrentlyAlive.Count - 1; i > -1; i--)
        {
            if (CurrentlyAlive[i] == null)
            {
                CurrentlyAlive.RemoveAt(i);
            }
        }
    }

    private EnemyColony markedBoss;
    private EnemyColony2 markedBoss2;
    public void resetTimer(EnemyColony EC, EnemyColony2 EC2)
    {
        //Debug.Log("ayo");
        if (startUILogic == true)
        {
            enableEnemyUI = true;
            BigUITimer = showBigUI;

            markedBoss = EC;
            markedBoss2 = EC2;
            


        }
    }

    private bool allBossesDead = false;
    public void checkBossCount()
    {
        BossesEliminated++;
        if (BossesEliminated == BossSettingList.Count)
        {
            //colonyHealth = 0;
            allBossesDead = true;
            //return true;
        }
        Debug.Log(BossesEliminated + " count " + BossSettingList.Count);
        StartCoroutine(startCleanUp());
        //return false;
    }

    private void linkBosses(GameObject go, int index, bool nameChange)
    {
        if (go.TryGetComponent<EnemyColony>(out EnemyColony EC))
        {
            EC.getReferences();
            EC.individualHP = BossSettingList[index].BossHP;
            EC.indivMaxHP = BossSettingList[index].BossHP;
            if (nameChange == true)
            {
                EC.NameOfEnemy = BossSettingList[index].newBossName;
            }
        }
        else if (go.TryGetComponent<EnemyColony2>(out EnemyColony2 EC2))
        {
            EC2.getReferences();
            EC2.individualHP = BossSettingList[index].BossHP;
            EC2.indivMaxHP = BossSettingList[index].BossHP;
            if (nameChange == true)
            {
                EC2.NameOfEnemy = BossSettingList[index].newBossName;
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    WaitForSeconds shortWait = new WaitForSeconds(2.0f);
    public IEnumerator startLogic()
    {
        yield return shortWait;
        startUILogic = true;
    }

    WaitForSeconds shortWait2 = new WaitForSeconds(5.0f);
    public IEnumerator startCleanUp()
    {
        yield return shortWait2;
        Debug.Log("COROUTINE CLEAN UP");
        cleanBossList();
    }

    private void Start()
    {

        colonyHealth = 0;
        for (int i = 0; i < BossSettingList.Count; i++)
        {
            colonyHealth += BossSettingList[i].BossHP;
        }

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPointDistances.Add(0f);
        }

        if (SpawnInstantly)
        {
            if (specificSpawnPoint)
            {
                for (int i = 0; i < BossSettingList.Count; i++)
                {
                    //Instantiate()
                    var go = Instantiate(BossSettingList[i].Boss, spawnPoints[indexOfSpawnPoint].transform.position, Quaternion.identity);
                    go.transform.parent = this.transform;
                    go.transform.localScale = Vector3.one;
                    linkBosses(go, i, BossSettingList[i].changeName);
                    CurrentlyAlive.Add(go.transform);
                    
                }
            }
            else
            {
                for (int i = 0; i < BossSettingList.Count; i++)
                {

                    int randomSpawn = Random.Range(0, spawnPoints.Count);
                    var go = Instantiate(BossSettingList[i].Boss, spawnPoints[randomSpawn].transform.position, Quaternion.identity);
                    go.transform.parent = this.transform;
                    go.transform.localScale = Vector3.one;
                    linkBosses(go, i, BossSettingList[i].changeName);
                    CurrentlyAlive.Add(go.transform);
                }
            }
        }



        //enemiesRemainingToSpawn = enemiesFromColony;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timeBetweenSpawns = MaxTbs;
        StartCoroutine(startLogic());
    }

    private void Update()
    {
        if (GlobalPlayerVariables.EnableAI)
        {
            if (player != null)
            {
                //float[] distances
                //ListGameObject
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    float temp = Vector2.Distance(spawnPoints[i].transform.position, player.transform.position);
                    spawnPointDistances[i] = temp;
                }

                if (/*colonyHealth > 0 &&*/ timeBetweenSpawns > 0 && Time.time > nextDecrease && (timeBetweenSpawns > MinTbs || timeBetweenSpawns < MaxTbs))
                {
                    nextDecrease = Time.time + DecreaseAfter;
                    timeBetweenSpawns -= tbsDecreaseRate;
                    if (timeBetweenSpawns < MinTbs)
                    {
                        timeBetweenSpawns = MinTbs;
                    }
                    if (timeBetweenSpawns > MaxTbs)
                    {
                        timeBetweenSpawns = 1000000;
                    }

                }

                if (timeBetweenSpawns > MaxTbs)
                {
                    tbsDecreaseRate = 0;
                    timeBetweenSpawns = 1000000;
                }
                int randomSpawn = Random.Range(0, spawnPoints.Count);


                if (spawnPointDistances[randomSpawn] <= DistToSpawnFromPlayer[randomSpawn])
                {

                    if (SPAnimReset)
                    {
                        ChosenSP = randomSpawn;
                        SpawnPointAnim[randomSpawn].SetBool("WasChosen", true);
                        SpawnPointAnim[randomSpawn].SetFloat("SpawnRate", 1 / timeBetweenSpawns);
                        SPAnimReset = false;
                    }
                    //Debug.Log(timeBetweenSpawns);
                    if (/*enemiesRemainingToSpawn > 0 &&  colonyHealth > 0 &&*/ Time.time > nextSpawnTime && SpawnedMobs.Count < SpawnCap)
                    {
                        //enemiesRemainingToSpawn--;
                        nextSpawnTime = Time.time + timeBetweenSpawns;

                        //print("randomSpawn = " + randomSpawn);
                        int randomEnemeies = Random.Range(0, 100);
                        //Debug.Log(randomEnemeies);
                        foreach (enemyType et in EnemyTypes)
                        {
                            if (randomEnemeies >= et.StartSpawnRange && randomEnemeies <= et.EndSpawnRange)
                                SpawnedMobs.Add(Instantiate(et.Enemies, spawnPoints[ChosenSP].transform.position, Quaternion.identity));
                        }
                        //Debug.Log("Spawned");

                        SpawnPointAnim[ChosenSP].SetBool("WasChosen", false);
                        SPAnimReset = true;
                        //spawnedEnemy.OnDeath += OnEnemyDeath;
                    }
                }

                StartCoroutine(RemoveDeadMobs());

                if (colonyHealth <= 0 && !bossDeath && !survivalMode || allBossesDead == true && !bossDeath && !survivalMode)
                {
                    colonyHealth = 0;
                    BossDeath();
                    bossDeath = true;
                    StartCoroutine(wait(5));
                }

                if (!isWaiting && bossDeath && !cutSceneFlag && !survivalMode)
                {
                    //StartCoroutine(player.Phasing(4f));
                    StartCoroutine(CutScene(4f));
                    //StartCoroutine(player.TakeOver(4f)); //in progress
                    StartCoroutine(CamFollow.MoveTo(new Vector3(xPos, yPos, -1), 2.8f, 2f));
                    StartCoroutine(CamFollow.ZoomTo(20, 1f));
                    cutSceneFlag = true;
                }

            }
            //Debug.Log(bossDeath);
            if (BigUITimer <= 0)
            {
                enableEnemyUI = false;
            }
            else
            {
                if (markedBoss != null)
                {
                    markedBoss.HealthBar.fillAmount = markedBoss.individualHP / markedBoss.indivMaxHP;
                }
                else if (markedBoss2 != null)
                {
                    markedBoss2.HealthBar.fillAmount = markedBoss2.individualHP / markedBoss2.indivMaxHP;
                }
            }
            BigUITimer -= Time.deltaTime;
        }

    }

    private void BossDeath()
    {
        tbsDecreaseRate = -3*tbsDecreaseRate;
        StaInd.StartShake(Mcamera, 1, 0.5f);
    }

    public void OnEnemyDeath(int type)
    {

        print("Enemy died");
        //enemiesRemainingToSpawn++;
        switch (type)
        {
            case 1:
                if(timeBetweenSpawns < MaxTbs - Enemy1DieIncrease || timeBetweenSpawns > MinTbs + Enemy1DieIncrease)
                    timeBetweenSpawns += Enemy1DieIncrease;
                break;
            case 2:
                if (timeBetweenSpawns < MaxTbs - Enemy2DieIncrease || timeBetweenSpawns > MinTbs + Enemy2DieIncrease)
                    timeBetweenSpawns += Enemy2DieIncrease;
                break;
            case 3:
                if (timeBetweenSpawns < MaxTbs - Enemy3DieIncrease || timeBetweenSpawns > MinTbs + Enemy3DieIncrease)
                    timeBetweenSpawns += Enemy3DieIncrease;
                break;
            default:
                Debug.Log("Unknown type");
                break;
        }
        print("enemies remaing to spawn incremented");
    }

    private IEnumerator wait(float dur)
    {
        isWaiting = true;
        yield return new WaitForSeconds(dur);
        isWaiting = false;
    }

    private IEnumerator RemoveDeadMobs()
    {
        yield return 0;

        SpawnedMobs.RemoveAll(item => item == null);

    }

    private IEnumerator CutScene(float duration)
    {
        GlobalPlayerVariables.EnablePlayerControl = false;
        GlobalPlayerVariables.EnableAI = false;
        yield return new WaitForSeconds(duration);
        GlobalPlayerVariables.EnablePlayerControl = true;
        GlobalPlayerVariables.EnableAI = true;
    }
}
