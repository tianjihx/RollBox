using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    Left, Up, Right, Down
}


public enum MapLayerType
{
    Terrain, Element, Box
}

public enum TerrainType
{
    Void = 0,
    Ground = 1,
    Water = 2
}

public enum ElementType
{
    None = 0,
    Wall = 1,
    Target = 2,
    Transport = 3
}

public class MapVector2
{
    public static MapVector2 Zero { get { return new MapVector2(); } }

    public MapVector2()
    {
        x = 0; 
        y = 0;
    }

    public MapVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int x;
    public int y;

    public override bool Equals(object obj)
    {
        var that = obj as MapVector2;
        if (this.x == that.x && this.y == that.y)
            return true;
        else
            return false;   
    }

    public override int GetHashCode()
    {
        return this.x * 100000 + this.y; //100000作为map的上限大小
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    public static bool operator==(MapVector2 left, MapVector2 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MapVector2 left, MapVector2 right)
    {
        return !left.Equals(right);
    }

    public static MapVector2 operator +(MapVector2 left, MapVector2 right)
    {
        return new MapVector2(left.x + right.x, left.y + right.y);
    }

    public static MapVector2 operator -(MapVector2 left, MapVector2 right)
    {
        return new MapVector2(left.x - right.x, left.y - right.y);
    }
}
