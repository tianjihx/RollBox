using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : Singleton<MapManager>{
    

    [SerializeField]
    private MapLayer m_Terrain;
    [SerializeField]
    private MapLayer m_Element;

    [SerializeField]
    private GroundUnit m_GroundUnitTemplate;
    [SerializeField]
    private WaterUnit m_WaterUnitTemplate;
    [SerializeField]
    private WallUnit m_WallUnitTemplate;
    [SerializeField]
    private TargetUnit m_TargetUnitTemplate;

    private MapVector2 mapSize;
    public MapVector2 MapSize
    {
        get
        {
            return mapSize;
        }
    }

    [SerializeField]
    public TextAsset m_testMapFile;


    private TerrainType[,] terrainLayer;
    private ElementType[,] elementLayer;

    private void Awake()
    {
        if (m_Terrain == null)
        {
            Debug.LogError("请关联Terrain组件！");
        }
        //ClearLayer(terrainLayer, TerrainType.Void);
        //ClearLayer(elementLayer, ElementType.None);
    }
    
    public TerrainType GetTerrainAt(int x, int y)
    {
        return terrainLayer[x, y];
    }

    public ElementType GetElementAt(int x, int y)
    {
        return elementLayer[x, y];
    }

    public void ClearMap()
    {
        m_Terrain.Clear();
        m_Element.Clear();
    }

    public void LoadMap(MapDescription desc)
    {
        mapSize = desc.Size;
        //加载地形
        m_Terrain.Size = MapSize;
        terrainLayer = new TerrainType[MapSize.x, MapSize.y];
        generateTerrain(desc.terrainDict);

        //加载元素
        m_Element.Size = MapSize;
        elementLayer = new ElementType[MapSize.x, MapSize.y];
        generateElement(desc.elementDict);
    }

    private void generateTerrain(Dictionary<MapVector2, TerrainType> dict)
    {
        foreach (var entry in dict)
        {
            var pos = entry.Key;
            var type = entry.Value;
            terrainLayer[pos.x, pos.y] = type;
            switch (type)
            {
                case TerrainType.Ground:
                    m_Terrain.CreateUnit(m_GroundUnitTemplate, pos);
                    break;
                case TerrainType.Void:
                    break;
                case TerrainType.Water:
                    m_Terrain.CreateUnit(m_WaterUnitTemplate, pos);
                    break;
            }
        }
    }

    private void generateElement(Dictionary<MapVector2, ElementType> dict)
    {
        foreach (var entry in dict)
        {
            var pos = entry.Key;
            var type = entry.Value;
            elementLayer[pos.x, pos.y] = type;
            switch (type)
            {
                case ElementType.None:
                    break;
                case ElementType.Wall:
                    m_Element.CreateUnit(m_WallUnitTemplate, pos);
                    break;
                case ElementType.Target:
                    m_Element.CreateUnit(m_TargetUnitTemplate, pos);
                    break;
                case ElementType.Transport:
                    break;
            }
        }
    }

    public bool TryParseMapFile(string mapFileContent, out MapDescription desc)
    {
        desc = new MapDescription();
        var lines = mapFileContent.Replace("\r", "").Split('\n');
        try
        {
            //第一行为宽和高
            var pair = lines[0].Split(' ');
            int width = int.Parse(pair[0]);
            int height = int.Parse(pair[1]);
            desc.Size = new MapVector2(width, height);
            //访问接下来的两个矩阵，分别代表Terrain和Element
            for (int x = 0; x < height; ++x)
            {
                
                var terrainUnits = lines[height - x].Split(' ');
                var elementUnits = lines[height*2 + 1 - x].Split(' ');
                for (int y = 0; y < width; ++y)
                {
                    //添加Terrain
                    int terrainUnitType = int.Parse(terrainUnits[y]);
                    TerrainType terrainType = (TerrainType)terrainUnitType;
                    desc.addTerrain(new MapVector2(x, y), terrainType);
                    //添加Element
                    int elementUnitType = int.Parse(elementUnits[y]);
                    ElementType elementType = (ElementType)elementUnitType;
                    desc.addElement(new MapVector2(x, y), elementType);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
            return false;
        }
        return true;
    }

}
