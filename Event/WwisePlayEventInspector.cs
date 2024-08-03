using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WwisePlayEventInspector : MonoBehaviour
{
    [SerializeField] // Permet � la variable d'�tre visible dans l'inspecteur Unity
    private bool playEventAutomatically = false; // Ajoutez une variable bool�enne pour contr�ler si l'�v�nement doit �tre d�clench� automatiquement

    public AK.Wwise.Event EventPlay;

    private void Start()
    {
        if (playEventAutomatically)
        {
            EventLaunch(); // D�clencher l'�v�nement automatiquement au d�marrage si playEventAutomatically est true
        }
    }

    private void EventLaunch()
    {
        EventPlay.Post(gameObject); // Utiliser Post() pour d�clencher l'�v�nement
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (playEventAutomatically)
        {
            EventLaunch(); // D�clencher l'�v�nement lorsqu'il y a des changements dans l'inspecteur
        }
    }
#endif
}
