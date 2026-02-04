using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class MinimapSetup : MonoBehaviour
{
    void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;

        // 1. Create Render Texture
        string rtPath = "Assets/06. Textures/MinimapRT.renderTexture";
        RenderTexture rt = AssetDatabase.LoadAssetAtPath<RenderTexture>(rtPath);
        if (rt == null)
        {
            rt = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
            rt.Create();
            AssetDatabase.CreateAsset(rt, rtPath);
            Debug.Log("Created MinimapRT at " + rtPath);
        }

        // 2. Create Map Camera
        GameObject camObj = GameObject.Find("MapCamera");
        if (camObj == null)
        {
            camObj = new GameObject("MapCamera");
            Camera cam = camObj.AddComponent<Camera>();
            camObj.transform.position = new Vector3(0, 100, 0);
            camObj.transform.rotation = Quaternion.Euler(90, 0, 0);
            cam.orthographic = true;
            cam.orthographicSize = 50;
            cam.targetTexture = rt;
            cam.cullingMask = -1; 
            Debug.Log("Created MapCamera");
        }
        else
        {
            Camera cam = camObj.GetComponent<Camera>();
            if (cam == null) cam = camObj.AddComponent<Camera>();
            cam.targetTexture = rt;
        }

        // 3. Setup UI
        GameObject canvasObj = GameObject.Find("MapCanvas");
        if (canvasObj == null)
        {
            Canvas[] canvases = Object.FindObjectsOfType<Canvas>();
            if (canvases.Length > 0)
            {
                // Prefer one named "Canvas" or just the first one
                canvasObj = canvases[0].gameObject;
            }
            else
            {
                canvasObj = new GameObject("MapCanvas");
                Canvas c = canvasObj.AddComponent<Canvas>();
                c.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }
        }

        Transform mapPanelTr = canvasObj.transform.Find("MapPanel");
        GameObject mapPanel;
        if (mapPanelTr == null)
        {
            mapPanel = new GameObject("MapPanel");
            mapPanel.transform.SetParent(canvasObj.transform, false);
            RawImage rawImg = mapPanel.AddComponent<RawImage>();
            rawImg.texture = rt;
            
            RectTransform rect = mapPanel.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400, 400); 
            
            Debug.Log("Created MapPanel");
        }
        else
        {
            mapPanel = mapPanelTr.gameObject;
            RawImage rawImg = mapPanel.GetComponent<RawImage>();
            if (rawImg == null) rawImg = mapPanel.AddComponent<RawImage>();
            rawImg.texture = rt;
        }

        // Cleanup self
        DestroyImmediate(this.gameObject);
#endif
    }
}
