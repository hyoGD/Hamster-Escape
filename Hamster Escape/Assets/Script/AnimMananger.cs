using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public enum Animal
{
    Cat,
    Hamster,
}

public class AnimMananger : MonoBehaviour
{
    [SerializeField] SkeletonAnimation anim;
    [SerializeField] SkeletonGraphic animGUI,Cat;
    [SerializeField] Animal animal;
    // [SpineSkin] [SerializeField] public string[] skins;
    [SpineAnimation] [SerializeField] public string[] animAction = new string[5];
    [SerializeField]  int n;
    public static AnimMananger instance;
    private void Awake()
    {
        instance = this;
    }

    public static void PlayAninmationHamster(SkeletonAnimation skAnim, string _strAnim, bool loop)
    {
        if (!skAnim.AnimationName.Equals(_strAnim))
        {

            skAnim.AnimationState.SetAnimation(0, _strAnim, loop);
        }
    }

    public static void PlayAninmationCat(SkeletonAnimation skAnim, string _strAnim, bool loop)
    {
        if (!skAnim.AnimationName.Equals(_strAnim) )
        {

            skAnim.AnimationState.SetAnimation(0, _strAnim, loop);
        }
    }

    public static void ChangeSkin(SkeletonAnimation skAnim, string ssSkinChange)
    {
       
        skAnim.Skeleton.SetSkin(ssSkinChange);
    }

    public void PlayAnim(int num, Animal s_animal)
    {
        if (s_animal == Animal.Hamster)
        {
            switch (num)
            {
                case 3:
                    PlayAninmationHamster(anim, animAction[num], false);
                    break;
                //case 4:
                //    PlayAninmation(anim, animAction[num], false);
                //    break;
                default:
                    PlayAninmationHamster(anim, animAction[num], true);
                    break;
            }
        }
    }

   
}
