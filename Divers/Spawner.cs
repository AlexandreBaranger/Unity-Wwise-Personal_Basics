using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Le GameObject que vous voulez spawner

    // Méthode pour spawner un GameObject à l'emplacement d'un autre GameObject
    public void SpawnAtLocation(GameObject targetObject)
    {
        // Vérifiez si le prefab à spawner est défini
        if (prefabToSpawn != null)
        {
            // Récupérez la position du GameObject cible
            Vector3 spawnPosition = targetObject.transform.position;

            // Spawnez le nouveau GameObject à cette position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab à spawner non défini !");
        }
    }

    // Met à jour le GameObject cible lorsque l'utilisateur clique dessus
    void Update()
    {
        // Si l'utilisateur clique sur le bouton gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            // Lance un rayon depuis la position de la souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si le rayon touche un GameObject
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifiez si le GameObject touché est valide
                if (hit.collider.gameObject != null)
                {
                    // Appelez la méthode SpawnAtLocation en passant le GameObject touché
                    SpawnAtLocation(hit.collider.gameObject);
                }
            }
        }
    }
}
