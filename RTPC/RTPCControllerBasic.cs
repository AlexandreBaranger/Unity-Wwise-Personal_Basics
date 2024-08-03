using UnityEngine;

public class RTPCControllerBasic : MonoBehaviour
{
    public AK.Wwise.RTPC rtpc1;
    [Range(0f, 100f)]
    public float sliderRtpc01;

    private void Update()
    {
        rtpc1.SetGlobalValue(sliderRtpc01);
    }
}
