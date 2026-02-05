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

        // 1. 렌더 텍스처 생성
        string rtPath = "Assets/06. Textures/MinimapRT.renderTexture";
        RenderTexture rt = AssetDatabase.LoadAssetAtPath<RenderTexture>(rtPath);
        if (rt == null)
        {
            rt = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
            rt.Create();
            AssetDatabase.CreateAsset(rt, rtPath);
            Debug.Log("미니맵 렌더 텍스처 생성 완료: " + rtPath);
        }

        // 2. 미니맵 카메라 생성
        GameObject camObj = GameObject.Find("MapCamera");
        int playerLayer = LayerMask.NameToLayer("Player");
        int mask = (playerLayer != -1) ? ~(1 << playerLayer) : -1;

        if (camObj == null)
        {
            camObj = new GameObject("MapCamera");
            Camera cam = camObj.AddComponent<Camera>();
            camObj.transform.position = new Vector3(0, 100, 0);
            camObj.transform.rotation = Quaternion.Euler(90, 0, 0); // 탑다운 뷰
            cam.orthographic = true;
            cam.orthographicSize = 50;
            cam.targetTexture = rt;
            // 플레이어 레이어는 숨기고, 미니맵 아이콘 레이어를 포함한 나머지는 표시
            cam.cullingMask = mask; 
            Debug.Log("미니맵 카메라 생성 완료 (플레이어 숨김 마스크 적용)");
        }
        else
        {
            Camera cam = camObj.GetComponent<Camera>();
            if (cam == null) cam = camObj.AddComponent<Camera>();
            cam.targetTexture = rt;
            camObj.transform.rotation = Quaternion.Euler(90, 0, 0);
            cam.cullingMask = mask;
        }

        // 3. UI 설정
        GameObject canvasObj = GameObject.Find("MapCanvas");
        if (canvasObj == null)
        {
            Canvas[] canvases = Object.FindObjectsOfType<Canvas>();
            if (canvases.Length > 0)
            {
                // "Canvas"라는 이름의 캔버스가 있으면 사용, 없으면 첫 번째 캔버스 사용
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
            
            Debug.Log("미니맵 패널 생성 완료");
        }
        else
        {
            mapPanel = mapPanelTr.gameObject;
            RawImage rawImg = mapPanel.GetComponent<RawImage>();
            if (rawImg == null) rawImg = mapPanel.AddComponent<RawImage>();
            rawImg.texture = rt;
        }

        // 자기 자신 삭제 (설정용 스크립트이므로)
        DestroyImmediate(this.gameObject);
#endif
    }
}
