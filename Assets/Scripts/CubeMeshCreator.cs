using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMeshCreator : MonoBehaviour
{
    public Mesh mesh;

    public Vector3[] vertices;
    public int[] triangles;
    
    public Vector3[] verticesInitialPos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateMesh();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            //vertices[i] = RotateX(vertices[i], 0.03f);
        }

        SnakeLikeMovement();
        UpdateMesh();
    }

    Vector3 RotateX(Vector3 vertex, float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);
        
        float newY = vertex.y * cosTheta - vertex.z * sinTheta;
        float newZ = vertex.y * sinTheta + vertex.z * cosTheta;
        
        return new Vector3(vertex.x, newY, newZ);
    }

    void CreateMesh()
    {
        List<Vector3> startingVertices = new List<Vector3>();
        
        startingVertices.Add(new Vector3(-0.5f, 0.5f, -0.5f)); //0
        startingVertices.Add(new Vector3(0.5f, 0.5f, -0.5f));//1
        startingVertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));//2
        startingVertices.Add(new Vector3(0.5f, -0.5f, -0.5f));//3
        startingVertices.Add(new Vector3(-0.5f, 0.5f, 0));//4
        startingVertices.Add(new Vector3(0.5f, 0.5f, 0));//5
        startingVertices.Add( new Vector3(-0.5f, -0.5f, 0));//6
        startingVertices.Add(new Vector3(0.5f, -0.5f, 0));//7

        List<int> startingTriangles = new List<int>
        {
            0, 1, 2,
            1, 3, 2,
            1, 5, 3,
            5, 7, 3,
            0, 4, 5,
            0, 5, 1,
            0, 6, 4,
            0, 2, 6,
            4, 6, 5,
            6, 7, 5,
            2, 3, 7,
            2, 7, 6 
        };

        for (int y = 0; y < 20; y++)
        {
            int amount = 8 + 4 * y;
            for (int i = amount - 4; i < amount; i++)
            {
                startingVertices.Add(startingVertices[i] + new Vector3( 0, 0, 0.5f)); //0.5f*Mathf.Cos(y*30 * Mathf.Deg2Rad)
            }
        
            int amount2 = 36 + 30 * y;
            for (int i = amount2 - 30; i < amount2; i++)
            {
                startingTriangles.Add(startingTriangles[i] + 4);
            }
        }

        
        vertices = startingVertices.ToArray();
        triangles = startingTriangles.ToArray();
        for (int i = 0; i < startingVertices.Count; i++)
        {
            
        }

        verticesInitialPos = vertices.Clone() as Vector3[];
    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    void SnakeLikeMovement()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            int chunk = (i / 4) + 1 ;
            float startCos = chunk * 30 * Mathf.Deg2Rad;
             float diramount = 1 * Mathf.Cos(startCos + (Time.time * 90 * Mathf.Deg2Rad));
            //float diramount = (Mathf.Cos(startCos)) * Mathf.Sin((Time.time * 90 * Mathf.Deg2Rad));
            vertices[i] = verticesInitialPos[i] + new Vector3(1*diramount, 0, 0);
        }
    }
}
