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
            SaveUtility.ClearSave();
            Constants.COLONIZE_BUTTON.SetActive(false);
            Constants.MENUS_BUTTON.SetActive(false);
            Constants.TOP_INFO.SetActive(true);
            WorldGeneration.StartWorld();
        }
        else
            lastClick = newClick;
    }
}
