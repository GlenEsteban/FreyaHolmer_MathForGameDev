/*
Exercise 7 Mesh Surface Area Calculator:
    In this exercise, there is one object - the mesh (cube). The objective is to calculate the surface area of the mesh.
Take into consideration that halving the magnitude of the cross product of two difference vectors from the tris of 
the mesh yields the area.
*/
using UnityEngine;

public class SurfaceAreaCalculator : MonoBehaviour {
    public float area;
    public Mesh mesh;

    void OnDrawGizmos() {
        Vector3[] vertices;
        int[] triangles;
        
        // cache references to the vertices and triangles
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;

        // reset area
        area = 0f;

        // using the vertices array and triangles array, calculate the area of each triangle
        for (int i = 0; i < triangles.Length; i += 3) {
            int aIndex = triangles [i];
            int bIndex = triangles [i + 1];
            int cIndex = triangles [i + 2];
            Vector3 a = vertices[triangles[i]];
            Vector3 b = vertices[triangles[i + 1]];
            Vector3 c = vertices[triangles[i + 2]];

            area += Vector3.Cross(b - a, c - a).magnitude;
        }
        area /= 2; // halves the sum to get the area of the mesh
    }
}
