using System;
using UnityEngine;
using UnityEngine.UI;

public class rotationmanager : MonoBehaviour
{
    public GameObject cube; // Reference to the Cube

    // UI Elements for Quaternions
    public InputField quaternionW;
    public InputField quaternionX;
    public InputField quaternionY;
    public InputField quaternionZ;
    public Button quaternionUpdateButton;

    // UI Elements for Euler Angles
    public InputField eulerYaw;
    public InputField eulerPitch;
    public InputField eulerRoll;
    public Button eulerUpdateButton;

    // UI Elements for Axis and Angle
    public InputField axisX;
    public InputField axisY;
    public InputField axisZ;
    public InputField angle;
    public Button axisAngleUpdateButton;

    // UI Elements for Rotation Vector
    public InputField rotationVectorX;
    public InputField rotationVectorY;
    public InputField rotationVectorZ;
    public Button rotationVectorUpdateButton;

    // UI Elements for Rotation Matrix
    public Text matrixDisplay;

    public void Start()
    {
        // Add listeners for update buttons
        quaternionUpdateButton.onClick.AddListener(UpdateFromQuaternion);
        eulerUpdateButton.onClick.AddListener(UpdateFromEulerAngles);
        axisAngleUpdateButton.onClick.AddListener(UpdateFromAxisAngle);
        rotationVectorUpdateButton.onClick.AddListener(UpdateFromRotationVector);

        UpdateAllPanels();
    }

    public void UpdateAllPanels()
    {
        Quaternion q = cube.transform.rotation;
        Vector3 euler = cube.transform.eulerAngles;

        // Update Quaternion Panel
        quaternionW.text = q.w.ToString("F4");
        quaternionX.text = q.x.ToString("F4");
        quaternionY.text = q.y.ToString("F4");
        quaternionZ.text = q.z.ToString("F4");

        // Update Euler Angles Panel
        eulerYaw.text = euler.y.ToString("F2");
        eulerPitch.text = euler.x.ToString("F2");
        eulerRoll.text = euler.z.ToString("F2");

        // Update Axis and Angle Panel
        float angle;
        Vector3 axis;
        QuaternionToAxisAngle(q, out axis, out angle);
        axisX.text = axis.x.ToString("F4");
        axisY.text = axis.y.ToString("F4");
        axisZ.text = axis.z.ToString("F4");
        this.angle.text = (angle * Mathf.Rad2Deg).ToString("F2");

        // Update Rotation Vector Panel
        Vector3 rotationVector = axis * angle;
        rotationVectorX.text = rotationVector.x.ToString("F4");
        rotationVectorY.text = rotationVector.y.ToString("F4");
        rotationVectorZ.text = rotationVector.z.ToString("F4");

        // Update Rotation Matrix Panel
        matrixDisplay.text = GetRotationMatrixString(q);
    }

    public void UpdateFromQuaternion()
    {
        float w = float.Parse(quaternionW.text);
        float x = float.Parse(quaternionX.text);
        float y = float.Parse(quaternionY.text);
        float z = float.Parse(quaternionZ.text);

        cube.transform.rotation = new Quaternion(x, y, z, w);
        UpdateAllPanels();
    }

    public void UpdateFromEulerAngles()
    {
        float yaw = float.Parse(eulerYaw.text);
        float pitch = float.Parse(eulerPitch.text);
        float roll = float.Parse(eulerRoll.text);

        cube.transform.eulerAngles = new Vector3(pitch, yaw, roll);
        UpdateAllPanels();
    }

    public void UpdateFromAxisAngle()
    {
        float x = float.Parse(axisX.text);
        float y = float.Parse(axisY.text);
        float z = float.Parse(axisZ.text);
        float angle = float.Parse(this.angle.text) * Mathf.Deg2Rad;

        Vector3 axis = new Vector3(x, y, z).normalized;
        cube.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
        UpdateAllPanels();
    }

    public void UpdateFromRotationVector()
    {
        float x = float.Parse(rotationVectorX.text);
        float y = float.Parse(rotationVectorY.text);
        float z = float.Parse(rotationVectorZ.text);

        Vector3 rotationVector = new Vector3(x, y, z);
        float angle = rotationVector.magnitude;
        Vector3 axis = rotationVector.normalized;

        cube.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
        UpdateAllPanels();
    }

    public void QuaternionToAxisAngle(Quaternion q, out Vector3 axis, out float angle)
    {
        angle = 2.0f * Mathf.Acos(q.w);
        float sinTheta = Mathf.Sqrt(1.0f - q.w * q.w);

        if (sinTheta > 0.0001f)
        {
            axis = new Vector3(q.x / sinTheta, q.y / sinTheta, q.z / sinTheta);
        }
        else
        {
            axis = Vector3.right; // Arbitrary axis
        }
    }

    string GetRotationMatrixString(Quaternion q)
    {
        Matrix4x4 m = Matrix4x4.Rotate(q);
        return $"[{m.m00:F4}, {m.m01:F4}, {m.m02:F4}]\n" +
               $"[{m.m10:F4}, {m.m11:F4}, {m.m12:F4}]\n" +
               $"[{m.m20:F4}, {m.m21:F4}, {m.m22:F4}]";
    }
}
