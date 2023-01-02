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

  public Material[] GroundMaterial;
  void Start() {
    mapScale = Random.Range(1.0f, 6.0f);
    heightMultiplier = Random.Range(1.0f, 2.0f);
    GenerateTile ();
  }
  void GenerateTile() {
    Vector3[] meshVertices = this.meshFilter.mesh.vertices;
    int tileDepth = (int)Mathf.Sqrt (meshVertices.Length);
    int tileWidth = tileDepth;
    float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap (tileDepth, tileWidth, this.mapScale);
    Texture2D tileTexture = BuildTexture (heightMap);
    int chosenMaterial = Random.Range(0, 9);
    this.tileRenderer.material = GroundMaterial[chosenMaterial];
    //heightmultiplier veranderen op basis van het material
    switch (chosenMaterial)
    {
            case 0:
                heightMultiplier = 0;
                break;
            case 1:
                heightMultiplier = Random.Range(0f, 1.0f);
                break;
            case 2:
                heightMultiplier = Random.Range(1.0f, 2.0f);
                break;
            case 3:
                heightMultiplier = Random.Range(1.0f, 2.0f);
                break;
            case 4:
                heightMultiplier = Random.Range(1.0f, 2.0f);
                break;
            case 5:
                heightMultiplier = Random.Range(1.0f, 2.0f);
                break;
            case 6:
                heightMultiplier = Random.Range(2.0f, 3.0f);
                break;
            case 7:
                heightMultiplier = Random.Range(0f, 2.0f);
                break;
            case 8:
                heightMultiplier = Random.Range(3.0f, 5.0f);
                break;
            case 9:
                heightMultiplier = Random.Range(1.0f, 2.0f);
                break;
            default:
                break;
    }
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