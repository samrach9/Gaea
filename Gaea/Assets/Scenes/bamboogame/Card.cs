using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    //public Sprite hiddenIconSprite;
    //public Sprite iconSprite;
    public Sprite hiddenIconSprite;
    public Sprite iconSprite;

    public bool isSelected;

    public CardsController controller;

    public void OnCardClick()
    {
        controller.SetSelected(this);
        Debug.Log("iwas slcicked");
    }

    public void SetIconSprite(Sprite sp)
    {
        iconSprite=sp;
        //iconImage=sp;
    }

    public void Show()
    {
        iconImage.sprite=iconSprite;
        //iconImage = iconSprite;
        isSelected=true;
    }

    public void Hide()
    {
        iconImage.sprite=hiddenIconSprite;
        isSelected=false;
    }
}
