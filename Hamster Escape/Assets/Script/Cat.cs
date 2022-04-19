using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Cat : MonoBehaviour
{
    public static Cat instance;
    [Header("SPINE")]
   
    //  public SkeletonAnimation hamsterSpine;
    public SkeletonAnimation cat;
    [SpineAnimation] public List<string> catAnimation;
   
    [SerializeField] int n;
    private void Awake()
    {
        {
            instance = this;
        }
    }

    public void PlayAnimCat(int num)
    {

        switch (num)
        {
            case 0:
                cat.AnimationState.SetAnimation(0, catAnimation[num], true);
                break;
            case 1:
                cat.AnimationState.SetAnimation(0, catAnimation[num], true);
                break;
            default:
                cat.AnimationState.SetAnimation(0, catAnimation[num], false);
                break;
        }
    }

    }
