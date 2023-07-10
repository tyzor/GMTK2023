using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTools : MonoBehaviour
{
    [SerializeField]
    private GameObject dirtPrefab;
    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private int mapSizeX = 100;
    [SerializeField]
    private int mapSizeY = 100;

/*
    [ContextMenu("Generate Background Dirt Tiles")]
    void GenerateDirtTiles()
    {

        var dirtBounds = dirtPrefab.GetComponent<MeshRenderer>().bounds;
        var wallBounds = wallPrefab.GetComponent<MeshRenderer>().bounds;
        var dirtContainer = new GameObject("DirtBG");
        GameObject firstTile = null;

        for (int x=0; x<5; x++)
        {
            for (int y=0; y<5; y++)
            {
                float px = x*dirtBounds.size.x;
                float py = y*dirtBounds.size.z;

                var newTile = Instantiate(dirtPrefab, new Vector3(px, wallBounds.size.y, py), Quaternion.Euler(0, Random.Range(0,4)*90,0));
                if(firstTile == null)
                    firstTile = newTile;
                newTile.name = $"dirt";
                newTile.transform.parent = dirtContainer.transform;
            }
        }

        // Combine all the meshes
        MeshFilter[] meshFilters = dirtContainer.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        firstTile.transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        .transform.gameObject.SetActive(true);

    }
    */

    [ContextMenu("Generate Background Dirt Tiles")]
    void FillDirtBackground()
    {
        var dirtBounds = dirtPrefab.GetComponent<MeshRenderer>().bounds;
        var wallBounds = wallPrefab.GetComponent<MeshRenderer>().bounds;
        var dirtContainer = new GameObject("DirtBG");

        float startX = -(mapSizeX / 2) * dirtBounds.size.x;
        float startY = -(mapSizeY / 2) * dirtBounds.size.z;

        for (int x=0; x<mapSizeX; x++)
        {
            for (int y=0; y<mapSizeY; y++)
            {
                float px = x*dirtBounds.size.x + startX;
                float py = y*dirtBounds.size.z + startY;

                var newTile = Instantiate(dirtPrefab, new Vector3(px, wallBounds.size.y, py), Quaternion.Euler(0, Random.Range(0,4)*90,0));
                newTile.name = $"dirt";
                newTile.transform.parent = dirtContainer.transform;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
