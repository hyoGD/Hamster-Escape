using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataGame", menuName = "Scriptable Objects/ DataGame")]
public class Data : ScriptableObject
{
    public List<Questions> question= new List<Questions>();
    public List<Item> Items = new List<Item>();
    public List<GameObject> itemDeco;
    [Header("Sprite Chuoi bieu tuong")]
    public Sprite[] chain_Normal;
    public Sprite[] ChainColor;
 
}
