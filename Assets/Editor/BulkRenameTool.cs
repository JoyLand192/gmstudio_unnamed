using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BulkRenameTool : EditorWindow
{
    private string prefix = "Object";
    private int startIndex = 1;

    [MenuItem("4sat's Tools/BulkRename")]
    private static void MenuOpen()
    {
        GetWindow<BulkRenameTool>("Rename");
    }

    private void OnGUI()
    {
        // 라벨 만들기
        GUILayout.Label("선택한 오브젝트의 이름 변경", EditorStyles.boldLabel);

        // 접두사 변경
        prefix = EditorGUILayout.TextField("접두어", prefix);

        // 시작 인덱스 변경
        startIndex = EditorGUILayout.IntField("시작 인덱스", startIndex);

        // 버튼 만들기
        if (GUILayout.Button("이름 변경"))
        {
            RenameObjects();
        }
    }

    private void RenameObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogError("선택된 오브젝트가 없음");
            return;
        }

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string number = (startIndex + i).ToString("00");
            selectedObjects[i].name = $"{prefix}_{number}";
        }
        Debug.Log($"{selectedObjects.Length}개의 이름 변경 완료");
    }
}
