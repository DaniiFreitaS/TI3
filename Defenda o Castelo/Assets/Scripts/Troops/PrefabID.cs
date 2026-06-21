using UnityEngine;

public class PrefabID : MonoBehaviour
{
    public int ID = 0;

    public void InstantiateAlert(GameObject alertTextPrefab)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GameObject alert = Instantiate(alertTextPrefab, gameObject.transform);
    }
}
