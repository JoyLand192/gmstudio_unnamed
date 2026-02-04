using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateBaseFolder : EditorWindow
{
    [MenuItem("4sat's Tools/Create Base Folder")]
    private static void MenuOpen()
    {
        GetWindow<CreateBaseFolder>("Folder Generator");
    }

    private void OnGUI()
    {
        // 라벨 만들기
        GUILayout.Label("선택한 오브젝트의 이름 변경", EditorStyles.boldLabel);

        // 버튼 만들기
        if (GUILayout.Button("폴더 생성"))
        {
            CreateFolder();
        }
    }

    // 폴더 만들기
    private void CreateFolder()
    {
        string[] folderName =
        {
            "01. Scenes",
            "02. Scripts",
            "03. Prefabs",
            "04. Animations",
            "05. Materials",
            "06. Textures",
            "07. Audioes",
            "08. Fonts"
        };

        foreach (string folder in folderName)
        {
            if (! AssetDatabase.IsValidFolder($"Assets/{folder}"))
            {
                AssetDatabase.CreateFolder("Assets", folder);
            }
        }

        // 프로젝트 탭 새로고침
        AssetDatabase.Refresh();
        Debug.Log("기본 폴더 생성 완료");
    }
}
