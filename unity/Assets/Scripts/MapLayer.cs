using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLayer : MonoBehaviour
{
    [Header("第几层")]
    public int m_Floor;

    [SerializeField]
    private int m_Row;
    [SerializeField]
    private int m_Col;


    private void Awake()
    {
        Clear();
    }

    public void CreateUnit(BlockUnit template, MapVector2 pos)
    {
        BlockUnit newUnit = Instantiate(template.gameObject).GetComponent<BlockUnit>();
        newUnit.transform.SetParent(transform);
        newUnit.Position = new Vector3(pos.x, pos.y, m_Floor);

    }

    public MapVector2 Size
    {
        get
        {
            return new MapVector2(m_Row, m_Col);
        }
        set
        {
            m_Row = value.x;
            m_Col = value.y;
        }
    }

    

    public void Clear()
    {
        while (transform.childCount > 0)
        {
            GameObject go = transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
