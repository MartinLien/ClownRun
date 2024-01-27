using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = GetComponent<ChangeImageOnHover>().hover;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = GetComponent<ChangeImageOnHover>()._default;
    }
}
