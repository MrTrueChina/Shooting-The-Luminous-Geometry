using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreatPlayerModel : MonoBehaviour
{
    [SerializeField]
    MeshFilter _filter;

    [SerializeField]
    string _path;
    [SerializeField]
    string _meshName;

    [SerializeField]
    Transform[] _vertices;

    [System.Serializable]
    struct Triangle
    {
        public int a;
        public int b;
        public int c;
    }
    [SerializeField]
    Triangle[] _triangles;

    public void CreatModel()
    {
        Debug.Log("Start Creat Player Model");
        
        _filter.mesh = GetMesh();
    }

    Mesh GetMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = GetVertices();
        mesh.triangles = GetTriangles();
        mesh.RecalculateNormals();
        mesh.RecalculateNormals();

        return mesh;
    }

    Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[_vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            vertices[i] = _vertices[i].position;
        
        return vertices;
    }

    private int[] GetTriangles()
    {
        int[] triangles = new int[_triangles.Length * 3];
        for (int i = 0; i < _triangles.Length; i++)
        {
            triangles[i * 3] = _triangles[i].a;
            triangles[i * 3 + 1] = _triangles[i].b;
            triangles[i * 3 + 2] = _triangles[i].c;
        }

        return triangles;
    }

    Vector3[] GetNormals()
    {
        return new Vector3[]
        {
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 1, -0.1f),
            new Vector3(1, 0, 0),
            new Vector3(0, -1, -0.1f),
        };
    }


    public void SaveModel()
    {
        AssetDatabase.CreateAsset(GetMesh(), _path + _meshName);
    }
}
