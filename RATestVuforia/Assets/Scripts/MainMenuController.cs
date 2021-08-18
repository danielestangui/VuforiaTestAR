using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private const string itemObjectName = "Item";
    private bool showInfo = false;

    [Header("Catalog")]
    public Transform[] _catalog;

    [Header("Button variables")]
    public UnityEngine.Transform _catalogHolder;
    public GameObject _itemButtonPrefab;

    [Header("Interface componets")]
    public Image ItemPreviewPanel;
    public Image ItemPreviewInfo;
    public Text ItemPreviewTittle;
    public Image ItemPreviewImage;
    public Button ARButton;
    public Button ThreeDButton;
    public Button MoreInfoButton;

    public GameObject itemObject;         


    private void Start()
    {
        ResizeInterface();
        ItemPreviewInfo.gameObject.SetActive(showInfo);


        if (_catalog.Length > 0) 
        {
            itemObject = _catalog[0].gameObject;
            UpdateItemPreviwPanel(_catalog[0]);
        }
 
        for (int i = 0; i < _catalog.Length; i++)
        {
            GameObject itemToAdd = Instantiate(_itemButtonPrefab);

            ItemButtonMM itemButton = itemToAdd.GetComponent<ItemButtonMM>();
            itemButton.item = _catalog[i];
            itemButton.GenerateItemButtonMM(_catalog[i]);
            itemToAdd.transform.parent = _catalogHolder;
        }
    }

    public void UpdateItemPreviwPanel(Transform newItemObject) 
    {

        // UI
        Item newItem = newItemObject.GetComponent<Item>();

        ARButton.gameObject.SetActive(newItem.ARPreview);
        ThreeDButton.gameObject.SetActive(newItem.ThreeDView);
        MoreInfoButton.enabled = newItem.MoreInfoView;

        ItemPreviewTittle.text = newItem.itemName;
        ItemPreviewImage.sprite = newItem.itemImage;

        showInfo = false;
        ItemPreviewInfo.gameObject.SetActive(showInfo);

        // ItemObject
        itemObject = newItemObject.gameObject;

    }

    public void ChangeScene(string newScene) 
    {
        ItemObjectController.itemObject = itemObject;
        SceneManager.LoadScene(newScene);
    }

    public void ShowInfoAction() 
    {
        showInfo = !showInfo;

        if (showInfo)
        {
            ItemPreviewInfo.gameObject.SetActive(true);
        }
        else 
        {
            ItemPreviewInfo.gameObject.SetActive(false);
        }
    }

    private void ResizeInterface() 
    {
        LayoutElement layoutIPP = ItemPreviewPanel.GetComponent<LayoutElement>();
        layoutIPP.preferredHeight = Screen.height * 0.60f;

    }

    
}
