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
                _grid[x, y] = new BuildingSprite("Unset", DefaultImage, Buildings.Unset);
            }
        }
    }

    public void SetBuildings()
    {
        var buildingCounter = totalBuildings;
        int hospitals = totalBuildings / 20;
        int schools = totalBuildings / 10;
        int offices = totalBuildings / 5;
        locations = new Vector2[totalBuildings];
        while (buildingCounter != 0)
        {
            if (hospitals != 0)
            {
                int randomSpawnPointx = Random.Range(0, (int)MapSize.x - 4);
                int randomSpawnPointy = Random.Range(0, (int)MapSize.y - 3);
                if ((_grid[randomSpawnPointx, randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 2)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 2)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), (randomSpawnPointy + 2)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 3), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 3), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 3), (randomSpawnPointy + 2)].BuildingType == Buildings.Unset))
                {
                    _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[3].Name, BuildingSprites[3].BuildingImage, BuildingSprites[3].BuildingType);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 3), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 3), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 3), (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    hospitals--;
                    buildingCounter--;
                }
            }
            else if (schools != 0)
            {
                int randomSpawnPointx = Random.Range(0, (int)MapSize.x - 3);
                int randomSpawnPointy = Random.Range(0, (int)MapSize.y - 2);
                if ((_grid[randomSpawnPointx, randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset))
                {
                    _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[2].Name, BuildingSprites[2].BuildingImage, BuildingSprites[2].BuildingType);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    schools--;
                    buildingCounter--;
                }
            }
            else if (offices != 0)
            {
                int randomSpawnPointx = Random.Range(0, (int)MapSize.x - 3);
                int randomSpawnPointy = Random.Range(0, (int)MapSize.y - 3);
                if ((_grid[randomSpawnPointx, randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 2)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 2), (randomSpawnPointy + 2)].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 2)].BuildingType == Buildings.Unset))
                {
                    _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[1].Name, BuildingSprites[1].BuildingImage, BuildingSprites[1].BuildingType);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 2), (randomSpawnPointy + 2)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    offices--;
                    buildingCounter--;
                }
            }
            else
            {
                int randomSpawnPointx = Random.Range(0, (int)MapSize.x - 2);
                int randomSpawnPointy = Random.Range(0, (int)MapSize.y - 2);
                if ((_grid[randomSpawnPointx, randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), randomSpawnPointy].BuildingType == Buildings.Unset)
                    && (_grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)].BuildingType == Buildings.Unset)
                    && (_grid[randomSpawnPointx, (randomSpawnPointy + 1)].BuildingType == Buildings.Unset))
                {
                    _grid[randomSpawnPointx, randomSpawnPointy] = new BuildingSprite(BuildingSprites[0].Name, BuildingSprites[0].BuildingImage, BuildingSprites[0].BuildingType);
                    _grid[(randomSpawnPointx + 1), randomSpawnPointy] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[randomSpawnPointx, (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    _grid[(randomSpawnPointx + 1), (randomSpawnPointy + 1)] = new BuildingSprite("Taken", DefaultImage, Buildings.Taken);
                    buildingCounter--;
                }

            }
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

                if ((_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name != "Unset")
                    && (_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name != "Taken"))
                {
                    var b = LeanPool.Spawn(BuildingPrefab);
                    b.transform.position = new Vector3(tX, tY, 0);
                    b.transform.SetParent(_buildingContainer.transform);
                    var renderer = b.GetComponent<SpriteRenderer>();
                    renderer.sprite = _grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].BuildingImage;

                    b.AddComponent<BoxCollider2D>();
                    var boxCollider = b.GetComponent<BoxCollider2D>();
                    float spriteWidth = (float)_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].BuildingImage.rect.width / 100;
                    float spriteHeight = (float)_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].BuildingImage.rect.height / 100;
                    var spritesize = new Vector2(spriteWidth, spriteHeight);
                    boxCollider.size = spritesize;

                    b.AddComponent<Rigidbody2D>();
                    var rigidBody = b.GetComponent<Rigidbody2D>();
                    rigidBody.isKinematic = true;

                    //var c = LeanPool.Spawn(BuildingPrefab);
                    //c.transform.position = new Vector3(tX, tY, 0);
                    b.AddComponent<CircleCollider2D>();
                    var avoidRadius = b.GetComponent<CircleCollider2D>();
                    avoidRadius.isTrigger = true;
                    var multiplier = 1.815f;
                    if (_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name == "House")
                        multiplier = multiplier * 1;
                    else if (_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name == "School")
                        multiplier = multiplier * 1.5f;
                    else if (_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name == "Office")
                        multiplier = multiplier * 1.5f;
                    else if (_grid[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].Name == "Hospital")
                        multiplier = multiplier * 2;
                    avoidRadius.radius = multiplier;

                    b.tag = "Building";

                    _buildings.Add(b);
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var zombie = other.gameObject.GetComponent<ZombieMove>();
        zombie.avoid = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var zombie = other.gameObject.GetComponent<ZombieMove>();
        zombie.avoid = false;
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
