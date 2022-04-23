using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasClickIntercept : EventTrigger
{
    protected PrefabPool prefabPool;
    private void Awake()
    {
        GameObject gameObjectFromScene = GameObject.Find("PrefabPool");
        prefabPool = gameObjectFromScene.GetComponent<PrefabPool>();
    }


    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        if (eventData.button == PointerEventData.InputButton.Left) { 
            Transform projectile = prefabPool.Turret;
            if (projectile != null)
            {
                projectile.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            }

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Transform projectile = prefabPool.Bomb;
            if (projectile != null)
            {
                projectile.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            }
        }
    }
}
