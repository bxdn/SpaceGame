using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static Vector3 SCALE_DEFAULT = new Vector3(175, 100, 0);
    private static Vector3 scale = SCALE_DEFAULT;
    private bool isPanning = false;
    Vector3 mouseOrigin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        Camera camera = GetComponent<Camera>();
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta > 0)
        {
            camera.orthographicSize /= 1.2f;
            scale /= 1.2f;
        }
        else if (scrollDelta < 0)
        {
            scale *= 1.2f;
            camera.orthographicSize *= 1.2f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isPanning = false;
        }
        if (isPanning)
        {
            Vector3 pos = camera.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);
            mouseOrigin = Input.mousePosition;
            Vector3 move = new Vector3(pos.x * scale.x, pos.y * scale.y, 0);

            camera.transform.Translate(move, Space.World);
        }
    }

    public static void Reset()
    {
        Reset(Vector2.zero);
    }

    public static void Reset(Vector2 location)
    {
        scale = SCALE_DEFAULT;
        Constants.CAMERA.GetComponent<Camera>().orthographicSize = 50;
        Constants.CAMERA.transform.localPosition = new Vector3(location.x, location.y, -1);
    }
}
