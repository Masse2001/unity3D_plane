

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public GameObject[] TreePrefabs;
    public float perlinScale = 0.1f;
    public float treeDensity = 0.5f;
    public float minDistanceBetweenTrees = 2.0f;

    void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        Perlin surface = new Perlin();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("No MeshFilter or Mesh found. Make sure your object has a MeshFilter component with a valid mesh.");
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;

        List<Vector3> treePositions = new List<Vector3>();

        for (int v = 0; v < vertices.Length; v++)
        {
            float perlinValue = surface.Noise(vertices[v].x * perlinScale, vertices[v].z * perlinScale) * 20;
            perlinValue = Mathf.Clamp(perlinValue, 0, TreePrefabs.Length - 1);

            Vector3 treePosition = transform.TransformPoint(vertices[v]);

            // Vérifiez la distance minimale par rapport aux arbres déjà instanciés
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
    }
}
