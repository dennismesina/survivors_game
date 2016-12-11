using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean;

public class sqTileEngine : MonoBehaviour {

    public List<TileSprite> TileSprites;
    public Vector2 MapSize;
    public Sprite DefaultImage;
    public GameObject TileContainerPrefab;
    public GameObject TilePrefab;
    public Vector2 CurrentPosition;
    public Vector2 ViewPortSize;
    private float tileWidth = 1.28f;
    private float tileHeight = 1.28f;

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
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                if(y % 2 == 0 && x % 2 == 0)
                    _map[x, y] = new TileSprite(TileSprites[0].Name, TileSprites[0].TileImage, TileSprites[0].TileType);
                else if(y % 2 == 0 && x % 2 == 1)
                    _map[x, y] = new TileSprite(TileSprites[1].Name, TileSprites[1].TileImage, TileSprites[1].TileType);
                else if (y % 2 == 1 && x % 2 == 0)
                    _map[x, y] = new TileSprite(TileSprites[1].Name, TileSprites[1].TileImage, TileSprites[1].TileType);
                else if (y % 2 == 1 && x % 2 == 1)
                    _map[x, y] = new TileSprite(TileSprites[0].Name, TileSprites[0].TileImage, TileSprites[0].TileType);
            }
        }
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

                var t = LeanPool.Spawn(TilePrefab);
                t.transform.position = new Vector3(tX, tY, 0);
                t.transform.SetParent(_tileContainer.transform);
                var renderer = t.GetComponent<SpriteRenderer>();
                renderer.sprite = _map[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].TileImage;
                _tiles.Add(t);
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
