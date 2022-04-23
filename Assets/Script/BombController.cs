using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    protected float growUpSpeed = 0.05f;
    protected bool destorySelf = false;
    protected Vector3 originalSize;
    // Start is called before the first frame update
    void Start()
    {
        originalSize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += new Vector3(growUpSpeed, growUpSpeed, growUpSpeed);
        if (this.transform.localScale.x > 10) {
            this.transform.localScale = originalSize;
            this.gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Enemy")) {
            //Debug.Log("I touch the Bomb");
            this.gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
        }
    }
}
