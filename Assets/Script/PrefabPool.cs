using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrefabPool : MonoBehaviour
{

    public GameObject displayMsg;
    public bool levelOne = false;
    public bool levelTwo = false;
    public bool nextLevelStep = false;
    public bool start = true;
    public bool gameOver = false;
    public bool isDead = false;
    //public bool StartLevelOne = true;
    private void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
        {
            levelOne = true;
            levelTwo = false;
        }
        else if (index == 2) {
            levelOne = false;
            levelTwo = true;
        }
    }
    private void Awake()
    {
        displayMsg = GameObject.Find("DisplayMsg");
        InitializeProjectiles();
        InitializeEnemies();
        InitializeTurret();
        InitializeBomb();
    }

    private void Update()
    {
        if (start && !levelTwo && levelOne)
        {
            displayMsg.GetComponent<Text>().text = "Level One!";
            displayMsg.GetComponent<Text>().color = Color.red;
            Invoke("DisableDisplay", 2);
            start = false;
        }
        else if (start && levelTwo && !levelOne)
        {
            displayMsg.GetComponent<Text>().text = "Level Two!";
            displayMsg.GetComponent<Text>().color = Color.red;
            Invoke("DisableDisplay", 2);
            start = false;
        }


        if (EnemiesInPlay.Length == 0 && levelOne) {
            displayMsg.SetActive(true);
            displayMsg.GetComponent<Text>().color = Color.red;
            displayMsg.GetComponent<Text>().text = "You Win!";
            levelOne = false;
            nextLevelStep = true;
            //levelTwo = true;

            Invoke("DisableDisplay", 2);
        }

        if (EnemiesInPlay.Length == 0 && levelTwo)
        {
            displayMsg.SetActive(true);
            displayMsg.GetComponent<Text>().color = Color.red;
            displayMsg.GetComponent<Text>().text = "You Pass this game!";
            levelTwo = false;
            nextLevelStep = true;
            gameOver = true;
            Invoke("DisableDisplay", 2);
        }

        //if (gameOver && !levelOne && !levelTwo && !nextLevelStep)
        //{
        //    displayMsg.SetActive(true);
        //    displayMsg.GetComponent<Text>().color = Color.red;
        //    displayMsg.GetComponent<Text>().text = "you lose!";
        //    //NextLevel = true;

        //    Invoke("DisableDisplay", 1);
        //}

        if (isDead)
        {
            displayMsg.SetActive(true);
            displayMsg.GetComponent<Text>().color = Color.red;
            displayMsg.GetComponent<Text>().text = "you lose!";

            Invoke("DisableDisplay", 1);
        }
    }
    private void DisableDisplay()
    {
        displayMsg.SetActive(false);
        //if (gameOver && !nextLevelStep) {
        //    SceneManager.LoadScene(0);
        //}
        if (!levelOne && !levelTwo && nextLevelStep && !gameOver)
        {
            SceneManager.LoadScene(2);
        }
        if (!levelOne && !levelTwo && nextLevelStep && gameOver)
        {
            SceneManager.LoadScene(0);
        }
        if (isDead)
        {
            SceneManager.LoadScene(0);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public int numTurretInScene;
    public Transform turretPrefab;
    protected Transform[] turretPrefabPool = new Transform[0];

    public int numBombInScene;
    public Transform bombPrefab;
    protected Transform[] bombPrefabPool = new Transform[0];

    public int numEnemiesInScene;
    public Transform enemyPrefab;
    protected Transform[] enemyPrefabPool = new Transform[0];

    public int numPlayerProjectilesInScene;
    public Transform projectilePrefab;
    protected Transform[] projectilePool = new Transform[0];


    public void InitializeProjectiles()
    {
        if (projectilePool.Length == 0)
        {
            projectilePool = new Transform[numPlayerProjectilesInScene];
            for (int c = 0; c < numPlayerProjectilesInScene; c++)
            {
                projectilePool[c] = Instantiate(projectilePrefab);
                projectilePool[c].gameObject.SetActive(false);
            }
        }
    } 
    public void InitializeEnemies()
    {
        if (enemyPrefabPool.Length == 0)
        {
            enemyPrefabPool = new Transform[numEnemiesInScene];
            for (int c = 0; c < numEnemiesInScene; c++)
            {
                enemyPrefabPool[c] = Instantiate(enemyPrefab);
                enemyPrefabPool[c].gameObject.SetActive(false);
            }
        }
    }

    public void InitializeTurret()
    {
        if (turretPrefabPool.Length == 0)
        {
            turretPrefabPool = new Transform[numTurretInScene];
            for (int c = 0; c < numTurretInScene; c++)
            {
                turretPrefabPool[c] = Instantiate(turretPrefab);
                turretPrefabPool[c].gameObject.SetActive(false);
            }
        }
    }

    public void InitializeBomb()
    {
        if (bombPrefabPool.Length == 0)
        {
            bombPrefabPool = new Transform[numBombInScene];
            for (int c = 0; c < numBombInScene; c++)
            {
                bombPrefabPool[c] = Instantiate(bombPrefab);
                bombPrefabPool[c].gameObject.SetActive(false);
            }
        }
    }

    public Transform Projectile
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            while (c < projectilePool.Length && returnTransform == null)
            {
                if (!projectilePool[c].gameObject.activeInHierarchy)
                {
                    returnTransform = projectilePool[c];
                    projectilePool[c].gameObject.SetActive(true);
                }
                c++;
            }
            return returnTransform;
        }
    }

    public Transform Enemy
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            while (c < enemyPrefabPool.Length && returnTransform == null)
            {
                if (!enemyPrefabPool[c].gameObject.activeInHierarchy)
                {
                    returnTransform = enemyPrefabPool[c];
                    enemyPrefabPool[c].gameObject.SetActive(true);
                }
                c++;
            }
            return returnTransform;
        }
    }

    public Transform Turret
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            while (c < turretPrefabPool.Length && returnTransform == null)
            {
                if (!turretPrefabPool[c].gameObject.activeInHierarchy)
                {
                    returnTransform = turretPrefabPool[c];
                    turretPrefabPool[c].gameObject.SetActive(true);
                }
                c++;
            }
            return returnTransform;
        }
    }

    public Transform Bomb
    {
        get
        {
            Transform returnTransform = null;
            int c = 0;
            while (c < bombPrefabPool.Length && returnTransform == null)
            {
                if (!bombPrefabPool[c].gameObject.activeInHierarchy)
                {
                    returnTransform = bombPrefabPool[c];
                    bombPrefabPool[c].gameObject.SetActive(true);
                }
                c++;
            }
            return returnTransform;
        }
    }

    public Transform[] EnemiesInPlay
    {
        get {
            List<Transform> enemiesInPlay = new List<Transform>();
            foreach (Transform enemy in enemyPrefabPool) {
                if (enemy.gameObject.activeInHierarchy) {
                    enemiesInPlay.Add(enemy);
                }
            }
            return enemiesInPlay.ToArray();
        }
    }
}
