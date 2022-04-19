using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//set up chain theo du lieu data.cs
public class Icon : MonoBehaviour
{
    public type t_icon;
    public Image parent, geometry;
    public Data data;
    public RectTransform rect;
    public bool opaque, normal = true, isTrue;
       
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Image>();
        geometry = transform.GetChild(0).GetComponent<Image>();
        if (normal)
        {
            setIcon(t_icon);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!normal && isTrue)
        {
            setIconTrue(t_icon);
        }
        else if (!normal && !isTrue)
        {
            setIconFaild(t_icon);
        }

    }

    public void setIcon(type t)
    {
        if (opaque)
        {
            parent.sprite = data.ChainColor[0];
        }
        else
        {
            parent.sprite = data.ChainColor[1];
        }
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_Normal[0];
                rect.sizeDelta = new Vector2(68, 68);              
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_Normal[1];
                rect.sizeDelta = new Vector2(66, 59);
                break;
                case type.vuong:
                    geometry.sprite = data.chain_Normal[2];
                rect.sizeDelta = new Vector2(59, 58);
                break;
            }
        
       
    }
    public void setIconTrue(type t)
    {
        parent.sprite = data.ChainColor[2];
    }
    public void setIconFaild(type t)
    {
        parent.sprite = data.ChainColor[3];
    }
}
