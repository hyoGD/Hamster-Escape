using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public Animator anim_Cat, anim_Shiba;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        anim_Shiba.SetBool("Check", false);
    }

    public void ShowShiba()
    {
        anim_Shiba.SetBool("Check", true);
    }
}
