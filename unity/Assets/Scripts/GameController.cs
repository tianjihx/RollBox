using UnityEngine;

public class GameController : Singleton<GameController>
{

    private void Awake()
    {
    }
    

    private void Start()
    {
        MapDescription desc;
        MapManager.I.TryParseMapFile(MapManager.I.m_testMapFile.text, out desc);
        MapManager.I.LoadMap(desc);
    }

}