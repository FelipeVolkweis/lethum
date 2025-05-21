using UnityEngine;

public class OrientationTracker : MonoBehaviour
{
    public Transform orientation;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 viewDir = cameraTransform.forward;
        viewDir.y = 0f;
        viewDir.Normalize();

        orientation.forward = viewDir;
    }
}