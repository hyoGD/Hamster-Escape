using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Anim : MonoBehaviour
{
    public static Anim instance;
    public Animator anim_Cat, anim_Shiba;

    private void Awake()
    {
        instance = this;
    }
  
    public void CatMoving()
    {
        anim_Cat.SetBool("Moving", true);
    }
    public void CatIdle()
    {
        anim_Cat.SetBool("Moving", false);
      
    }

    public void HIdeShiba()
    {
        anim_Shiba.SetBool("Hide", true);
        anim_Shiba.SetBool("Check",false);
        Debug.Log("Shiba Hide");
    }

    public void ShowShiba()
    {
        anim_Shiba.SetBool("Check", true);
        Debug.Log("Show Shiba");
    }
}
