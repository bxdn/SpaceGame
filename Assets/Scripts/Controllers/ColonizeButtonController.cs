using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GUI;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonizeButtonController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        Constants.COLONIZE_PROMPT.SetActive(true);
        AddStructureController.Activate(EStructure.HQ, (Selection.CurrentSelection as IColonizable).ColonizableManager);
    }
}
