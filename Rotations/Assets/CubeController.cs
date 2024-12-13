using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float xRotation = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        float yRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Rotate(xRotation, yRotation, 0, Space.World);
    }

}
