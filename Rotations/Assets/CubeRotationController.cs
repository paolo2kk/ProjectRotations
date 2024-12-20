using UnityEngine;
using UnityEngine.UI;

public class CubeRotationController : MonoBehaviour
{
    public Transform cube; // Reference to the cube
    public InputField quaternionInput;
    public InputField eulerInput;
    public InputField axisAngleInput;
    public InputField rotationVectorInput;
    public Text rotationMatrixText;
    public Button quaternionButton;
    public Button eulerButton;
    public Button axisAngleButton;
    public Button rotationVectorButton;
    public Button resetButton;

    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation for resetting
        initialRotation = cube.rotation;

        // Attach event listeners to buttons
        quaternionButton.onClick.AddListener(UpdateFromQuaternion);
        eulerButton.onClick.AddListener(UpdateFromEuler);
        axisAngleButton.onClick.AddListener(UpdateFromAxisAngle);
        rotationVectorButton.onClick.AddListener(UpdateFromRotationVector);
        resetButton.onClick.AddListener(ResetCube);
    }

    void Update()
    {
        // Continuously update all representations
        UpdatePanels();
    }

    void UpdatePanels()
    {
        // Quaternion
        Quaternion currentRotation = cube.rotation;
        quaternionInput.text = $"{currentRotation.x:F2}, {currentRotation.y:F2}, {currentRotation.z:F2}, {currentRotation.w:F2}";

        // Euler Angles
        Vector3 eulerAngles = cube.eulerAngles;
        eulerInput.text = $"{eulerAngles.x:F2}, {eulerAngles.y:F2}, {eulerAngles.z:F2}";

        // Axis-Angle
        Vector3 axis;
        float angle;
        currentRotation.ToAngleAxis(out angle, out axis);
        axisAngleInput.text = $"{axis.x:F2}, {axis.y:F2}, {axis.z:F2}, {angle:F2}";

        // Rotation Vector
        Vector3 rotationVector = axis * angle * Mathf.Deg2Rad;
        rotationVectorInput.text = $"{rotationVector.x:F2}, {rotationVector.y:F2}, {rotationVector.z:F2}";

        // Rotation Matrix
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(currentRotation);
        rotationMatrixText.text = MatrixToString(rotationMatrix);
    }

    public void UpdateFromQuaternion()
    {
        if (TryParseQuaternion(quaternionInput.text, out Quaternion newRotation))
        {
            cube.rotation = newRotation;
        }
    }

    public void UpdateFromEuler()
    {
        if (TryParseVector3(eulerInput.text, out Vector3 newEuler))
        {
            cube.rotation = Quaternion.Euler(newEuler);
        }
    }

    public void UpdateFromAxisAngle()
    {
        if (TryParseAxisAngle(axisAngleInput.text, out Vector3 axis, out float angle))
        {
            cube.rotation = Quaternion.AngleAxis(angle, axis);
        }
    }

    public void UpdateFromRotationVector()
    {
        if (TryParseVector3(rotationVectorInput.text, out Vector3 rotationVector))
        {
            float angle = rotationVector.magnitude * Mathf.Rad2Deg;
            Vector3 axis = rotationVector.normalized;
            cube.rotation = Quaternion.AngleAxis(angle, axis);
        }
    }

    public void ResetCube()
    {
        cube.rotation = initialRotation;
        UpdatePanels();
    }

    private bool TryParseQuaternion(string input, out Quaternion quaternion)
    {
        string[] values = input.Split(',');
        quaternion = Quaternion.identity;
        if (values.Length != 4) return false;

        return float.TryParse(values[0], out quaternion.x) &&
               float.TryParse(values[1], out quaternion.y) &&
               float.TryParse(values[2], out quaternion.z) &&
               float.TryParse(values[3], out quaternion.w);
    }

    private bool TryParseVector3(string input, out Vector3 vector)
    {
        string[] values = input.Split(',');
        vector = Vector3.zero;
        if (values.Length != 3) return false;

        return float.TryParse(values[0], out vector.x) &&
               float.TryParse(values[1], out vector.y) &&
               float.TryParse(values[2], out vector.z);
    }

    private bool TryParseAxisAngle(string input, out Vector3 axis, out float angle)
    {
        string[] values = input.Split(',');
        axis = Vector3.zero;
        angle = 0f;
        if (values.Length != 4) return false;

        return float.TryParse(values[0], out axis.x) &&
               float.TryParse(values[1], out axis.y) &&
               float.TryParse(values[2], out axis.z) &&
               float.TryParse(values[3], out angle);
    }

    private string MatrixToString(Matrix4x4 matrix)
    {
        return $"{matrix.m00:F2}, {matrix.m01:F2}, {matrix.m02:F2}\n" +
               $"{matrix.m10:F2}, {matrix.m11:F2}, {matrix.m12:F2}\n" +
               $"{matrix.m20:F2}, {matrix.m21:F2}, {matrix.m22:F2}";
    }
}
