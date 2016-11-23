using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean;

public class BuildingEngine : MonoBehaviour
{
    public List<BuildingSprite> BuildingSprites;
    public Sprite DefaultImage;
    public GameObject BuildingContainerPrefab;
    public GameObject BuildingPrefab;
    public Vector2 CurrentPosition;
    public Vector2 ViewPortSize;
    public int totalBuildings;
    public float tileWidth;
    public float tileHeight;
    public Vector2 MapSize;

    private Vector2[] locations;
    private BuildingSprite[,] _grid;
    private GameObject controller;
    private GameObject _buildingContainer;
    private List<GameObject> _buildings = new List<GameObject>();

    public void Start()
    {
        controller = GameObject.Find("Controller");
        _grid = new BuildingSprite[(int)MapSize.x, (int)MapSize.y];

        InitializeGrid();
        SetBuildings();
        PlaceBuildings();
    }

    public void Update()
    {

    }

    public void InitializeGrid()
    {
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                _grid[x, y] = new BuildingSprite("unset", DefaultImage, Buildings.Unset);
            }
        }
    }

    public void SetBuildings()
    {
        var buildingCounter = totalBuildings;
        var hospitals = totalBuildings / 20;
        var schools = totalBuildings / 10;
        var offices = totalBuildings / 5;
        locations = new Vector2[totalBuildings];
        while (buildingCounter != 0)
        {
            int randomSpawnPointx = (int)Random.Range(0, ViewPortSize.x);
            int randomSpawnPointy = (int)Random.Range(0, ViewPortSize.y);
            if (hospitals != 0)
            {
                _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[3].Name, BuildingSprites[3].BuildingImage, BuildingSprites[3].BuildingType);

                hospitals--;
            }
            else if (schools != 0)
            {
                _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[2].Name, BuildingSprites[2].BuildingImage, BuildingSprites[2].BuildingType);
                schools--;
            }
            else if (offices != 0)
            {
                _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[1].Name, BuildingSprites[1].BuildingImage, BuildingSprites[1].BuildingType);
                offices--;
            }
            else
            {
                _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[0].Name, BuildingSprites[0].BuildingImage, BuildingSprites[0].BuildingType);
            }
            buildingCounter--;
        }
    }

    public void PlaceBuildings()
    {
        foreach (GameObject o in _buildings)
        {
            LeanPool.Despawn(o);
        }
        _buildings.Clear();
        LeanPool.Despawn(_buildingContainer);
        _buildingContainer = LeanPool.Spawn(BuildingContainerPrefab);
        var viewOffsetX = ViewPortSize.x / 2f;
        var viewOffsetY = ViewPortSize.y / 2f;
        for (var y = -viewOffsetY; y < viewOffsetY; y++)
        {
            if (y % 2 == 0)
            {
                for (var x = -viewOffsetX; x < viewOffsetX; x++)
                {
                    var tX = x * tileWidth;
                    var tY = y * tileHeight;

                    var iX = x + CurrentPosition.x;
                    var iY = y + CurrentPosition.y;

                    if (iX < 0) continue;
                    if (iY < 0) continue;
                    if (iX > MapSize.x - 2) continue;
                    if (iY > MapSize.y - 2) continue;

                    var b = LeanPool.Spawn(BuildingPrefab);
                    b.transform.position = new Vector3(tX, tY, 0);
                    b.transform.SetParent(_buildingContainer.transform);
                    var renderer = b.GetComponent<SpriteRenderer>();
                    renderer.sprite = _grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].BuildingImage;
                    _buildings.Add(b);
                }
            }
            else
            {
                for (var x = -viewOffsetX + 1; x < viewOffsetX; x++)
                {
                    var tX = (x - .5f) * tileWidth;
                    var tY = y * tileHeight;

                    var iX = x + CurrentPosition.x;
                    var iY = y + CurrentPosition.y;

                    if (iX < 0) continue;
                    if (iY < 0) continue;
                    if (iX > MapSize.x - 2) continue;
                    if (iY > MapSize.y - 2) continue;

                    var b = LeanPool.Spawn(BuildingPrefab);
                    b.transform.position = new Vector3(tX, tY, 0);
                    b.transform.SetParent(_buildingContainer.transform);
                    var renderer = b.GetComponent<SpriteRenderer>();
                    renderer.sprite = _grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].BuildingImage;
                    _buildings.Add(b);
                }
            }
        }
    }
    private BuildingSprite FindBuilding(Buildings building)
    {
        foreach (BuildingSprite buildingSprite in BuildingSprites)
        {
            if (buildingSprite.BuildingType == building) return buildingSprite;
        }
        return null;
    }
}
