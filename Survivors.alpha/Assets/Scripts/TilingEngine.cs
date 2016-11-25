using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean;

public class TilingEngine : MonoBehaviour
{
    public List<TileSprite> TileSprites;
    public Vector2 MapSize;
    public Sprite DefaultImage;
    public GameObject TileContainerPrefab;
    public GameObject TilePrefab;
    public Vector2 CurrentPosition;
    public Vector2 ViewPortSize;    
    private float tileWidth = 2.55f;
    private float tileHeight = .91f;

    private int[,] _mapInts;
    private TileSprite[,] _map;
    private GameObject controller;
    private GameObject _tileContainer;
    private List<GameObject> _tiles = new List<GameObject>();
 
    public void Start()
    {
        controller = GameObject.Find("Controller");
        _map = new TileSprite[(int)MapSize.x, (int)MapSize.y];

        DefaultTiles();
        SetTiles();
        AddTilesToWorld();
    }

    private void DefaultTiles()
    {
        _mapInts = new int[(int)MapSize.x, (int)MapSize.y]; //For refactoring - try to connect ints to map, then initialize by going through ints. -Dennis
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                _map[x, y] = new TileSprite("unset", DefaultImage, Tiles.Unset);
            }
        }
    }

    private void SetTiles()
    {
        var tinyRoad1x = 10;
        var tinyRoad2x = 5;
        var tinyRoad3x = 22;
        bool tophalf = false;
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            if (y % 2 == 1)
            {
                for (var x = 0; x < MapSize.x - 1; x++)
                {
                    _map[x, y] = new TileSprite(TileSprites[0].Name, TileSprites[0].TileImage, TileSprites[0].TileType);
                }
            }
            else
            {
                for (var x = 0; x < MapSize.x - 1; x++)
                {
                    _map[x, y] = new TileSprite(TileSprites[1].Name, TileSprites[1].TileImage, TileSprites[1].TileType);
                }
            }
        }
        int centerRight =  (int) ViewPortSize.x/2; //(int) Random.Range(1, ViewPortSize.x - 1);
        int centerLeft = (int)ViewPortSize.x / 2;
        for (var y = 0; y < MapSize.y - 1; y++) //this loop for setting roads
        {
            if (!tophalf)
            {
                if (y % 2 == 1)
                {
                    if (centerRight < (int)MapSize.y / 2)
                    {
                        _map[centerRight - 2, y + 1] = new TileSprite(TileSprites[2].Name, TileSprites[2].TileImage, TileSprites[2].TileType);
                        _map[centerRight - 1, y] = new TileSprite(TileSprites[3].Name, TileSprites[3].TileImage, TileSprites[3].TileType);
                    }
                    if (centerLeft >= 1)
                    {
                        _map[centerLeft - 1, y] = new TileSprite(TileSprites[4].Name, TileSprites[4].TileImage, TileSprites[4].TileType);
                        _map[centerLeft, y] = new TileSprite(TileSprites[5].Name, TileSprites[5].TileImage, TileSprites[5].TileType);
                    }
                    else
                    {
                        tophalf = true;
                    }
                    centerLeft--;
                }
                else
                {
                    if (centerRight < (int)MapSize.y / 2)
                    {
                        _map[centerRight - 1, y + 1] = new TileSprite(TileSprites[2].Name, TileSprites[2].TileImage, TileSprites[2].TileType);
                        _map[centerRight - 1, y] = new TileSprite(TileSprites[3].Name, TileSprites[3].TileImage, TileSprites[3].TileType);
                    }
                    if (centerLeft >= 1)
                    {
                        _map[centerLeft - 1, y] = new TileSprite(TileSprites[4].Name, TileSprites[4].TileImage, TileSprites[4].TileType);
                        _map[centerLeft, y] = new TileSprite(TileSprites[5].Name, TileSprites[5].TileImage, TileSprites[5].TileType);
                    }
                    else
                    {
                        tophalf = true;
                    }
                    centerRight++;
                }
            }
            else
            {
                if (y % 2 == 1)
                {
                    if (centerLeft <= (int)MapSize.x/2)
                    {
                        _map[centerLeft, y] = new TileSprite(TileSprites[2].Name, TileSprites[2].TileImage, TileSprites[2].TileType);
                        _map[centerLeft + 1, y] = new TileSprite(TileSprites[3].Name, TileSprites[3].TileImage, TileSprites[3].TileType);
                    }
                    if (centerRight >= (int)MapSize.x/2)
                    {
                        _map[centerRight - 2, y-2] = new TileSprite(TileSprites[4].Name, TileSprites[4].TileImage, TileSprites[4].TileType);
                        _map[centerRight - 2, y] = new TileSprite(TileSprites[5].Name, TileSprites[5].TileImage, TileSprites[5].TileType);
                    }
                    centerRight--;
                }
                else
                {
                    if (centerLeft < (int)MapSize.x / 2)
                    {
                        _map[centerLeft, y] = new TileSprite(TileSprites[2].Name, TileSprites[2].TileImage, TileSprites[2].TileType);
                        _map[centerLeft + 1, y] = new TileSprite(TileSprites[3].Name, TileSprites[3].TileImage, TileSprites[3].TileType);
                    }
                    if (centerRight >= (int)MapSize.x / 2)
                    {
                        _map[centerRight - 2, y-2] = new TileSprite(TileSprites[4].Name, TileSprites[4].TileImage, TileSprites[4].TileType);
                        _map[centerRight - 2, y] = new TileSprite(TileSprites[5].Name, TileSprites[5].TileImage, TileSprites[5].TileType);
                    }
                    centerLeft++;
                }
            }
            if (y > 13 && y < 43) //this loop for tiny road1
            {
                if (y % 2 == 0)
                {
                    _map[tinyRoad1x, y] = new TileSprite(TileSprites[6].Name, TileSprites[6].TileImage, TileSprites[6].TileType);
                }
                else
                {
                    _map[tinyRoad1x + 1, y] = new TileSprite(TileSprites[6].Name, TileSprites[6].TileImage, TileSprites[6].TileType);
                    tinyRoad1x++;
                }
            }
            if(y > 22 && y < 52) //this loop for tiny road1
            {
                if (y % 2 == 0)
                {
                    _map[tinyRoad2x, y] = new TileSprite(TileSprites[6].Name, TileSprites[6].TileImage, TileSprites[6].TileType);
                }
                else
                {
                    _map[tinyRoad2x + 1, y] = new TileSprite(TileSprites[6].Name, TileSprites[6].TileImage, TileSprites[6].TileType);
                    tinyRoad2x++;
                }
            }
            if (y > 15 && y < 45) //this loop for tiny road1
            {
                if (y % 2 == 0)
                {
                    _map[tinyRoad3x - 1, y] = new TileSprite(TileSprites[7].Name, TileSprites[7].TileImage, TileSprites[7].TileType);
                    tinyRoad3x--;
                }
                else
                {
                    _map[tinyRoad3x, y] = new TileSprite(TileSprites[7].Name, TileSprites[7].TileImage, TileSprites[7].TileType);
                }
            }
        }
        _map[22, 15] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[10, 13] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[5, 22] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[16, 26] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[12, 35] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[20, 52] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[25, 43] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
        _map[7, 45] = new TileSprite(TileSprites[8].Name, TileSprites[8].TileImage, TileSprites[8].TileType);
    }

    private void Update()
    {
        //AddTilesToWorld(); //If this gets commented out, comment out the random street generator inside Start()
    }

    private void AddTilesToWorld()
    {
        foreach (GameObject o in _tiles)
        {
            LeanPool.Despawn(o);
        }
        _tiles.Clear();
        LeanPool.Despawn(_tileContainer);
        _tileContainer = LeanPool.Spawn(TileContainerPrefab);
        var viewOffsetX = ViewPortSize.x/2f;
        var viewOffsetY = ViewPortSize.y/2f;
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

                    var t = LeanPool.Spawn(TilePrefab);
                    t.transform.position = new Vector3(tX, tY, 0);
                    t.transform.SetParent(_tileContainer.transform);
                    var renderer = t.GetComponent<SpriteRenderer>();
                    renderer.sprite = _map[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].TileImage;
                    _tiles.Add(t);
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

                    var t = LeanPool.Spawn(TilePrefab);
                    t.transform.position = new Vector3(tX, tY, 0);
                    t.transform.SetParent(_tileContainer.transform);
                    var renderer = t.GetComponent<SpriteRenderer>();
                    renderer.sprite = _map[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].TileImage;
                    _tiles.Add(t);
                }
            }
        }
    }

    private TileSprite FindTile(Tiles tile)
    {
        foreach (TileSprite tileSprite in TileSprites)
        {
            if (tileSprite.TileType == tile) return tileSprite;
        }
        return null;
    }
}
