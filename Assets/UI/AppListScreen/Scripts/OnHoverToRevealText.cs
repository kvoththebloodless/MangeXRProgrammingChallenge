using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnHoverToRevealText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TextPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {   //Can be lerped to appear smoothly using a coroutine but no time
        TextPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextPanel.SetActive(false);
    }




}
