using UnityEngine;

public class SplineBasicFollow : MonoBehaviour
{
    public SplineBasic splineBasic;
    private int currentIndex = 0;
    private float t = 0.0f;

    private void Update()
    {
        if (splineBasic != null && splineBasic.splinePoints.Length > 1)
        {
            t += Time.deltaTime / splineBasic.speed;
            if (t >= 1.0f)
            {
                t = 0.0f;
                currentIndex++;
                if (currentIndex >= splineBasic.splinePoints.Length - 1)
                    currentIndex = 0;
            }

            Vector3 newPosition = Vector3.Lerp(splineBasic.splinePoints[currentIndex], splineBasic.splinePoints[currentIndex + 1], t);
            transform.position = newPosition;
        }
    }
}
