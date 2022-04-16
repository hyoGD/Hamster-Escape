using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    public static MovingPlayer instance;
    public Anim anim;
    [SerializeField] public bool isMoving, pause, die, caught;
    [SerializeField] private Transform check;
    [SerializeField] private LayerMask checkLayer;
    [SerializeField] public float speed;
    [SerializeField]  public int waypoinIndex;
  


    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
      
        // die = Physics2D.OverlapCircle(check.position, 1f, checkLayer); 
        Moving();

       

      
        if (die)
        {
            Invoke("Die", 0.1f);
        }

    }

    public void Moving()
    {
        #region xử lý khi đi đến 1 vật thể check điều kiện với ký tự bấm trước đó
        if (isMoving && !pause && !die)
        {
           
            if (waypoinIndex <= GameController.instance.targets.Count - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameController.instance.targets[waypoinIndex + 1].transform.position, speed * Time.deltaTime);
                anim.CatMoving();
              //  ShibaHide();
                if (transform.position == GameController.instance.targets[waypoinIndex + 1].transform.position && waypoinIndex<= GameController.instance.check_Chain.Count-1)
                {

                     
                    if (GameController.instance.addOpaque[waypoinIndex])
                    {
                       
                        if (GameController.instance.check_Chain[waypoinIndex])
                        {
                            anim.ShowShiba();
                            Invoke("ShibaHide", 3f);
                            pause = true;
                            anim.CatIdle();
                            Invoke("See", 5f);
                           
                            Debug.Log("dung dieu kien 1");
                        }
                        else
                        {

                            anim.ShowShiba();
                            Invoke("ShibaHide", 3f);
                            waypoinIndex += 1;
                            Debug.Log("sai dieu kien 1");
                           
                        }
                    }
                    else
                    {
                       
                        if (GameController.instance.check_Chain[waypoinIndex])
                        {

                            waypoinIndex += 1;
                            Debug.Log("dung dieu kien 2");
                           
                        }
                        else
                        {                        
                            anim.CatIdle();
                            //StartCoroutine(See());
                            anim.ShowShiba();
                           Invoke("CheckDie", 2);
                            Invoke("ShibaHide", 3f);
                            pause = true;
                            Invoke("See", 5f);
                           
                            Debug.Log("sai dieu kien 2");
                            
                        }
                    }
                }
                
                if (transform.position == GameController.instance.targets[waypoinIndex + 1].transform.position && waypoinIndex == GameController.instance.check_Chain.Count )
                {
                    anim.CatIdle();
                    ShibaHide();
                    Debug.Log("Finish");                 
                }
                else
                {
                    die = Physics2D.OverlapCircle(check.position, 0.1f, checkLayer);
                   
                }
            }
            else
            {
                return;
            }
           
        }
        #endregion
    }


    public void See()
    {
      //  yield return new WaitForSeconds(5);
        if (pause)
        {
            waypoinIndex += 1;
            pause = false;
        }
    }

    public void ShibaHide()
    {     
            anim.HIdeShiba(); 
    }
    public void CheckDie()
    {

        die = true;
      
    }
    void Die()
    {
        GameController.instance.Lose.SetActive(true);
        pause = true;
       // isMoving = false;
        anim.CatIdle();
    }

}

