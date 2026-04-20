using UnityEngine;

public class EllipseOrbit : MonoBehaviour
{
    public Transform target;
    public float radiusX = 3f;
    public float radiusY = 1f;
    public float speed = 2f;

    private float angle;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radiusX;
        float y = Mathf.Sin(angle) * radiusY;

        transform.position = target.position + new Vector3(x, y, 0f);
    }
}
