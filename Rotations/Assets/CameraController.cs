using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Referencia al cubo
    public float distance = 5.0f; // Distancia inicial de la cámara
    public float rotationSpeed = 100.0f; // Velocidad de rotación
    public float zoomSpeed = 2.0f; // Velocidad de zoom
    public float minDistance = 2.0f; // Distancia mínima de zoom
    public float maxDistance = 10.0f; // Distancia máxima de zoom

    private float currentAngleX = 0.0f; // Ángulo de rotación horizontal
    private float currentAngleY = 20.0f; // Ángulo de rotación vertical

    void Update()
    {
        // Rotación con el mouse
        if (Input.GetMouseButton(1)) // Botón derecho del mouse
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            currentAngleX += mouseX;
            currentAngleY = Mathf.Clamp(currentAngleY + mouseY, -45f, 45f); // Limita el ángulo vertical
        }

        // Zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // Actualizar la posición y rotación de la cámara
        Quaternion rotation = Quaternion.Euler(currentAngleY, currentAngleX, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;
        transform.position = position;
        transform.LookAt(target);
    }
}
