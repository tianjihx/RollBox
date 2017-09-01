using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var map = target as MapManager;

        base.DrawDefaultInspector();
        if (GUILayout.Button("生成测试Map", GUILayout.ExpandWidth(true)))
        {
            MapDescription desc;
            map.TryParseMapFile(map.m_testMapFile.text, out desc);
            map.LoadMap(desc);
        }
        if (GUILayout.Button("清空Map", GUILayout.ExpandWidth(true)))
        {
            map.ClearMap();
        }
    }
}