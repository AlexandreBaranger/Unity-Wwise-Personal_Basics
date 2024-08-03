using UnityEngine;
using System.Collections.Generic;
public class WwiseInstanceZone : MonoBehaviour
{
    public GameObject gameObjectToSync;
    private Vector3 previousPosition;
    public WwiseInstanceData[] prefabDataArray;
    public float sphereRadius = 1.0f;
    private Dictionary<string, bool> instanceFlags = new Dictionary<string, bool>();
    private Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> enterFlags = new Dictionary<string, bool>();
    private void Start()
    {
        if (gameObjectToSync != null)
        {previousPosition = gameObjectToSync.transform.position;}
    }
    private void Update()
    {
        if (gameObjectToSync != null)
        {
            Vector3 velocity = (gameObjectToSync.transform.position - previousPosition) / Time.deltaTime;
            transform.position = gameObjectToSync.transform.position;
            List<string> objectsToRemove = new List<string>();
            foreach (WwiseInstanceData data in prefabDataArray)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);
                bool found = false;
                foreach (Collider collider in colliders)
                {
                    GameObject obj = collider.gameObject;
                    if (obj.name == data.prefabName)
                    {
                        found = true;
                        if (!enterFlags.ContainsKey(data.prefabName) || !enterFlags[data.prefabName])
                        {
                            if (!instanceFlags.ContainsKey(data.prefabName) || !instanceFlags[data.prefabName])
                            {
                                GameObject prefab = data.prefab;
                                Debug.Log("Prefab trouvé : " + data.prefabName);
                                Debug.Log("Coordonnées du GameObject trouvé : " + obj.transform.position);
                                GameObject instance = Instantiate(prefab, obj.transform.position, Quaternion.identity);
                                instances[data.prefabName] = instance;
                                instanceFlags[data.prefabName] = true;
                                if (data.instantiateEvent != null)
                                {data.instantiateEvent.Post(instance);}
                            }
                        }
                        enterFlags[data.prefabName] = true;
                    }
                }
                if (!found)
                {
                    enterFlags[data.prefabName] = false;
                    if (instanceFlags.ContainsKey(data.prefabName) && instanceFlags[data.prefabName])
                    {
                        instanceFlags[data.prefabName] = false;                       
                        if (instances.ContainsKey(data.prefabName))
                        {
                            Destroy(instances[data.prefabName]);
                            instances.Remove(data.prefabName);
                        }
                        if (data.destroyEvent != null)
                        {data.destroyEvent.Post(gameObject);
                        }
                    }
                }
            }
        }
    }
}
