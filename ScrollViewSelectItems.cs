using CORE_ITEMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ScrollViewSelectItems : MonoBehaviour
{
    [SerializeField] private int                    child_count;
    [SerializeField] private int                    all_buttons_count;
    [SerializeField] private List<GameObject>       all_buttons;
    
    [Header("Item_Element")] 
    [SerializeField] private GameObject             item_element;
    
    [Header("Item Element Container")]
    [SerializeField] private GameObject             item_element_container;
    
    public TextMeshProUGUI                          txt_details_title;

    Root_Resources                                  res;
    [SerializeField]private List<GameObject>        pool;
    private int                                     pooling_quantity;
    
    void Start()
    {
        child_count = transform.GetChild(0).childCount;

        all_buttons = new List<GameObject>();

        for (int i = 0; i < child_count; i++)
        {
            if (transform.GetChild(0).GetChild(i).name != "---Separator---")
            {
                all_buttons.Add(transform.GetChild(0).GetChild(i).gameObject);
            } 
        }

        foreach(GameObject but in all_buttons)
        {
            but.TryGetComponent(out CleanButton button);
            button.onClick.AddListener(() => Onclick(but));
        }

        //Select at Start the button selected at index = 0 (Forge)
        CreatePool();
        SetSelectedFirst(0);

    }

    private void CreatePool()
    {
        res = Reader.Instance.GetRootResourcesFromJson();
        var items_count = res.forge.Count + res.blacksmith.Count;
        pool = new List<GameObject>();
        for (int i = 0; i < items_count; i++)
        {
            var item = Instantiate(item_element, item_element_container.transform);
            item.SetActive(false);
            pool.Add(item);
        }
    }

    //optimizar porque siempre esta buscando todos los botones y apagandolo aunque esten apagados
    private void Onclick(GameObject button) 
    {
        Select(button);

        var sib = button.transform.GetSiblingIndex();

        foreach(GameObject but in all_buttons)
        {
            if(but.transform.GetSiblingIndex() != sib)
            {
                Deselect(but);
            }
        }

        button.TryGetComponent(out ButtonID but_id);
        var title = but_id.GetID;
        
        switch(title)
        {
            case "Forge": 
                txt_details_title.text = title;
                SetItems(title);
                break;

            case "Blacksmith": txt_details_title.text = title;
                SetItems(title);
                break;

            case "Letherwork": txt_details_title.text = title;
                SetItems(title);
                break;
                //...
        }

    }
    private void  SetItems (string s)
    {
       
        foreach (var item in pool)
        {
            item.SetActive(false);
        }

        var r = ReagentsToFrames.Instance;

        switch(s)
        {
            case "Forge":
                {
                    //r.CleanFrames(); //limpiamos al cambiar de boton
                    for (int i = 0; i < res.forge.Count; i++)
                    {
                        pool.ElementAt(i).SetActive(true);
                        pool.ElementAt(i).TryGetComponent(out Item_Element ie);
                        Sprite icon = null;
                        switch(res.forge.ElementAt(i).name)
                        {
                            case "Copper": icon = Resources.Load<Sprite>("Icons/Forge/copper"); break;
                            case "Iron": icon = Resources.Load<Sprite>("Icons/Forge/iron"); break;
                            case "Bronze": icon = Resources.Load<Sprite>("Icons/Forge/bronze"); break;
                            case "Black Iron": icon = Resources.Load<Sprite>("Icons/Forge/black_iron"); break;
                            case "Thorium": icon = Resources.Load<Sprite>("Icons/Forge/thorium"); break;

                        }
                        ie.SetInternalInfo(res.forge.ElementAt(i).name, res.forge.ElementAt(i).quantity, 0); //test
                        ie.SetInfo(icon, res.forge.ElementAt(i).name, res.forge.ElementAt(i).quantity.ToString());
                        
                        
                    }
                }
                break;
            case "Blacksmith":
                {
                    //r.CleanFrames(); //limpiamos al cambiar de boton
                    for (int i = 0; i < res.blacksmith.Count; i++)
                    {
                        pool.ElementAt(i).SetActive(true);
                        pool.ElementAt(i).TryGetComponent(out Item_Element ie);
                        Sprite icon = null;
                        switch (res.blacksmith.ElementAt(i).name)
                        {
                            case "Copper Gear": icon = Resources.Load<Sprite>("Icons/Blacksmith/copper_gear"); break;
                            case "Iron Gear": icon = Resources.Load<Sprite>("Icons/Blacksmith/iron_gear"); break;
                            case "Arrowhead": icon = Resources.Load<Sprite>("Icons/Blacksmith/arrowhead"); break;
                            case "Lock Pick": icon = Resources.Load<Sprite>("Icons/Blacksmith/lock_pick"); break;
                            

                        }
                        ie.SetInfo(icon, res.blacksmith.ElementAt(i).name, res.blacksmith.ElementAt(i).quantity.ToString());
                        ie.SetInternalInfo(res.blacksmith.ElementAt(i).name, res.blacksmith.ElementAt(i).quantity, 0);
                    }
                }
                break;

                //...

        }
    }
    


    private void SetSelectedFirst (int index)
    {
        var first = all_buttons.ElementAt(index);
        SetItems("Forge");
        Select(first);
    }

    private void Select (GameObject button)
    {
        button.transform.GetChild(0).gameObject.SetActive(false);
        button.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void Deselect (GameObject button)
    {
        button.transform.GetChild(0).gameObject.SetActive(true);
        button.transform.GetChild(1).gameObject.SetActive(false);
    }


}
