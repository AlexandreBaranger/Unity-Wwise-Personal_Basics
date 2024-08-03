using UnityEngine;
using AK.Wwise;

[CreateAssetMenu(fileName = "WwiseInstanceData", menuName = "WwiseInstanceData")]
public class WwiseInstanceData : ScriptableObject
{
    public GameObject prefab;
    public string prefabName;
    public bool instanceCreated = false;
    public AK.Wwise.Event instantiateEvent; 
    public AK.Wwise.Event destroyEvent;    
}
