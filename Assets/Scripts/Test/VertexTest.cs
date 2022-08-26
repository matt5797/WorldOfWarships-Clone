using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTest : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Mesh mesh;
    Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        mesh = meshFilter.mesh;
        //mesh = meshFilter.sharedMesh;

        vertices = mesh.vertices;

        print(vertices.Length);

        /*for(int i = 0; i < vertices.Length; i++)
        {
            if (i > vertices.Length/3 && i< vertices.Length * 2 / 3)
                vertices[i].y += 1;
        }
        mesh.vertices = vertices;*/
        //mesh.RecalculateBounds();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            //if (i > vertices.Length / 3 && i < vertices.Length * 2 / 3)
            vertices[i].y += 5 * Time.deltaTime * i / vertices.Length;
        }
        mesh.vertices = vertices;
        //mesh.RecalculateBounds();
    }

    /*void Test()
    {
        Ray ray = new Ray();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            print(" / " + hit.triangleIndex);
        }

    }*/

    void GetSphereVertex(Vector3 center, float radius)
    {
        //Physics.SphereCastAll(center, radius, transform.up, 0.1f, );
    }
}
