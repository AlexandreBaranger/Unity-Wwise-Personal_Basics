using UnityEngine;
using System.IO;
using System.Collections;
using System.Globalization;

public class WwiseSpeedControlEventSynthPreset : MonoBehaviour
{
    public string csvFileName;
    public GameObject targetObject;
    public float updateInterval = 0.1f;
    public float maxSpeed = 10.0f;
    public float minHorizontalMovementForEvent = 0.1f;
    public float minVerticalMovementForEvent = 0.1f;
    public float minSpeedRange = 1f;
    public float maxSpeedRange = 5f;
    private float timeSinceLastUpdate = 0.0f;
    private Vector3 lastPosition;
    private float currentSpeed = 0.0f;
    private bool isMoving = false;
    public AK.Wwise.Event speedChangeEvent;

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object not assigned!");
            enabled = false;
            return;
        }
        lastPosition = targetObject.transform.position;
    }

    private void Update()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object not assigned!");
            enabled = false;
            return;
        }



        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {
            Vector3 currentPosition = targetObject.transform.position;
            float speed = Vector3.Distance(currentPosition, lastPosition) / updateInterval;
            currentSpeed = Mathf.Clamp(speed, 0.0f, maxSpeed);
            float horizontalMovement = Mathf.Abs(currentPosition.x - lastPosition.x);
            float verticalMovement = Mathf.Abs(currentPosition.y - lastPosition.y);
            float lateralMovement = Mathf.Abs(currentPosition.z - lastPosition.z);
            if (currentSpeed >= minSpeedRange && currentSpeed <= maxSpeedRange &&
                horizontalMovement < minHorizontalMovementForEvent && verticalMovement < minVerticalMovementForEvent && !isMoving)
            {
                speedChangeEvent.Post(gameObject);
                isMoving = true;
                LoadCSVFromSpeedEvent(csvFileName);
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

    private void LoadCSVFromSpeedEvent(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        StartCoroutine(LoadCSV(filePath));
    }
    private IEnumerator LoadCSV(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("CSV file not found at path: " + filePath);
            yield break;
        }
        string[] rows = File.ReadAllLines(filePath);
        yield return null;
        foreach (string row in rows)
        {
            string[] columns = row.Split(',');
            if (columns.Length == 5)
            {
                CSVData data = new CSVData
                {
                    Volume = columns[0].Trim(),
                    Parameter = columns[1].Trim(),
                    MinRandomRange = float.Parse(columns[3].Trim(), CultureInfo.InvariantCulture),
                    MaxRandomRange = float.Parse(columns[4].Trim(), CultureInfo.InvariantCulture)
                };
                float value;
                string valueStr = columns[2].Trim();
                if (valueStr == "0.000000")
                {
                    value = 0.0f;
                }
                else if (float.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    data.Value = value;
                    SendValueToWwise(data);
                }
                else
                {
                    Debug.LogWarning("Failed to parse value: " + valueStr);
                }
            }
            else
            {
                Debug.LogWarning("Row format is incorrect: " + row);
            }
        }
    }
    private void SendValueToWwise(CSVData data)
    {
        float randomizedValue = UnityEngine.Random.Range(data.Value + data.MinRandomRange, data.Value + data.MaxRandomRange);
        string formattedValue = randomizedValue.ToString("0.000000", CultureInfo.InvariantCulture);
        AkSoundEngine.SetRTPCValue(data.Parameter, float.Parse(formattedValue, CultureInfo.InvariantCulture));
    }
    [System.Serializable]
    public class CSVData
    {
        public string Volume;
        public string Parameter;
        public float Value;
        public float MinRandomRange;
        public float MaxRandomRange;
    }
}
