using UnityEngine;
using System.Collections.Generic;
using AK.Wwise;

public class WwiseStateController : MonoBehaviour
{
    public List<EventToggle> eventToggles = new List<EventToggle>();
    public GameObject targetGameObject; // GameObject � colorer

    [System.Serializable]
    public class EventToggle
    {
        public string stateGroupName; // Le nom du groupe d'�tats Wwise
        public AK.Wwise.Event stateEvent; // S�lectionnez l'�v�nement Wwise
        public bool isStateActive; // Cochez pour activer l'�v�nement
        public Color associatedColor; // Couleur associ�e � cet �tat
        public GameObject objectToActivate; // GameObject � activer lors de l'activation de cet �tat
    }

    private Renderer objectRenderer; // Renderer du GameObject pour changer la couleur

    private void Start()
    {
        if (targetGameObject != null)
            objectRenderer = targetGameObject.GetComponent<Renderer>();
        else
            Debug.LogWarning("Aucun GameObject cible n'est d�fini dans le script.");
    }

    private void UpdateWwiseStates()
    {
        foreach (var toggle in eventToggles)
        {
            if (toggle.isStateActive)
            {
                toggle.stateEvent.Post(gameObject);
                if (objectRenderer != null)
                    objectRenderer.material.color = toggle.associatedColor; // Changer la couleur du GameObject
                else
                    Debug.LogWarning("Aucun Renderer trouv� sur le GameObject cible.");

                if (toggle.objectToActivate != null)
                    toggle.objectToActivate.SetActive(true); // Activer le GameObject associ�
                toggle.isStateActive = false; // R�initialiser la case � cocher apr�s avoir post� l'�v�nement
            }
            else
            {
                if (toggle.objectToActivate != null)
                    toggle.objectToActivate.SetActive(false); // D�sactiver le GameObject associ�
            }
        }
    }

    private void Awake()
    {
        UpdateWwiseStates();
    }

    private void OnValidate()
    {
        UpdateWwiseStates();
    }
}
