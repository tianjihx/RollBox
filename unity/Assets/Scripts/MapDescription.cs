using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDescription
{
    public MapDescription()
    {
        terrainDict = new Dictionary<MapVector2, TerrainType>();
        elementDict = new Dictionary<MapVector2, ElementType>();
    }

    public Dictionary<MapVector2, TerrainType> terrainDict;
    public Dictionary<MapVector2, ElementType> elementDict;

    public MapVector2 Size;

    public void addTerrain(MapVector2 pos, TerrainType type)
    {
        if (!terrainDict.ContainsKey(pos))
        {
            terrainDict.Add(pos, type);
        }
        else
        {
            Debug.LogError(pos + "已经存在地形" + type.ToString() + "，请勿重复添加！");
        }
    }

    public void addElement(MapVector2 pos, ElementType type)
    {
        if (!elementDict.ContainsKey(pos))
        {
            elementDict.Add(pos, type);
        }
        else
        {
            Debug.LogError(pos + "已经存在元素" + type.ToString() + "，请勿重复添加！");
        }
    }

}
