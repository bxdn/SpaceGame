using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static Vector3 SCALE_DEFAULT = new Vector3(217, 100, 0);
    private static Vector3 scale = SCALE_DEFAULT;
    public static bool Locked { get; set; }
    Vector3 mouseOrigin;
    public static Camera Camera { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveUtility.SaveGame();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        if (!Locked)
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            if (scrollDelta > 0)
            {
                Camera.orthographicSize /= 1.2f;
                scale /= 1.2f;
            }
            else if (scrollDelta < 0)
            {
                scale *= 1.2f;
                Camera.orthographicSize *= 1.2f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                mouseOrigin = Input.mousePosition;
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                var pos = Camera.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);
                mouseOrigin = Input.mousePosition;
                var move = new Vector3(pos.x * scale.x, pos.y * scale.y, 0);

                Camera.transform.Translate(move, Space.World);
            }
        }
    }

    public static void Reset()
    {
        Reset(Vector2.zero);
    }

    public static void Reset(Vector2 location)
    {
        Locked = false;
        scale = SCALE_DEFAULT;
        Constants.CAMERA.GetComponent<Camera>().orthographicSize = 50;
        Constants.CAMERA.transform.localPosition = new Vector3(location.x, location.y, -1);
    }
}
