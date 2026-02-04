using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
