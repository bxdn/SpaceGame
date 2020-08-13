using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColonyDialogLauncher : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool activate = Selection.CurrentSelection is OrbiterGUIController orbiter && orbiter.Orbiter is IColonizable col && col.ColonizableManager.Owner == Player.Domain;
        activate |= Selection.CurrentSelection is OrbitParentGUIController parent && parent.System is IColonizable planet && planet.ColonizableManager.Owner == Player.Domain;
        if (activate)
        {
            panel.SetActive(true);
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            CameraController.Locked = true;
        }
    }
}
