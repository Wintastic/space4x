using UnityEngine;
using System.Collections;

public class Sphere {
	public GameObject gameObject;

	public Sphere(string name, Vector3 center, float radius, int longitudeSegments, int latitudeSegments, Material material) {
		gameObject = new GameObject(name + "Sphere");
		gameObject.transform.position = center;
		buildSphere(radius, longitudeSegments, latitudeSegments);
		gameObject.AddComponent<MeshRenderer>().material = material;
	}

	private void buildSphere(float radius, int longitudeSegments, int latitudeSegments) {
		MeshFilter filter = gameObject.AddComponent< MeshFilter >();
		Mesh mesh = filter.mesh;
		mesh.Clear();
		
		#region Vertices
		Vector3[] vertices = new Vector3[(longitudeSegments+1) * latitudeSegments + 2];
		float _pi = Mathf.PI;
		float _2pi = _pi * 2f;
		
		vertices[0] = Vector3.up * radius;
		for( int lat = 0; lat < latitudeSegments; lat++ )
		{
			float a1 = _pi * (float)(lat+1) / (latitudeSegments+1);
			float sin1 = Mathf.Sin(a1);
			float cos1 = Mathf.Cos(a1);
			
			for( int lon = 0; lon <= longitudeSegments; lon++ )
			{
				float a2 = _2pi * (float)(lon == longitudeSegments ? 0 : lon) / longitudeSegments;
				float sin2 = Mathf.Sin(a2);
				float cos2 = Mathf.Cos(a2);
				
				vertices[ lon + lat * (longitudeSegments + 1) + 1] = new Vector3( sin1 * cos2, cos1, sin1 * sin2 ) * radius;
			}
		}
		vertices[vertices.Length-1] = Vector3.up * -radius;
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		uvs[0] = Vector2.up;
		uvs[uvs.Length-1] = Vector2.zero;
		for( int lat = 0; lat < latitudeSegments; lat++ )
			for( int lon = 0; lon <= longitudeSegments; lon++ )
				uvs[lon + lat * (longitudeSegments + 1) + 1] = new Vector2( (float)lon / longitudeSegments, 1f - (float)(lat+1) / (latitudeSegments+1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];
		
		//Top Cap
		int i = 0;
		for( int lon = 0; lon < longitudeSegments; lon++ )
		{
			triangles[i++] = lon+2;
			triangles[i++] = lon+1;
			triangles[i++] = 0;
		}
		
		//Middle
		for( int lat = 0; lat < latitudeSegments - 1; lat++ )
		{
			for( int lon = 0; lon < longitudeSegments; lon++ )
			{
				int current = lon + lat * (longitudeSegments + 1) + 1;
				int next = current + longitudeSegments + 1;
				
				triangles[i++] = current;
				triangles[i++] = current + 1;
				triangles[i++] = next + 1;
				
				triangles[i++] = current;
				triangles[i++] = next + 1;
				triangles[i++] = next;
			}
		}
		
		//Bottom Cap
		for( int lon = 0; lon < longitudeSegments; lon++ )
		{
			triangles[i++] = vertices.Length - 1;
			triangles[i++] = vertices.Length - (lon+2) - 1;
			triangles[i++] = vertices.Length - (lon+1) - 1;
		}
		#endregion
		
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		mesh.RecalculateBounds();
		mesh.Optimize();
	}

}

