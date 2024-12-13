using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"Clicked on: {EventSystem.current.currentSelectedGameObject}");
        }
    }