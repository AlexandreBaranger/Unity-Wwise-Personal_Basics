using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurfaceFootstepParams
{
    public string surfaceTag;
    public string switchSurface;
}

public class WwiseFPSFootstepController : MonoBehaviour
{
    public AK.Wwise.Event footstepSoundEvent;
    public string switchGroup;

    public float minWalkSpeed = 0.1f;
    public float maxWalkSpeed = 4f;
    public float minRunSpeed = 5f;
    public float maxRunSpeed = 7f;
    public float minStepTimingWalk = 0.5f;
    public float maxStepTimingWalk = 0.5f;
    public float minStepTimingRun = 0.3f;
    public float maxStepTimingRun = 0.3f;

    public float groundDistance = 1f;

    public List<SurfaceFootstepParams> surfaceFootstepParams;

    public bool debugMode = false;

    private bool isGrounded = false;
    private float currentSpeed = 0f;
    private float currentStepTiming = 0.5f;
    private RaycastHit hitInfo;
    private Vector3 lastPosition;
    private float nextStepTime = 0f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        CheckGrounded();

        Vector3 currentPosition = transform.position;
        Vector3 velocity = (currentPosition - lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;

        currentSpeed = velocity.magnitude;

        // Recherche des paramètres de pas pour la surface actuelle
        SurfaceFootstepParams currentSurfaceParams = GetSurfaceFootstepParams();

        // Mise à jour du timing des pas en fonction de la vitesse
        float minStepTiming = currentSpeed <= maxWalkSpeed ? minStepTimingWalk : minStepTimingRun;
        float maxStepTiming = currentSpeed <= maxWalkSpeed ? maxStepTimingWalk : maxStepTimingRun;

        float t = Mathf.InverseLerp(minWalkSpeed, maxRunSpeed, currentSpeed);
        currentStepTiming = Mathf.Lerp(maxStepTiming, minStepTiming, t);

        if (currentSpeed > minWalkSpeed && isGrounded && Time.time >= nextStepTime)
        {
            PlayFootstepSound();
            CalculateNextStepTime();
        }
    }

    SurfaceFootstepParams GetSurfaceFootstepParams()
    {
        foreach (SurfaceFootstepParams param in surfaceFootstepParams)
        {
            if (hitInfo.collider != null && hitInfo.collider.CompareTag(param.surfaceTag))
            {
                return param;
            }
        }
        return null;
    }

    void PlayFootstepSound()
    {
        if (footstepSoundEvent != null)
        {
            footstepSoundEvent.Post(gameObject);
            foreach (var surfaceSwitch in surfaceFootstepParams)
            {
                if (hitInfo.collider != null && hitInfo.collider.CompareTag(surfaceSwitch.surfaceTag))
                {
                    AkSoundEngine.SetSwitch(switchGroup, surfaceSwitch.switchSurface, gameObject);
                    if (debugMode)
                    {
                        Debug.Log("Surface trouvée : " + surfaceSwitch.surfaceTag);
                    };
                    break;
                }
            }
            if (debugMode)
            {
                Debug.Log("Wwise event is playing: " + footstepSoundEvent.Name);
            }
        }
        else
        {
            Debug.LogWarning("Wwise event is not configured or missing.");
        }
    }

    void CalculateNextStepTime()
    {
        nextStepTime = Time.time + currentStepTiming;
    }

    void CheckGrounded()
    {
        Vector3 raycastOrigin = transform.position + Vector3.up * (GetComponent<CharacterController>().height / 2);

        if (Physics.Raycast(raycastOrigin, Vector3.down, out hitInfo, groundDistance))
        {
            isGrounded = true;
            if (debugMode)
            {
                Debug.DrawRay(raycastOrigin, Vector3.down * groundDistance, Color.magenta);
            }
        }
        else
        {
            isGrounded = false;
            if (debugMode)
            {
                Debug.Log("No ground detected!");
            }
        }
    }
}
