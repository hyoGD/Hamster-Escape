using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public bool fistPlay = false;
    public GameObject mess;


    private void Start()
    {
        fistPlay = PlayerPrefs.GetInt("Tutorial") == 1;
        if (!fistPlay)
        {
            gameObject.SetActive(true);
            mess.SetActive(true);
            mess.transform.GetChild(0).GetComponent<Text>().text = " Press the correct icon to let the Hamster hide behind the objects.Don't get caught.";
            
          // lv11: Press another icon to keep the Hamster away from the traps.Don't get caught.
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
