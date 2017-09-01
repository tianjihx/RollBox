using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : Singleton<BoxManager>
{
    
    [SerializeField]
    private BoxUnit m_BoxUnitTemplate;

    public int m_Floor = 1;

    public List<BoxUnit> boxList = new List<BoxUnit>();

	// Use this for initialization
	void Start () {
        AddBox(new MapVector2(0, 0));
        AddBox(new MapVector2(3, 3));
    }

    // Update is called once per frame
    void Update () {
		
    }

    public void Move(Direction directoin)
    {
        foreach (var box in boxList)
        {
            box.MoveDirection(directoin);
        }
    }

    public void AddBox(MapVector2 spawnPos)
    {
        var box = CreateBox(spawnPos, Color.red);
        boxList.Add(box);
    }

    public BoxUnit CreateBox(MapVector2 pos, Color color)
    {
        BoxUnit newUnit = Instantiate(m_BoxUnitTemplate.gameObject).GetComponent<BoxUnit>();
        newUnit.Position = new Vector3(pos.x, pos.y, m_Floor);
        newUnit.PositionInMap = pos;
        return newUnit;
    }

    /// <summary>
    /// 检查指定底图位置上是否有箱子，有的话返回这个箱子
    /// </summary>
    /// <param name="pos">要检查的位置</param>
    public BoxUnit ExistBoxAtPos(MapVector2 pos)
    {
        Debug.Log("检查运动路径有没有箱子");
        foreach (var box in boxList)
        {
            Debug.Log("box:" + box.PositionInMap);
            Debug.Log("pos:" + pos);
            if (box.PositionInMap == pos)
                return box;
        }
        return null;
    }
}
