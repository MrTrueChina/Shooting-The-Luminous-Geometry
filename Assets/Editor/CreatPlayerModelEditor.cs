using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CreatPlayerModel))]
public class CreatPlayerModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawCreatPlayerModel();
        DrawSavePlayerModel();
    }

    void DrawCreatPlayerModel()
    {
        if (GUILayout.Button("Creat Model"))
        {
            CreatPlayerModel creat = (CreatPlayerModel)target;
            creat.CreatModel();
        }
    }

    void DrawSavePlayerModel()
    {
        if (GUILayout.Button("Save Model"))
        {
            CreatPlayerModel creat = (CreatPlayerModel)target;
            creat.SaveModel();
        }
    }
}
