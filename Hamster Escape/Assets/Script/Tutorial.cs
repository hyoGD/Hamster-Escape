using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool fistPlay = false;

    private void Start()
    {
        fistPlay = PlayerPrefs.GetInt("Tutorial") == 1;
        if (!fistPlay)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
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
