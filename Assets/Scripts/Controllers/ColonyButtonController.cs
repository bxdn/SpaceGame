using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonyButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CameraController.Locked)
            Activate();
    }

    private static void Activate()
    {
        var selected = Selection.CurrentSelection;
        if (WorldMapGUI.Activated)
            if (selected is OrbitParentGUIController)
                (selected as OrbitParentGUIController).System.RenderSystem();
            else
                (selected as OrbiterGUIController).Orbiter.Parent.RenderSystem();
        else
        {
            WorldMapGUI.Render(selected.ModelObject as IColonizable);
        }
        Constants.TOP_INFO.SetActive(!Constants.TOP_INFO.activeSelf);
        /*Constants.COLONY_PANEL.SetActive(true);
        Constants.GOODS_PANEL.SetActive(true);
        Constants.COLONY_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 0);
        Constants.GOODS_PANEL.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, 0);
        CameraController.Locked = true;*/
    }
}
