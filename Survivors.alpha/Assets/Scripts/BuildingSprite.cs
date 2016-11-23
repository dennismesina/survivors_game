using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class BuildingSprite
{
    public string Name;
    public Sprite BuildingImage;
    public Buildings BuildingType;

    public BuildingSprite()
    {
        Name = "Unset";
        BuildingImage = new Sprite();
        BuildingType = Buildings.Unset;
    }

    public BuildingSprite(string name, Sprite image, Buildings building)
    {
        Name = name;
        BuildingImage = image;
        BuildingType = building;
    }
}
