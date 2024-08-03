using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseAnimationEventOnce : MonoBehaviour
{
    public AK.Wwise.Event eventOnceStartLoop;
    public bool startLoop = true;
    public void AudioEventOnce()
    {
        if (startLoop)
        {
            { AkSoundEngine.PostEvent(eventOnceStartLoop.Id, gameObject); }
            startLoop = false;
        }
        
    }
}


