using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float initialForwardAngle = 90; // initial angle of your "gun barrel"
    public float maxRotationSpeed = 60;
    public float averageNumShotsPerSecond = 1;
    public float rotationSpeed = 60;
    public float projectileForce = 1000;
    public float threshold = 4;
    public float bulletTorque = 100;

    protected Transform target;
    protected PrefabPool prefabPool;
    private void Awake()
    {
        prefabPool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }
    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Transform[] enemyShips = prefabPool.EnemiesInPlay;
            if(enemyShips.Length > 0) { 
                int randomShip = Random.Range(0, enemyShips.Length);
                target = enemyShips[randomShip];
            }
        }
        if (target != null && target.gameObject.activeInHierarchy) { 
            RotateGradually2D();
            Shoot();
        }
    }

    protected void Shoot()
    {
        //if averageNumShotsPerSecond is 2, multiplying the inverse (0.5) * 60 gives us 30
        //since Shoot is called 60 times per second...
        int highEndOfRange = (int)(1 / averageNumShotsPerSecond) * 60;
        int random = Random.Range(1, highEndOfRange);
        if (random == 1)
        {
            //instantiate a projectile and set its location
            Transform projectile = prefabPool.Projectile;
            if (projectile != null)
            {
                projectile.position = transform.GetChild(0).position;
                Vector2 projectileDirection = transform.up;
                projectile.GetComponent<Rigidbody2D>().AddForce(projectileDirection * projectileForce);
                projectile.GetComponent<Rigidbody2D>().AddTorque(bulletTorque);
            }
        }
    }

    #region RotateGradually2D()
    //cnobert: adapted this code from https://answers.unity.com/questions/624856/rotate-2d-turret-toward-target-heading-lerpangle.html
    private float currentAngle = 0; // Current angle
    protected void RotateGradually2D()
    {
        float angleToTarget; // Destination angle
        float signToTarget;
        angleToTarget = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        signToTarget = Mathf.Sign(angleToTarget - currentAngle);
        if (Mathf.Abs(angleToTarget - currentAngle) > threshold)
        {
            currentAngle += signToTarget * maxRotationSpeed * Time.deltaTime;
        }
        else
        {
            currentAngle = angleToTarget;
        }
        transform.parent.transform.eulerAngles = new Vector3(0, 0, currentAngle - initialForwardAngle);
    }
    #endregion

}