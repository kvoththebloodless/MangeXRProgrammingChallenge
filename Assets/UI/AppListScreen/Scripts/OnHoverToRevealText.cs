using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnHoverToRevealText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TextPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextPanel.SetActive(false);
    }




}
