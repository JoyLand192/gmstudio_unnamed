using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 프로젝트 탭의 가독성
/// </summary>

public class EasyProjectTab
{
    // 유니티 프로젝트 실행 시 자동으로 함수 실행
    [InitializeOnLoadMethod]
    private static void ReadAbility()
    {
        // 프로젝트 탭에 GUI요소 추가
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(string grid, Rect selectionRect)
    {
        var index = (int)(selectionRect.y - 4) / 16;

        if (index % 2 == 0) return;

        Rect position = selectionRect;
        position.x = 0;
        position.xMax = selectionRect.xMax;

        Color color = GUI.color;
        GUI.color = new Color(0, 0, 0, 0.35f);
        GUI.Box(position, string.Empty);
        GUI.color = color;
    }
}
