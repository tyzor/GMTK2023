using UnityEngine;
using System.Collections;

public class DungeonFloor: MonoBehaviour
{
	public int mapWidth = 10;
	public int mapHeight = 10;

	[SerializeField]
	private MapTile.TileType[] mapData;

	[SerializeField]
	private GameObject floorPrefab;

    [SerializeField]
    private GameObject wallPrefab;

	[SerializeField]
    private GameObject wallDoorPrefab;

    [SerializeField]
    private GameObject wallTopPrefab;

    // Use this for initialization
    void Start()
	{
		LoadFromFile("Levels/Floor1");
		//CreateMap();
		RenderMap();
	}

	// Update is called once per frame
	void Update()
	{
			
	}

	void LoadFromFile(string filename)
	{
        var textFile = Resources.Load<TextAsset>(filename);
		string mapString = textFile.text.Replace("\r","");

		string[] mapLines = mapString.Split("\n");

		mapWidth = mapLines[0].Length;
		mapHeight = mapLines.Length;
		mapData = new MapTile.TileType[mapWidth * mapHeight];

		Debug.Log($"width {mapWidth} height {mapHeight}");

		for (int x = 0; x < mapWidth; x++)
		{
			for (int y = 0; y < mapHeight; y++)
			{
				var tileChar = mapLines[y][x];
				MapTile.TileType newTile = MapTile.TileType.Wall;
				if(tileChar == 'W')
				{
					newTile = MapTile.TileType.Wall;
				} else if(tileChar == 'S')
				{
					newTile = MapTile.TileType.Entrance;
				} else if(tileChar == 'E')
				{
					newTile = MapTile.TileType.Exit;
				} else if(tileChar == '.')
				{
					newTile = MapTile.TileType.Floor;
				}
				mapData[(mapHeight - 1 - y) * mapWidth + x] = newTile;
	
			}
		}
    }

	void RenderMap()
	{
        Bounds wallBounds = wallPrefab.GetComponent<MeshRenderer>().bounds;
        Bounds floorBounds = floorPrefab.GetComponent<MeshRenderer>().bounds;
		Vector3 tileSize = floorBounds.size;

		var ContainerObj = new GameObject("FloorMap");

        // Create the appropriate prefabs based on the floor layout
        for(int x = 0; x < mapWidth; x++)
		{
            for (int y = 0; y < mapHeight; y++)
            {
				var mapTile = mapData[y*mapWidth + x];
				if (mapTile == MapTile.TileType.Floor
					|| mapTile == MapTile.TileType.Entrance
					|| mapTile == MapTile.TileType.Exit)
				{
					float px = (x * tileSize.x);
					float py = (y * tileSize.z);
                    // Instantiate at position (0, 0, 0) and zero rotation.
                    var newTile = Instantiate(floorPrefab, new Vector3(px, 0, py), Quaternion.identity);
					newTile.name = $"FloorTile{x},{y}";
					newTile.transform.parent = ContainerObj.transform;

					BuildWalls(x, y, ContainerObj);
					continue;
;               }

				if (mapTile == MapTile.TileType.Wall)
				{
					// Put dirt on top to simulate the ground
					float px = (x * tileSize.x);
					float py = (y * tileSize.z);
					var newTile = Instantiate(wallTopPrefab, new Vector3(px, wallBounds.size.y, py), Quaternion.Euler(0, Random.Range(0,4)*90,0));
					newTile.name = $"WallTile{x},{y}";
					newTile.transform.parent = ContainerObj.transform;
					continue;
				}

            }
        }
    }

	void BuildWalls(int x, int y, GameObject parent)
	{
		Bounds wallBounds = wallPrefab.GetComponent<MeshRenderer>().bounds;
        Bounds floorBounds = floorPrefab.GetComponent<MeshRenderer>().bounds;
        Vector3 tileSize = floorBounds.size;

		MapTile.TileType tile = getMapTile(x,y);

        // North Wall
		if( tile == MapTile.TileType.Exit )
		{
            float px = x * tileSize.x;
            float py = y * tileSize.z + floorBounds.extents.z;
            Instantiate(wallDoorPrefab, new Vector3(px, 0, py), Quaternion.identity, parent.transform);
        }
        else if (y >= mapHeight-1 || getMapTile(x,y+1) == MapTile.TileType.Wall )
		{
			float px = x * tileSize.x;
			float py = y * tileSize.z + floorBounds.extents.z;
			Instantiate(wallPrefab, new Vector3(px,0,py), Quaternion.identity, parent.transform);
		}

        // South Wall
		if( tile == MapTile.TileType.Entrance )
		{
            float px = x * tileSize.x;
            float py = y * tileSize.z - floorBounds.extents.z;
            Instantiate(wallDoorPrefab, new Vector3(px, 0, py), Quaternion.identity, parent.transform);
        }
        else if (y <= 0 || getMapTile(x,y-1) == MapTile.TileType.Wall)
        {
            float px = x * tileSize.x;
            float py = y * tileSize.z - floorBounds.extents.z;
            Instantiate(wallPrefab, new Vector3(px, 0, py), Quaternion.identity, parent.transform);
        }

        // East Wall
        if (x >= mapWidth - 1 || getMapTile(x+1,y) == MapTile.TileType.Wall)
        {
            float px = x * tileSize.x + floorBounds.extents.x;
			float py = y * tileSize.z;
            Instantiate(wallPrefab, new Vector3(px, 0, py), Quaternion.Euler(0,90,0), parent.transform);

        }

        // West Wall
        if (x <= 0 || getMapTile(x-1,y) == MapTile.TileType.Wall)
        {
            float px = x * tileSize.x - floorBounds.extents.x;
            float py = y * tileSize.z;
            Instantiate(wallPrefab, new Vector3(px, 0, py), Quaternion.Euler(0, 90, 0), parent.transform);
        }

    }

	public MapTile.TileType getMapTile(int x, int y)
	{
		return mapData[mapWidth * y + x];

	}

}

