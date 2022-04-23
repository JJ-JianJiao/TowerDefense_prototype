using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    protected PrefabPool prefabPool;


    public Transform moveTowardsTarget;
    public int numEnemiesWave01;
    public int numEnemiesWave02;
    public int numEnemiesWave03;


    private void Awake ()
    {

        GameObject gameObjectFromScene = GameObject.Find("PrefabPool");
        prefabPool = gameObjectFromScene.GetComponent<PrefabPool>();
	}
    private void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1) {
            SpawnWave01(numEnemiesWave01, 10);
            SpawnWave01(numEnemiesWave02, 15);
        }
        else if (index == 2) {
            SpawnWave01(numEnemiesWave01, 10);
            SpawnWave01(numEnemiesWave02, 15);
            SpawnWave01(numEnemiesWave03, 20);
        }
    }

    protected void SpawnWave01(int numEnemiesWave, int radius)
    {
        Transform [] enemies = new Transform[numEnemiesWave];
        for(int c = 0; c < numEnemiesWave; c++)
        {
            //enemies[c] = prefabPool.Projectile;
            enemies[c] = prefabPool.Enemy;
            enemies[c].GetComponent<EnemyController>().target = moveTowardsTarget;
        }

        //Vector3 centrePos = new Vector3(0, 0, 32);
        Vector3 centrePos = new Vector3(0, 0, 0);
        //place the enemies in a circle
        for (int pointNum = 0; pointNum < numEnemiesWave; pointNum++)
        {
            float i = (pointNum * 1.0f) / numEnemiesWave;
            // get the angle for this step (in radians, not degrees)
            float angle = i * Mathf.PI * 2;
            // the X &amp; Y position for this angle are calculated using Sin &amp; Cos
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;
            Vector3 pos = new Vector3(x, y, 0) + centrePos;
            // no need to assign the instance to a variable unless you're using it afterwards:
            enemies[pointNum].transform.position = pos;
        }

    }
}
