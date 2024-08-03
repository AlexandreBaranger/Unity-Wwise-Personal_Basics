using UnityEngine;

public class WwiseSpeedControlEvent : MonoBehaviour
{
    public GameObject targetObject;
    public float updateInterval = 0.1f;
    public float maxSpeed = 10.0f;
    public float minHorizontalMovementForEvent = 0.1f; // Mouvement horizontal minimal pour d�clencher l'�v�nement
    public float minVerticalMovementForEvent = 0.1f; // Mouvement vertical minimal pour d�clencher l'�v�nement
    public float minSpeedRange = 1f; // Vitesse minimale pour d�clencher l'�v�nement dans la fourchette
    public float maxSpeedRange = 5f; // Vitesse maximale pour d�clencher l'�v�nement dans la fourchette
    private float timeSinceLastUpdate = 0.0f;
    private Vector3 lastPosition;
    private float currentSpeed = 0.0f;
    private bool isMoving = false;
    public AK.Wwise.Event speedChangeEvent;

    private void Start()
    {
        lastPosition = targetObject.transform.position;
    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {
            Vector3 currentPosition = targetObject.transform.position;
            float speed = Vector3.Distance(currentPosition, lastPosition) / updateInterval;
            currentSpeed = Mathf.Clamp(speed, 0.0f, maxSpeed);
            float horizontalMovement = Mathf.Abs(currentPosition.x - lastPosition.x);
            float verticalMovement = Mathf.Abs(currentPosition.y - lastPosition.y);
            float lateralMovement = Mathf.Abs(currentPosition.z - lastPosition.z);

            // V�rifier si la vitesse et le mouvement sont suffisants pour d�clencher l'�v�nement
            if (currentSpeed >= minSpeedRange && currentSpeed <= maxSpeedRange &&
                horizontalMovement < minHorizontalMovementForEvent && verticalMovement < minVerticalMovementForEvent && !isMoving)
            {
                speedChangeEvent.Post(gameObject);
                isMoving = true;
            }
            else if ((currentSpeed < minSpeedRange || currentSpeed > maxSpeedRange ||
                      horizontalMovement >= minHorizontalMovementForEvent || verticalMovement >= minVerticalMovementForEvent) && isMoving)
            {
                speedChangeEvent.Stop(gameObject);
                isMoving = false;
            }

            lastPosition = currentPosition;
            timeSinceLastUpdate = 0;
        }
    }
}
