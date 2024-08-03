using UnityEngine;

public class WwiseListenerTPS : MonoBehaviour
{
    public Transform character; 
    public Transform cameraTransform; 
    public float distance = 0.1f; 
    public float smoothTime = 0.1f;
    private Vector3 velocity;
    private float currentRotation;
    private float rotationVelocity;

    void Start()
    {
        transform.position = character.position - character.forward * distance;
        currentRotation = cameraTransform.eulerAngles.y;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = character.position - character.forward * distance;
        float targetRotation = cameraTransform.eulerAngles.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        currentRotation = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref rotationVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
    }
}