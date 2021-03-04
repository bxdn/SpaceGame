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
            RevertView(selected);
        else
            RenderColony(selected);
        Constants.TOP_INFO.SetActive(!Constants.TOP_INFO.activeSelf);
    }

    private static void RevertView(IModelObject selected)
    {
        if (selected is IParent p)
            p.RenderSystem();
        else
            (selected as IChild).Parent.RenderSystem();
        Constants.MENUS_BUTTON.SetActive(false);
        Constants.COLONIZE_BUTTON.SetActive(false);
    }
    private static void RenderColony(IModelObject selected)
    {
        var selectedIsUnOwned = selected is IColonizable c && c.ColonizableManager is IColonizableManager m &&
                m.Owner != WorldGeneration.Galaxy.Player.Domain;
        if (selectedIsUnOwned)
            Constants.COLONIZE_BUTTON.SetActive(true);
        else
            Constants.MENUS_BUTTON.SetActive(true);
        WorldMapGUI.Render(selected as IColonizable);
    }
}
