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
        panel.SetActive(true);
        panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
