using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    void Start(){
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices. Length; v++){
            vertices[v].y = Mathf. Cos(vertices[v].x * 10);
            print(vertices[v].y);
        }
        mesh.vertices = vertices;
        mesh. RecalculateBounds();
        mesh. RecalculateNormals();
        this.gameObject. AddComponent<MeshCollider>();
    }
}
