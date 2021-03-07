using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayController : MonoBehaviour, IPointerClickHandler
{
    private static Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = "Paused";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CameraController.Locked)
        {
            ColonyUpdater.TogglePause();
        }
    }
    public static void ToggleText()
    {
        if (text.text.Equals("Playing".ToString()))
            text.text = "Paused".ToString();
        else
            text.text = "Playing".ToString();
    }
}
