using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{  
    public GameController gamecontroller;
    public Anim anim;
    public bool isMoving, pause;
    public float speed;
    public int waypoinIndex;

    private void Start()
    {
        if (gamecontroller == null)
        {
            gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        }

    }
    private void Update()
    {
        Moving();
    }

    public void Moving()
    {
        if (isMoving && !pause)
        {      
            if (waypoinIndex <= gamecontroller.targets.Count - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, gamecontroller.targets[waypoinIndex+1].transform.position, speed * Time.deltaTime);
                anim.CatMoving();
                           
                if (transform.position == gamecontroller.targets[waypoinIndex + 1].transform.position)
                {                   
                    if (gamecontroller.check_Chain[waypoinIndex])
                    {
                        pause = true;
                        anim.CatIdle();
                        StartCoroutine(See());

                        Debug.Log("dung dieu kien");
                    }
                    else
                    {
                        waypoinIndex += 1;
                        Debug.Log("sai dieu kien");
                      
                    }
                }
            
            }
        }
    }
    

  public IEnumerator  See()
    {
        yield return new WaitForSeconds(5);
        if (pause)
        {
            waypoinIndex += 1;
            pause = false;
        }
       
        
    }

}

