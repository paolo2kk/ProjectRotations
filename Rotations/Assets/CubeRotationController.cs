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
    public Text Matrix00, Matrix01, Matrix02;
    public Text Matrix10, Matrix11, Matrix12;
    public Text Matrix20, Matrix21, Matrix22;

    // Botones
    public Button quaternionButton;
    public Button eulerButton;
    public Button axisAngleButton;
    public Button rotationVectorButton;
    public Button resetButton;

    private Quaternion initialRotation; 
    private Quaternion originalQuaternion;

    void Start()
    {
        initialRotation = cube.rotation;

        quaternionButton.onClick.AddListener(UpdateQuaternion);
        eulerButton.onClick.AddListener(UpdateEuler);
        axisAngleButton.onClick.AddListener(UpdateAxisAngle);
        rotationVectorButton.onClick.AddListener(UpdateRotationVector);
        resetButton.onClick.AddListener(ResetRotation);

        UpdateCanvas();
    }

    void Update()
    {
        DisplayEulerAngles();
        DisplayQuaternion();
        DisplayEulerAxisAndAngle();
        DisplayMatrix();
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
            Quaternion eulerRotation = Quaternion.Euler(yaw, pitch, roll);
            cube.rotation = eulerRotation.normalized;
        }
    }

    void UpdateAxisAngle()
    {
        if (float.TryParse(axisX2.text, out float x) &&
            float.TryParse(axisY2.text, out float y) &&
            float.TryParse(axisZ2.text, out float z) &&
            float.TryParse(axisAngle2.text, out float angle))
        {
            Vector3 axis = new Vector3(x, y, z).normalized;
            Quaternion axisAngleRotation = Quaternion.AngleAxis(angle, axis);
            cube.rotation = axisAngleRotation.normalized;
        }
    }

    void UpdateRotationVector()
    {
        if (float.TryParse(rotationVectorX2.text, out float x) &&
            float.TryParse(rotationVectorY2.text, out float y) &&
            float.TryParse(rotationVectorZ2.text, out float z))
        {
            Vector3 rotationVector = new Vector3(x, y, z);
            float angle = rotationVector.magnitude * Mathf.Rad2Deg;
            Vector3 axis = rotationVector.normalized;
            Quaternion rotationVectorRotation = Quaternion.AngleAxis(angle, axis);
            cube.rotation = rotationVectorRotation.normalized;
        }
    }

    void ResetRotation()
    {
        cube.rotation = initialRotation;
    }

    void UpdateCanvas()
    {
        DisplayQuaternion();
        DisplayEulerAngles();
        DisplayEulerAxisAndAngle();
        DisplayMatrix();
    }

    // Nuevas funciones
    void DisplayQuaternion()
    {
        quaternionW.text = cube.rotation.w.ToString("F3");
        quaternionX.text = cube.rotation.x.ToString("F3");
        quaternionY.text = cube.rotation.y.ToString("F3");
        quaternionZ.text = cube.rotation.z.ToString("F3");
    }

    void DisplayEulerAngles()
    {
        Vector3 euler = cube.rotation.eulerAngles;
        eulerYaw.text = euler.x.ToString("F3");
        eulerPitch.text = euler.y.ToString("F3");
        eulerRoll.text = euler.z.ToString("F3");
    }

    void DisplayEulerAxisAndAngle()
    {
        cube.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        axisX.text = axis.x.ToString("F3");
        axisY.text = axis.y.ToString("F3");
        axisZ.text = axis.z.ToString("F3");
        axisAngle.text = angle.ToString("F3");
    }

    void DisplayMatrix()
    {
        Matrix4x4 M = EulerAnglesToMatrix(cube.transform.rotation.eulerAngles);

        Matrix00.text = M[0, 0].ToString("F3");
        Matrix01.text = M[0, 1].ToString("F3");
        Matrix02.text = M[0, 2].ToString("F3");
        Matrix10.text = M[1, 0].ToString("F3");
        Matrix11.text = M[1, 1].ToString("F3");
        Matrix12.text = M[1, 2].ToString("F3");
        Matrix20.text = M[2, 0].ToString("F3");
        Matrix21.text = M[2, 1].ToString("F3");
        Matrix22.text = M[2, 2].ToString("F3");
    }

    public static Matrix4x4 EulerAnglesToMatrix(Vector3 angles)
    {
        float roll = angles.x * Mathf.Deg2Rad;
        float pitch = angles.y * Mathf.Deg2Rad;
        float yaw = angles.z * Mathf.Deg2Rad;

        Matrix4x4 Rx = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, Mathf.Cos(roll), -Mathf.Sin(roll), 0),
            new Vector4(0, Mathf.Sin(roll), Mathf.Cos(roll), 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 Ry = new Matrix4x4(
            new Vector4(Mathf.Cos(pitch), 0, Mathf.Sin(pitch), 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(-Mathf.Sin(pitch), 0, Mathf.Cos(pitch), 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 Rz = new Matrix4x4(
            new Vector4(Mathf.Cos(yaw), -Mathf.Sin(yaw), 0, 0),
            new Vector4(Mathf.Sin(yaw), Mathf.Cos(yaw), 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        return Rz * Ry * Rx;
    }
}
