using UnityEngine;
using UnityEngine.UI;

public class FogOfWarManager : MonoBehaviour
{
    public Transform player;
    public Camera mapCamera;
    public RawImage mapRawImage;
    
    // The texture that holds the explored state (White = Explored, Black = Fog)
    public RenderTexture fogRT; 
    
    // The brush texture (Soft circle, White)
    public Texture2D brushTexture;
    public float brushSize = 0.1f; // proportional to map size (0-1)

    private Material drawMaterial;
    private Material fogDisplayMaterial;
    public Shader fogShader;

    void Start()
    {
        if (fogShader == null) fogShader = Shader.Find("Custom/MinimapFog");

        // 1. Setup Fog RenderTexture
        if (fogRT == null)
        {
            fogRT = new RenderTexture(512, 512, 0, RenderTextureFormat.R8);
            fogRT.name = "FogMaskRT";
            // Initialize black
            Graphics.SetRenderTarget(fogRT);
            GL.Clear(false, true, Color.black);
            Graphics.SetRenderTarget(null);
        }

        // 2. Setup Display Material
        if (mapRawImage != null)
        {
            fogDisplayMaterial = new Material(fogShader);
            fogDisplayMaterial.SetTexture("_MainTex", mapRawImage.texture); // The camera output
            fogDisplayMaterial.SetTexture("_MaskTex", fogRT);
            mapRawImage.material = fogDisplayMaterial;
            // RawImage still needs the camera RT as 'texture' for UI layout, but Shader uses it as MainTex
        }

        // 3. Setup Brush (Generate if null)
        if (brushTexture == null)
        {
            brushTexture = CreateCircleTexture(64);
        }
    }

    void Update()
    {
        if (player == null || mapCamera == null) return;

        // Convert Player World Pos -> Viewport Pos (0,0 to 1,1)
        Vector3 viewPos = mapCamera.WorldToViewportPoint(player.position);
        
        // Check bounds
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            DrawFog(viewPos.x, viewPos.y);
        }
    }

    void DrawFog(float x, float y)
    {
        // Simple blit using GL or Graphics.DrawTexture logic would be complex in Update.
        // Easiest is using a temporary RenderTexture and a shader, OR simple Blit with offset.
        // Here we use Graphics.SetRenderTarget to draw the brush quad.
        
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = fogRT;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, 512, 512, 0); // Setup pixel coordinates

        float drawX = x * 512;
        float drawY = (1 - y) * 512; // Flip Y for GL
        float drawSize = brushSize * 512;

        Graphics.DrawTexture(
            new Rect(drawX - drawSize/2, drawY - drawSize/2, drawSize, drawSize), 
            brushTexture
        );

        GL.PopMatrix();
        RenderTexture.active = prev;
    }

    Texture2D CreateCircleTexture(int size)
    {
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Color[] colors = new Color[size * size];
        float center = size / 2f;
        float radius = size / 2f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                float alpha = Mathf.Clamp01(1 - (dist / radius));
                alpha = Mathf.Pow(alpha, 2); // Soft edge
                colors[y * size + x] = new Color(1, 1, 1, alpha);
            }
        }
        tex.SetPixels(colors);
        tex.Apply();
        return tex;
    }
}
