using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTest2 : MonoBehaviour
{
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetSphereVertex(transform.position, 5);
        }
    }

    void GetSphereVertex(Vector3 center, float radius)
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(center, radius, transform.up*-1, 1f, layerMask);

        foreach (RaycastHit hit in raycastHits)
        {
            print(hit.collider.name);
            MeshCollider meshCollider = hit.collider as MeshCollider;
            var mesh = meshCollider.sharedMesh;
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;
            var p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
            var p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
            var p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

        }
    }
}
