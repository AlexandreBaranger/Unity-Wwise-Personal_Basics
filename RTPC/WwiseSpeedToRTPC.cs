using UnityEngine;
using AK.Wwise;
public class WwiseSpeedToRTPC : MonoBehaviour
{
    public RTPC wwiseRTPC;
    public GameObject targetObject;
    public float updateInterval = 0.1f;
    public float maxSpeed = 100.0f;
    [Header("Vitesse en cours")]
    public float currentSpeed;
    private Vector3 lastPosition;
    private float timeSinceLastUpdate;

    private void Start()
    {
        if (targetObject == null)
        {Debug.LogError("Veuillez assigner l'objet cible dans l'inspecteur.");
         enabled = false;
         return;}
        lastPosition = targetObject.transform.position;
    }
    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {Vector3 currentPosition = targetObject.transform.position;
         float speed = Vector3.Distance(currentPosition, lastPosition) / updateInterval;
         currentSpeed = Mathf.Clamp(speed / maxSpeed * 100.0f, 0.0f, 100.0f);
            if (wwiseRTPC != null)
            {AkSoundEngine.SetRTPCValue(wwiseRTPC.Id, currentSpeed);}
            lastPosition = currentPosition;
            timeSinceLastUpdate = 0;}
    }
}
