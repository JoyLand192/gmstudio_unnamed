using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 오브젝트에 추가된 컴포넌트를 아이콘으로 표시
/// </summary>

public static class ShowComponents
{
    private static readonly Color disabledColor = new Color(1, 1, 1, 0.5f); // 비활성화 색상

    // 16*16 사이즈
    private const int width = 16;
    private const int height = 16;

    [InitializeOnLoadMethod]
    private static void Show()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instance, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instance) as GameObject;

        if (obj == null) return;

        Rect position = selectionRect;
        position.x = position.xMax - width;
        position.width = width;
        position.height = height;

        // 오브젝트에 있는 컴포넌트를 역순으로 배치 (Transform 제외)
        var components = obj.GetComponents<Component>().Where(c => c != null).Where(c => c != (c is Transform)).Reverse();

        var current = Event.current;

        foreach(Component c in components)
        {
            Texture image = AssetPreview.GetMiniThumbnail(c);

            if (image == null && c is MonoBehaviour)
            {
                var ms = MonoScript.FromMonoBehaviour(c as MonoBehaviour);
                string path = AssetDatabase.GetAssetPath(ms);
                image = AssetDatabase.GetCachedIcon(path);
            }

            if (image == null)
            {
                continue;
            }

            Color color = GUI.color;
            GUI.color = c.IsEnabled() ? Color.white : disabledColor;
            GUI.DrawTexture(position, image, ScaleMode.ScaleToFit);
            GUI.color = color;
            position.x -= position.width;
        }
    }

    // 컴포넌트가 비활성화인지 (컴포넌트의 활성화 상태를 가져옴)
    public static bool IsEnabled(this Component self)
    {
        if (self == null) return true;

        var type = self.GetType();
        var property = type.GetProperty("enabled", typeof(bool));

        if (property == null) return true;

        return (bool)property.GetValue(self, null);
    }
}
