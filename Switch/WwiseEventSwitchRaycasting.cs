using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurfaceSwitch
{
    public string surfaceTag;
    public string wwiseSwitch;
}
public class WwiseEventSwitchRaycasting : MonoBehaviour
{
    public AK.Wwise.Event walk;
    public AK.Wwise.Event run;
    public AK.Wwise.Event ground;


    public string switchGroup;
    public float raycastDistance = 1f;
    public float raycastHeight = 0.85f;
    public float raycastInterval = 0.1f;
    public List<SurfaceSwitch> surfaceSwitches = new List<SurfaceSwitch>();
    public bool debugMode = false;
    private void Start()
    { InvokeRepeating("PerformRaycast", 0, raycastInterval); }
    private void PerformRaycast()
    {
        Vector3 headPosition = transform.position + Vector3.up * raycastHeight;
        Vector3 direction = Vector3.down;
        RaycastHit[] hits = Physics.RaycastAll(headPosition, direction, raycastDistance);
        List<string> collisionTags = new List<string>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player")) continue;
            string surfaceTag = hit.collider.tag;
            collisionTags.Add(surfaceTag);
            foreach (var surfaceSwitch in surfaceSwitches)
            { if (surfaceTag == surfaceSwitch.surfaceTag) { AkSoundEngine.SetSwitch(switchGroup, surfaceSwitch.wwiseSwitch, gameObject); if (debugMode) { Debug.Log("Surface trouvée : " + surfaceTag); }; break; } }
        }
    }
    public void PlayWalkEvent()
    { walk.Post(gameObject); if (debugMode) { Debug.Log("Événement Wwise déclenché"); }; }
    public void PlayRunEvent()
    { run.Post(gameObject); if (debugMode) { Debug.Log("Événement Wwise déclenché"); }; }
    public void PlayGroundEvent()
    { ground.Post(gameObject); if (debugMode) { Debug.Log("Événement Wwise déclenché"); }; }

}