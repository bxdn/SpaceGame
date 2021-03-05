using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CameraController.Locked)
            Activate();
        else
            Deactivate();
    }

    private static void Activate()
    {
        var current = (IColonizable)Selection.CurrentSelection;
        GoodsDialogController.Reset(current.ColonizableManager.CurrentColony);
        ColonyDialogController.Reset(current.ColonizableManager.CurrentColony);
        Constants.COLONY_PANEL.SetActive(true);
        Constants.GOODS_PANEL.SetActive(true);
        Constants.COLONY_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 0);
        Constants.GOODS_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, 0);
        CameraController.Locked = true;
    }

    private static void Deactivate()
    {
        Constants.COLONY_PANEL.SetActive(false);
        Constants.GOODS_PANEL.SetActive(false);
        CameraController.Locked = false;
    }
}
