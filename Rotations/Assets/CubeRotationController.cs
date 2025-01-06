using UnityEngine;
using UnityEngine.UI;

public class CubeRotationController : MonoBehaviour
{
    public Transform cube; // Referencia al cubo

    // Panel de Quaternions
    public InputField quaternionW;
    public InputField quaternionW2;
    public InputField quaternionX;
    public InputField quaternionX2;
    public InputField quaternionY;
    public InputField quaternionY2;
    public InputField quaternionZ;
    public InputField quaternionZ2;

    // Panel de Euler
    public InputField eulerYaw;
    public InputField eulerYaw2;
    public InputField eulerPitch;
    public InputField eulerPitch2;
    public InputField eulerRoll;
    public InputField eulerRoll2;

    // Panel de Axis-Angle
    public InputField axisX;
    public InputField axisX2;
    public InputField axisY;
    public InputField axisY2;
    public InputField axisZ;
    public InputField axisZ2;
    public InputField axisAngle;
    public InputField axisAngle2;

    // Panel de Rotation Vector
    public InputField rotationVectorX;
    public InputField rotationVectorX2;
    public InputField rotationVectorY;
    public InputField rotationVectorY2;
    public InputField rotationVectorZ;
    public InputField rotationVectorZ2;

    // Panel de Matriz de Rotación
    public Text rotationMatrixText;

    // Botones
    public Button quaternionButton;
    public Button eulerButton;
    public Button axisAngleButton;
    public Button rotationVectorButton;
    public Button resetButton;

    private Quaternion initialRotation; // Rotación inicial del cubo
    private Quaternion originalQuaternion;

    void Start()
    {
        // Guardar la rotación inicial
        initialRotation = cube.rotation;

        // Vincular botones
        quaternionButton.onClick.AddListener(UpdateQuaternion);
        eulerButton.onClick.AddListener(UpdateEuler);
        axisAngleButton.onClick.AddListener(UpdateAxisAngle);
        rotationVectorButton.onClick.AddListener(UpdateRotationVector);
        resetButton.onClick.AddListener(ResetRotation);

        // Actualizar los valores del Canvas en tiempo real
        UpdateCanvas();
    }

    void Update()
    {
        // Actualizar los valores del Canvas en tiempo real
        UpdateCanvas();
    }

    void UpdateQuaternion()
{
    if (float.TryParse(quaternionW2.text, out float w) &&
        float.TryParse(quaternionX2.text, out float x) &&
        float.TryParse(quaternionY2.text, out float y) &&
        float.TryParse(quaternionZ2.text, out float z))
    {
        originalQuaternion = new Quaternion(x, y, z, w);
        cube.rotation = originalQuaternion.normalized; 
    }
}

    void UpdateEuler()
    {
        if (float.TryParse(eulerYaw2.text, out float yaw) &&
            float.TryParse(eulerPitch2.text, out float pitch) &&
            float.TryParse(eulerRoll2.text, out float roll))
        {
            // Crear rotación basada en ángulos de Euler
            Quaternion eulerRotation = Quaternion.Euler(yaw, pitch, roll);

            // Asignar la rotación normalizada al cubo
            cube.rotation = eulerRotation.normalized;

        }
        else
        {
            Debug.LogError("Error parsing euler inputs");
        }
    }


    void UpdateAxisAngle()
    {
        if (float.TryParse(axisX2.text, out float x) &&
            float.TryParse(axisY2.text, out float y) &&
            float.TryParse(axisZ2.text, out float z) &&
            float.TryParse(axisAngle2.text, out float angle))
        {
            // Normalizar el vector de eje
            Vector3 axis = new Vector3(x, y, z).normalized;

            // Crear rotación basada en el eje y el ángulo
            Quaternion axisAngleRotation = Quaternion.AngleAxis(angle, axis);

            // Asignar la rotación normalizada al cubo
            cube.rotation = axisAngleRotation.normalized;
        }
        else
        {
            Debug.LogError("Error parsing axis-angle inputs");
        }
    }

    void UpdateRotationVector()
    {
        if (float.TryParse(rotationVectorX2.text, out float x) &&
            float.TryParse(rotationVectorY2.text, out float y) &&
            float.TryParse(rotationVectorZ2.text, out float z))
        {
            // Crear un vector de rotación
            Vector3 rotationVector = new Vector3(x, y, z);

            // Calcular la magnitud del vector y convertir a grados
            float angle = rotationVector.magnitude * Mathf.Rad2Deg;

            // Normalizar el vector para obtener el eje de rotación
            Vector3 axis = rotationVector.normalized;

            // Crear rotación basada en el eje y el ángulo
            Quaternion rotationVectorRotation = Quaternion.AngleAxis(angle, axis);

            // Asignar la rotación normalizada al cubo
            cube.rotation = rotationVectorRotation.normalized;
        }
        else
        {
            Debug.LogError("Error parsing rotation vector inputs");
        }
    }


    void ResetRotation()
    {
        // Restaurar la rotación inicial
        cube.rotation = initialRotation;
    }

    void UpdateCanvas()
    {
        // Actualizar Quaternions
        quaternionW.text = cube.rotation.w.ToString("F3");
        quaternionX.text = cube.rotation.x.ToString("F3");
        quaternionY.text = cube.rotation.y.ToString("F3");
        quaternionZ.text = cube.rotation.z.ToString("F3");

        // Actualizar Euler Angles
        Vector3 euler = cube.rotation.eulerAngles;
        eulerYaw.text = euler.x.ToString("F3");
        eulerPitch.text = euler.y.ToString("F3");
        eulerRoll.text = euler.z.ToString("F3");

        // Actualizar Axis-Angle
        cube.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        axisX.text = axis.x.ToString("F3");
        axisY.text = axis.y.ToString("F3");
        axisZ.text = axis.z.ToString("F3");
        axisAngle.text = angle.ToString("F3");

        // Actualizar Rotation Vector
        Vector3 rotationVector = axis * (angle * Mathf.Deg2Rad);
        rotationVectorX.text = rotationVector.x.ToString("F3");
        rotationVectorY.text = rotationVector.y.ToString("F3");
        rotationVectorZ.text = rotationVector.z.ToString("F3");

        // Actualizar Rotation Matrix
        Matrix4x4 matrix = Matrix4x4.Rotate(cube.rotation);
        rotationMatrixText.text =
            $"{matrix.m00:F3} {matrix.m01:F3} {matrix.m02:F3}\n" +
            $"{matrix.m10:F3} {matrix.m11:F3} {matrix.m12:F3}\n" +
            $"{matrix.m20:F3} {matrix.m21:F3} {matrix.m22:F3}";
    }
}
