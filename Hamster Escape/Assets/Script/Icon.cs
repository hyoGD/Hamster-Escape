using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//set up chain theo du lieu data.cs
public class Icon : MonoBehaviour
{
    public type t_icon;
    private Image geometry;
    public Data data;
    public bool opaque, normal = true, isTrue;
       
    // Start is called before the first frame update
    void Start()
    {
        geometry = GetComponent<Image>();
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
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_Normal[0];

                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_Normal[1];

                    break;
                case type.vuong:
                    geometry.sprite = data.chain_Normal[2];

                    break;
            }
        }
        else
        {
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_Normal[3];
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_Normal[4];
                    break;
                case type.vuong:
                    geometry.sprite = data.chain_Normal[5];
                    break;

            }

        }
    }
    public void setIconTrue(type t)
    {
        if (opaque)
        {
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_True[0];
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_True[1];
                    break;
                case type.vuong:
                    geometry.sprite = data.chain_True[2];
                    break;
            }
        }
        else
        {
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_True[0];
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_True[1];
                    break;
                case type.vuong:
                    geometry.sprite = data.chain_True[2];
                    break;
            }
        }
    }
    public void setIconFaild(type t)
    {
        if (opaque)
        {
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_Faild[0];
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_Faild[1];
                    break;
                case type.vuong:
                    geometry.sprite = data.chain_Faild[2];
                    break;
            }
        }
        else
        {
            switch (t)
            {
                case type.tron:
                    geometry.sprite = data.chain_Faild[0];
                    break;
                case type.tamgiac:
                    geometry.sprite = data.chain_Faild[1];
                    break;
                case type.vuong:
                    geometry.sprite = data.chain_Faild[2];
                    break;
            }
        }
    }
}
