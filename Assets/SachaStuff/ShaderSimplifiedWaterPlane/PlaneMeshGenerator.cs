using UnityEditor;
using UnityEngine;

public class PlaneMeshGenerator : MonoBehaviour
{
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    private void Awake () {
    		Generate();
    	}
    
    private void Generate ()
    {
        Mesh mesh;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        Vector3 planeOrigin = new Vector3(-5, 0, -5);
        
        var vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        var uv = new Vector2[(xSize + 1) * (ySize + 1)];
        var normals = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            float yRatio = (float)y / ySize;
            
            for (int x = 0; x <= xSize; x++, i++)
            {
                float xRatio = (float)x / xSize;

                vertices[i] = planeOrigin + new Vector3(xRatio * 10, 0, yRatio * 10);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                normals[i] = Vector3.up;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.normals = normals;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
            for (int x = 0; x < xSize; x++, ti += 6, vi++) {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
    }
}
