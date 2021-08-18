using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string itemName;
    public Sprite itemIconImage;
    public Sprite itemImage;


    public bool ARPreview = false;
    public bool ThreeDView = false;
    public bool MoreInfoView = true;

    public void SetItem(Item item) 
    {
        this.itemName = item.itemName;
        this.itemIconImage = item.itemIconImage;
        this.itemImage = item.itemImage;
        this.ARPreview = item.ARPreview;
        this.ThreeDView = item.ThreeDView;
        this.MoreInfoView = item.MoreInfoView;
    }
}
