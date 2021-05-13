using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectColonyController : MonoBehaviour, IPointerClickHandler
{
    public int Idx { get; set; }
    public void OnPointerClick(PointerEventData p)
    {
        var manager = (Selection.CurrentSelection as IColonizable).ColonizableManager;
        manager.SetCurrentColony(Idx);
        WorldMapRenderController.ShowBuildableSquares();
        ColonyDialogController.Reset(manager.CurrentColony);
        GoodsDialogController.Reset(manager.CurrentColony);
    }
}
