using UnityEngine;

public class RTPCCurveAddAnimation: MonoBehaviour
{
    public AnimationClip clip;
    public float timeAnimation;
    public RTPCCurveControl rtpcCurveControl;

    [WwiseAnimationSelectorAttribute(typeof(RTPCCurveAddAnimation), typeof(RTPCCurveControl))]
    public string methodName;

    void Start()
    {
        if (clip != null && !string.IsNullOrEmpty(methodName))
        {
            AnimationEvent evt = new AnimationEvent();
            evt.time = timeAnimation;
            evt.functionName = "InvokeMethod";
            evt.stringParameter = methodName;
            clip.AddEvent(evt);
        }
    }

    void InvokeMethod(string methodName)
    {
        var split = methodName.Split('.');
        if (split.Length == 2)
        {
            var className = split[0];
            var method = split[1];

            if (className == nameof(RTPCCurveAddAnimation))
            {
                Invoke(method, 0f);
            }
            else if (className == nameof(RTPCCurveControl))
            {
                rtpcCurveControl.Invoke(method, 0f);
            }
        }
    }
}
