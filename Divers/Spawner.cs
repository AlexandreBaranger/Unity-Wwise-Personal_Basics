using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Le GameObject que vous voulez spawner

    // M�thode pour spawner un GameObject � l'emplacement d'un autre GameObject
    public void SpawnAtLocation(GameObject targetObject)
    {
        // V�rifiez si le prefab � spawner est d�fini
        if (prefabToSpawn != null)
        {
            // R�cup�rez la position du GameObject cible
            Vector3 spawnPosition = targetObject.transform.position;

            // Spawnez le nouveau GameObject � cette position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab � spawner non d�fini !");
        }
    }

    // Met � jour le GameObject cible lorsque l'utilisateur clique dessus
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
                // V�rifiez si le GameObject touch� est valide
                if (hit.collider.gameObject != null)
                {
                    // Appelez la m�thode SpawnAtLocation en passant le GameObject touch�
                    SpawnAtLocation(hit.collider.gameObject);
                }
            }
        }
    }
}
