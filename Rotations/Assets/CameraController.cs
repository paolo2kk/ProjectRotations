using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Referencia al cubo
    public float distance = 5.0f; // Distancia inicial de la c�mara
    public float rotationSpeed = 100.0f; // Velocidad de rotaci�n
    public float zoomSpeed = 2.0f; // Velocidad de zoom
    public float minDistance = 2.0f; // Distancia m�nima de zoom
    public float maxDistance = 10.0f; // Distancia m�xima de zoom

    private float currentAngleX = 0.0f; // �ngulo de rotaci�n horizontal
    private float currentAngleY = 20.0f; // �ngulo de rotaci�n vertical

    void Update()
    {
        // Rotaci�n con el mouse
        if (Input.GetMouseButton(1)) // Bot�n derecho del mouse
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            currentAngleX += mouseX;
            currentAngleY = Mathf.Clamp(currentAngleY + mouseY, -45f, 45f); // Limita el �ngulo vertical
        }

        // Zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // Actualizar la posici�n y rotaci�n de la c�mara
        Quaternion rotation = Quaternion.Euler(currentAngleY, currentAngleX, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;
        transform.position = position;
        transform.LookAt(target);
    }
}
