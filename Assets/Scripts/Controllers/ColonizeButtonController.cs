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
        var manager = (Selection.CurrentSelection as IColonizable).ColonizableManager;
        manager.Colonize(WorldGeneration.Galaxy);
        ColonyDialogController.Reset(manager.Colony);
        GoodsDialogController.Reset(manager.Colony);
        gameObject.SetActive(false);
        Constants.MENUS_BUTTON.SetActive(true);
    }
}
