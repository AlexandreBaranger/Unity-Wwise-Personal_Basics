using UnityEngine;

public class SplineSpeedControl : MonoBehaviour
{
    public SplineBasic splineBasic; // R�f�rence � la classe SplineBasic
    [Range(0f, 100f)]
    public float sliderSpeed;

    private void Update()
    {
        if (splineBasic != null)
        {
            splineBasic.speed = sliderSpeed; // Affecter la valeur du slider � la vitesse de la spline
        }
    }
}
