using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using Assets.Scripts.Registry;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonizeButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CameraController.Locked)
            return;
        Constants.COLONIZE_PROMPT.SetActive(true);
        var hq = RegistryUtil.Structures.GetStructure(RegistryUtil.HQ);
        AddStructureController.Activate(hq, (Selection.CurrentSelection as IColonizable).ColonizableManager);
    }
}
