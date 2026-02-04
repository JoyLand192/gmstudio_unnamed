using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject MapCanvas;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            MapOpen();
        }
        else
        {
            MapCanvas.SetActive(false);
        }
    }

    private void MapOpen()
    {
        MapCanvas.SetActive(true);
    }
}
