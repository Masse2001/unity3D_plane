
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class progressiveForest : MonoBehaviour
{
        public GameObject[] TreePrefabs;
    public float perlinScale = 0.1f;
    public float treeDensity = 0.25f;
    public float minDistanceBetweenTrees = 2.0f;

    

    public int treesPerFrame = 20; // Nombre d'arbres à générer par frame

    private Perlin surface;
    private MeshFilter meshFilter;
    private List<Vector3> treePositions = new List<Vector3>();
    private bool generationComplete = false;

    public bool endWithCollision = false;

    void Start()
    {
        surface = new Perlin();
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(GenerateForestAsync());
    }

    IEnumerator GenerateForestAsync()
    {
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;

        for (int v = 0; v < vertices.Length; v += treesPerFrame)
        {
            for (int i = 0; i < treesPerFrame && v + i < vertices.Length; i++)
            {
                float perlinValue = surface.Noise(vertices[v + i].x * perlinScale, vertices[v + i].z * perlinScale) * 20;
                perlinValue = Mathf.Clamp(perlinValue, 0, TreePrefabs.Length - 1);

                Vector3 treePosition = transform.TransformPoint(vertices[v + i]);

                // Vérification de la distance minimale par rapport aux arbres déjà instanciés
                bool canInstantiate = true;
                foreach (Vector3 existingTreePos in treePositions)
                {
                    if (Vector3.Distance(existingTreePos, treePosition) < minDistanceBetweenTrees)
                    {
                        canInstantiate = false;
                        break;
                    }
                }

                if (canInstantiate && Random.value < treeDensity)
                {
                    // Instancier un arbre seulement si la distance minimale est respectée et si un nombre aléatoire est inférieur à la densité souhaitée
                    Instantiate(TreePrefabs[(int)perlinValue], treePosition, Quaternion.identity);
                    treePositions.Add(treePosition);
                }
            }

            yield return null; // Attendre la frame suivante avant de générer la suite
        }

        generationComplete = true;
    }

    void Update()
    {
        //ondition pour détecter la fin de la génération
        if (generationComplete)
        {
            //lorsque la génération est terminée
            Debug.Log("Generation Complete!");
        }
    }

    //
    void OnCollisionEnter(Collision collision)
    {
        if(endWithCollision){
            if (collision.gameObject.CompareTag("plane")) // Assurez-vous de définir un tag pour votre avion dans l'éditeur Unity
            {
                // Arrêtez le jeu
                Time.timeScale = 0f;
                Debug.Log("Game Over! Plane collided with the ground.");
                Application.Quit();
            }
        }
        
    }
}