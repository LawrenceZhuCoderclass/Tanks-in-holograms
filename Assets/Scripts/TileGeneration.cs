using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileGeneration : MonoBehaviour {
  [SerializeField]
  NoiseGeneration noiseMapGeneration;
  [SerializeField]
  private MeshRenderer tileRenderer;
  [SerializeField]
  private MeshFilter meshFilter;
  [SerializeField] 
        private MeshCollider meshCollider;
  [SerializeField]
  private float mapScale;

  [SerializeField]
  private float heightMultiplier;

  void Start() {
    GenerateTile ();
  }
  void GenerateTile() {
    Vector3[] meshVertices = this.meshFilter.mesh.vertices;
    int tileDepth = (int)Mathf.Sqrt (meshVertices.Length);
    int tileWidth = tileDepth;
    float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap (tileDepth, tileWidth, this.mapScale);
    Texture2D tileTexture = BuildTexture (heightMap);
    this.tileRenderer.material.mainTexture = tileTexture;
    UpdateMeshVertices(heightMap);
  }
  private Texture2D BuildTexture(float[,] heightMap) {
    int tileDepth = heightMap.GetLength (0);
    int tileWidth = heightMap.GetLength (1);
    Color[] colorMap = new Color[tileDepth * tileWidth];
    for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
      for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
        int colorIndex = zIndex * tileWidth+ xIndex;
        float height= heightMap[zIndex, xIndex];
        colorMap [colorIndex] = Color.Lerp (Color.black, Color.white, height);
      }
    }
    Texture2D tileTexture = new Texture2D (tileWidth, tileDepth);
    tileTexture.wrapMode = TextureWrapMode.Clamp;
    tileTexture.SetPixels (colorMap);
    tileTexture.Apply ();
    return tileTexture;
  }
  
  private void UpdateMeshVertices(float[,] heightMap) {
    int tileDepth = heightMap.GetLength (0);
    int tileWidth = heightMap.GetLength (1);
    Vector3[] meshVertices = this.meshFilter.mesh.vertices;
    int vertexIndex = 0;
    for (int zIndex = 0; zIndex < tileDepth; zIndex++) {
      for (int xIndex = 0; xIndex < tileWidth; xIndex++) {
        float height = heightMap [zIndex, xIndex];
        Vector3 vertex = meshVertices [vertexIndex];
        meshVertices[vertexIndex] = new Vector3(vertex.x, height * this.heightMultiplier, vertex.z);
        vertexIndex++;
      }
    }
    this.meshFilter.mesh.vertices = meshVertices;
    this.meshFilter.mesh.RecalculateBounds ();
    this.meshFilter.mesh.RecalculateNormals ();
    this.meshCollider.sharedMesh = this.meshFilter.mesh;
  }
}