using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetController : MonoBehaviour, IPointerClickHandler
{
    float lastClick = -1;
    public void OnPointerClick(PointerEventData eventData)
    {
        float newClick = Time.time;
        if (!CameraController.Locked && newClick - lastClick < .5)
        {
            Save.ClearSave();
            WorldGeneration.StartWorld();
        }
        else
            lastClick = newClick;
    }
}
