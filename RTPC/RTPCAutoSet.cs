using UnityEngine;
using AK.Wwise;

public class RTPCAutoSet : MonoBehaviour
{
    public AK.Wwise.RTPC rtpc;
    public Collider triggerCollider;
    public float startValue = -96f;
    public float endValue = 0f;
    public float transitionTime = 1f;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Range(-96f, 0f)]
    public float manualValue;

    private bool triggered = false;
    private bool exitTriggered = false; // Variable pour savoir si le trigger de sortie a d�j� �t� d�clench�
    private float transitionTimer = 0f;
    private float currentValue;

    void Start()
    {
        currentValue = startValue;
        rtpc.SetGlobalValue(currentValue);
    }

    void Update()
    {
        if (triggered)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionTime);
            currentValue = Mathf.Lerp(startValue, endValue, transitionCurve.Evaluate(t));
            rtpc.SetGlobalValue(currentValue);
            manualValue = currentValue;

            if (transitionTimer >= transitionTime)
            {
                triggered = false;
                transitionTimer = 0f;
            }
        }
        else if (exitTriggered)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionTime);
            currentValue = Mathf.Lerp(endValue, startValue, transitionCurve.Evaluate(t));
            rtpc.SetGlobalValue(currentValue);
            manualValue = currentValue;

            if (transitionTimer >= transitionTime)
            {
                exitTriggered = false; // R�initialiser le d�clencheur de sortie
                transitionTimer = 0f;
            }
        }
        else
        {
            rtpc.SetGlobalValue(manualValue);
            currentValue = manualValue;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!exitTriggered) // Si le trigger de sortie n'a pas �t� d�clench�, on peut d�clencher le fade-in
            {
                triggered = true;
                Debug.Log("Collision d�tect�e avec succ�s. D�clencher le fade-in.");
            }
            else // Si le trigger de sortie a �t� d�clench�, r�initialiser la variable exitTriggered
            {
                exitTriggered = false;
                Debug.Log("Collision d�tect�e avec succ�s. R�initialiser le d�clencheur de sortie.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered) // Si le fade-in n'est pas en cours, d�clencher le fade-out
            {
                exitTriggered = true;
                Debug.Log("Collision sortie avec succ�s. D�clencher le fade-out.");
            }
        }
    }
}
