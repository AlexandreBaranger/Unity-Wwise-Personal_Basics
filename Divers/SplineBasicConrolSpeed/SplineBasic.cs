using UnityEngine;

public class SplineBasic : MonoBehaviour
{
    public GameObject[] splinePointsObjects; // Tableau pour stocker les GameObjects représentant les points de la spline
    public Vector3[] splinePoints; // Tableau pour stocker les positions des points de la spline
    private int splineCount;

    public bool debug_DrawSpline = true;
    public float speed = 1.0f; // Ajout de la propriété speed

    private void Start()
    {
        splineCount = splinePointsObjects.Length; // Obtenir le nombre de points de la spline à partir des GameObjects

        splinePoints = new Vector3[splineCount];
        for (int i = 0; i < splineCount; i++)
        {
            splinePoints[i] = splinePointsObjects[i].transform.position; // Obtenir les positions des points à partir des GameObjects
        }
    }

    private void Update()
    {
        if (splineCount > 1)
        {
            for (int i = 0; i < splineCount - 1; i++)
            {
                Debug.DrawLine(splinePoints[i], splinePoints[i + 1], Color.green);
            }
        }
    }

    public int GetSplinePointsCount()
    {
        return splineCount;
    }
}
