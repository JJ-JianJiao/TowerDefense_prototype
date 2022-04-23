using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Bullet")) {
            //Debug.Log("You hit the deadZone");
            collider.gameObject.SetActive(false);
        }
    }
}
