using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Tutorial : MonoBehaviour
{
    public bool fistPlay = false;
    public GameObject mess;  
   public Animator animlv11;
    public float duration;
    string txtMesslv1 = "Press the correct icon to let the Hamster hide behind the objects.Don't get caught.";
    string txtMesslv11 = "Press another icon to keep the Hamster away from the traps.Don't get caught.";
    string txt = "";
    private void Start()
    {
         
        animlv11 = GetComponent<Animator>();
        fistPlay = PlayerPrefs.GetInt("Tutorial") == 1;
        if (!fistPlay)
        {
            if (GameController.instance.index == 0)
            {
                // gameObject.SetActive(true);
                mess.SetActive(true);
                DOTween.To(() => txt, x => txt = x, txtMesslv1, duration).OnUpdate(() => mess.transform.GetChild(0).GetComponent<Text>().text = txt);
            }
            else if (GameController.instance.index == 10)
            {
                 gameObject.SetActive(true);
              //  gameObject.GetComponent<Image>().enabled = false;
                //startPos.DOAnchorPos(endPos.anchoredPosition, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                mess.SetActive(true);
                DOTween.To(() => txt, x => txt = x, txtMesslv11, duration).OnUpdate(() => mess.transform.GetChild(0).GetComponent<Text>().text = txt);
            }
        }
        else
        {
            gameObject.SetActive(false);
            mess.gameObject.SetActive(false);
        }
       
    }
    // Start is called before the first frame update

    public void SetTutorial()
    {
        
        gameObject.SetActive(false);
        fistPlay = true;
        PlayerPrefs.SetInt("Tutorial", fistPlay ? 1 : 0);
    }
}
