using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CORE_ITEMS;
using Ricimi;

public class Item_Element : MonoBehaviour
{
    public Image sprite;
    public TextMeshProUGUI name;
    public TextMeshProUGUI txt_quantity;
    public int quality;
    public int actual_quantity;

    public string ID;
    public int quantity;

    public void SetInfo (Sprite sprite, string name, string quantity)
    {
        this.sprite.sprite = sprite;
        this.name.text = name;
        this.txt_quantity.text = quantity;
        actual_quantity = int.Parse(quantity);
    }

    public void SetInternalInfo (string ID, int quantity, int quality)
    {
        this.ID = ID;
        this.quantity = quantity;
        this.quality = quality;
    }

    private void Start()
    {
        TryGetComponent(out CleanButton but);
        but.onClick.AddListener(CallBack);
    }

    private void CallBack ()
    {
        if(actual_quantity > 0)
        {
            ReagentsToFrames reagent = new ReagentsToFrames(ID, sprite.sprite, ref actual_quantity);
            reagent.Craft();
        }
            
        UpdateItem();
    }

    public void UpdateItem()
    {
        txt_quantity.text = actual_quantity.ToString();
    }

    public ref int GetQuantity ()
    {
        return ref actual_quantity;
    }

}
