using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CameraController.Locked)
        {
            Save.ClearSave();
            WorldGeneration.StartWorld();
        }
    }
}
