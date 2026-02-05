using UnityEngine;

public class MinimapIconManager : MonoBehaviour
{
    public Sprite iconSprite;
    public Color iconColor = Color.yellow;
    public float iconScale = 2f;
    public string iconLayerName = "MinimapIcon";

    private GameObject iconObject;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        CreateIcon();
    }

    void CreateIcon()
    {
        iconObject = new GameObject("MinimapIcon");
        iconObject.transform.SetParent(this.transform);
        iconObject.transform.localPosition = new Vector3(0, 50, 0);
        iconObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        iconObject.transform.localScale = Vector3.one * iconScale;

        int layer = LayerMask.NameToLayer(iconLayerName);
        if (layer != -1) iconObject.layer = layer;

        if (iconSprite != null)
        {
            spriteRenderer = iconObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = iconSprite;
            spriteRenderer.color = iconColor;
        }
        else
        {
            // 스프라이트가 없을 경우 기본 삼각형(네비게이터 스타일) 메쉬 생성
            MeshFilter meshFilter = iconObject.AddComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[] {
                new Vector3(0, 0.7f, 0),     // 위 (0)
                new Vector3(-0.5f, -0.5f, 0),// 왼쪽 아래 (1)
                new Vector3(0, -0.1f, 0),    // 안쪽 뒤 (2)
                new Vector3(0.5f, -0.5f, 0)  // 오른쪽 아래 (3)
            };
            // 두 개의 삼각형으로 모양 정의
            mesh.triangles = new int[] { 
                0, 2, 1, // 왼쪽 절반
                0, 3, 2  // 오른쪽 절반
            };
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;

            MeshRenderer meshRenderer = iconObject.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
            meshRenderer.material.color = iconColor;
        }
    }

    void LateUpdate()
    {
        if (iconObject != null)
        {
            // 아이콘은 자식 오브젝트이므로 플레이어의 위치를 자동으로 따라갑니다.
            // 플레이어의 Y축 회전(바라보는 방향)만 일치시키고, 
            // 미니맵 카메라를 위해 X, Z축은 평평하게 유지합니다.
            iconObject.transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
        }
    }
}
