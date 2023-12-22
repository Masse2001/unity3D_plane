using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MakeTerrain : MonoBehaviour {
    void Start(){
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices. Length; v++){
            vertices[v].y = Random. Range(0f,10f);
            }
        mesh.vertices = vertices;
        mesh. RecalculateBounds();
        mesh. RecalculateNormals();
        this.gameObject. AddComponent<MeshCollider>();
    }
}