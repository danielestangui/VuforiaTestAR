using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonMM : MonoBehaviour
{
    public Transform item;

    private Button button;
    private Text buttonTitle;
    private Image buttonImage;

    public void GenerateItemButtonMM(Transform item)
    {
        button = this.GetComponent<Button>();
        buttonTitle = this.GetComponentInChildren<Text>();
        buttonImage = this.GetComponent<Image>();

        button.onClick.AddListener(TaskOnClick);
        buttonTitle.text = item.GetComponent<Item>().itemName;
        buttonImage.sprite = item.GetComponent<Item>().image;
    }

    private void TaskOnClick() 
    {
        MainMenuController menuController = this.GetComponentInParent<MainMenuController>();
        menuController.UpdateItemPreviwPanel(item);
    }
}