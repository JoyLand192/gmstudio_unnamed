using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class EasyHierarchy
{
    private const int Row_Height = 16;
    private const int offset_Y = -4;

    private static readonly Color color = new Color(0, 0, 0, 0.08f);

    // 생성자 : 클래스가 인스턴싱 될 때 자동으로 호출되는 함수 ex) Start
    static EasyHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instance, Rect rect)
    {
        int index = (int)(rect.y + offset_Y) / Row_Height;

        if (index % 2 == 0) return;

        float xMax = rect.xMax;

        rect.x = 32;
        rect.xMax = xMax + 16;

        EditorGUI.DrawRect(rect, color);
    }
}
