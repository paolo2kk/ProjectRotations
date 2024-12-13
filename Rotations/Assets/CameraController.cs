using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(h, 0, v);

        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}