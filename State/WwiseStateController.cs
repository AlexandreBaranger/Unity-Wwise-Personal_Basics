using UnityEngine;
using System.Collections.Generic;
using AK.Wwise;

public class WwiseStateController : MonoBehaviour
{
    public List<EventToggle> eventToggles = new List<EventToggle>();
    public GameObject targetGameObject; // GameObject à colorer

    [System.Serializable]
    public class EventToggle
    {
        public string stateGroupName; // Le nom du groupe d'états Wwise
        public AK.Wwise.Event stateEvent; // Sélectionnez l'événement Wwise
        public bool isStateActive; // Cochez pour activer l'événement
        public Color associatedColor; // Couleur associée à cet état
        public GameObject objectToActivate; // GameObject à activer lors de l'activation de cet état
    }

    private Renderer objectRenderer; // Renderer du GameObject pour changer la couleur

    private void Start()
    {
        if (targetGameObject != null)
            objectRenderer = targetGameObject.GetComponent<Renderer>();
        else
            Debug.LogWarning("Aucun GameObject cible n'est défini dans le script.");
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
                    Debug.LogWarning("Aucun Renderer trouvé sur le GameObject cible.");

                if (toggle.objectToActivate != null)
                    toggle.objectToActivate.SetActive(true); // Activer le GameObject associé
                toggle.isStateActive = false; // Réinitialiser la case à cocher après avoir posté l'événement
            }
            else
            {
                if (toggle.objectToActivate != null)
                    toggle.objectToActivate.SetActive(false); // Désactiver le GameObject associé
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
