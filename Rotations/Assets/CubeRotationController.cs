using UnityEngine;
using UnityEngine.UI;

public class CubeRotationController : MonoBehaviour
{
    public Transform cube; // Referencia al cubo

    // Panel de Quaternions
    public InputField quaternionW;
    public InputField quaternionX;
    public InputField quaternionY;
    public InputField quaternionZ;

    // Panel de Euler
    public InputField eulerYaw;
    public InputField eulerPitch;
    public InputField eulerRoll;

    // Panel de Axis-Angle
    public InputField axisX;
    public InputField axisY;
    public InputField axisZ;
    public InputField axisAngle;

    // Panel de Rotation Vector
    public InputField rotationVectorX;
    public InputField rotationVectorY;
    public InputField rotationVectorZ;

    // Panel de Matriz de Rotación
    public Text rotationMatrixText;

    // Botones
    public Button quaternionButton;
    public Button eulerButton;
    public Button axisAngleButton;
    public Button rotationVectorButton;
    public Button resetButton;

    private Quaternion initialRotation; // Rotación inicial del cubo

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
        if (float.TryParse(quaternionW.text, out float w) &&
            float.TryParse(quaternionX.text, out float x) &&
            float.TryParse(quaternionY.text, out float y) &&
            float.TryParse(quaternionZ.text, out float z))
        {
            cube.rotation = new Quaternion(x, y, z, w);
        }
        else
        {
            Debug.LogError("Error parsing quaternion inputs");
        }
    }

    void UpdateEuler()
    {
        if (float.TryParse(eulerYaw.text, out float yaw) &&
            float.TryParse(eulerPitch.text, out float pitch) &&
            float.TryParse(eulerRoll.text, out float roll))
        {
            cube.rotation = Quaternion.Euler(pitch, yaw, roll);
        }
        else
        {
            Debug.LogError("Error parsing euler inputs");
        }
    }

    void UpdateAxisAngle()
    {
        if (float.TryParse(axisX.text, out float x) &&
            float.TryParse(axisY.text, out float y) &&
            float.TryParse(axisZ.text, out float z) &&
            float.TryParse(axisAngle.text, out float angle))
        {
            Vector3 axis = new Vector3(x, y, z).normalized;
            cube.rotation = Quaternion.AngleAxis(angle, axis);
        }
        else
        {
            Debug.LogError("Error parsing axis-angle inputs");
        }
    }

    void UpdateRotationVector()
    {
        if (float.TryParse(rotationVectorX.text, out float x) &&
            float.TryParse(rotationVectorY.text, out float y) &&
            float.TryParse(rotationVectorZ.text, out float z))
        {
            Vector3 rotationVector = new Vector3(x, y, z);
            float angle = rotationVector.magnitude * Mathf.Rad2Deg;
            Vector3 axis = rotationVector.normalized;
            cube.rotation = Quaternion.AngleAxis(angle, axis);
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
        eulerYaw.text = euler.y.ToString("F3");
        eulerPitch.text = euler.x.ToString("F3");
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
