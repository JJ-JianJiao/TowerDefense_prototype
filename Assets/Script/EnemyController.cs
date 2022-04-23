using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed;
    protected GameObject prefabPool;

    private void Awake()
    {
        prefabPool = GameObject.Find("PrefabPool");
    }
    //this example uses sprites from "Spaceship Construction Spirtes Set" on unity asset store
    void Update ()
    {
		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector3 moveDirection = gameObject.transform.position - target.position;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //it's working exactly as I want, except it needs to be flipped by 180 degrees on the z axis.
            transform.RotateAround(transform.position, transform.forward, 180f);
        }

        if (moveDirection == Vector3.zero) {
            prefabPool.GetComponent<PrefabPool>().isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Bullet")) {
            //Debug.Log("You hit me");
            collider.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

    }
}
