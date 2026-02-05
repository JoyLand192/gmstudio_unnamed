using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // 맵을 보여줄 캔버스 (MapCanvas)
    public GameObject MapCanvas;

    public void Update()
    {
        // 탭(Tab) 키를 누르고 있는 동안 맵을 엽니다
        if (Input.GetKey(KeyCode.M))
        {
            MapOpen();
        }
        else
        {
            // 키를 떼면 맵을 닫습니다
            MapCanvas.SetActive(false);
        }
    }

    // 맵을 활성화하는 함수
    private void MapOpen()
    {
        MapCanvas.SetActive(true);
    }
}
