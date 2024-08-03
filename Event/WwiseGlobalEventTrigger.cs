using UnityEngine;

public class WwiseGlobalEventTrigger : MonoBehaviour
{
    public AK.Wwise.Event eventNameIn;
    public AK.Wwise.Event eventNameStay;
    public AK.Wwise.Event eventNameOut;
    public float triggerInterval = 1f; // Fr�quence de rafra�chissement, r�glable dans l'inspecteur
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent(eventNameIn.Id, gameObject);
            timer = 0f; // R�initialiser le compteur de temps lorsqu'un joueur entre
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && timer >= triggerInterval)
        {
            AkSoundEngine.PostEvent(eventNameStay.Id, gameObject);
            timer = 0f; // R�initialiser le compteur de temps apr�s avoir d�clench� l'�v�nement
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent(eventNameOut.Id, gameObject);
            timer = 0f; // R�initialiser le compteur de temps lorsqu'un joueur sort
        }
    }
}


