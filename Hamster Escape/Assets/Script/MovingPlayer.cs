using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum Colorr
{
    red,
    blue,
}

public class MovingPlayer : MonoBehaviour
{
    public static MovingPlayer instance;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public bool isMoving, pause, doubt, die, win;
    [SerializeField] private Transform check;
    [SerializeField] public float speed;
    [SerializeField] public int waypoinIndex;


    private void Awake()
    {
        instance = this;

      
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //int ran = Random.Range(0, 2);
        //Cat.instance.PlayAnimCat(ran);

    }
    private void Update()
    {
        Application.targetFrameRate = 60;
    }
    private void FixedUpdate()
    {
        Moving(GameController.instance, Hamster.instance, Cat.instance);

        if (die)
        {
            Invoke("Die", 0.1f);
        }
        //if (win)
        //{
        //    float time = Time.deltaTime;
        //    if (Time.timeScale > 0.2f)
        //    {
        //        Time.timeScale = Time.timeScale - time;
        //    }
        //}
       
    }

    public void Moving(GameController gamecontroller, Hamster hamster, Cat cat)
    {
        Vector3 target = gamecontroller.targets[waypoinIndex + 1].transform.position;
        float distToPlayer = Vector2.Distance(transform.position, target);

        #region xử lý khi đi đến 1 vật thể check điều kiện với ký tự bấm trước đó
        if (isMoving && !pause && !die)
        {
            if (waypoinIndex <= gamecontroller.targets.Count - 1)
            {
                if (transform.position == gamecontroller.targets[0].transform.position)
                {
                    cat.PlayAnimCat(3);
                    //  Debug.Log("Start");
                }
                hamster.PlayAnimHamster(1);
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position == target && waypoinIndex < gamecontroller.data.question[gamecontroller.index].soluongchuoi.Length)
                {
                    doubt = false;
                    CheckGamePlay(gamecontroller, hamster, cat);
                    //   waypoinIndex++;
                }
                if (transform.position == gamecontroller.targets[gamecontroller.targets.Count - 2].transform.position)
                {
                    win = true;
                    pause = true;
                    int ran = Random.Range(3, 6);
                   // hamster.PlayAnimHamster(ran);
                    // transform.DOMove(transform.position + Vector3.one * 3f, 2f).SetEase(Ease.OutQuad);
                    // Debug.Log("Finish");
                    //Invoke("Win", 1.7f);
                    Win();
                }
            }
            else
            {
                return;

            }
        }

        #endregion
    }

    void CheckGamePlay(GameController gamecontroller, Hamster hamster, Cat cat)
    {
        if (gamecontroller.addItem[waypoinIndex].colorr == Colorr.blue)
        {
            if (checkList())
            {                        
                cat.PlayAnimCat(4);         
                hamster.PlayAnimHamster(0);             
                pause = true;              
                StartCoroutine(Action(1f, 3f, 2f));
                // Debug.Log("dung dieu kien 1");
            }
            else
            {
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 0.5f);
                Invoke("CheckDie", 2f);
                // anim.ShowShiba();
                Invoke("ShibaHide", 2f);
                waypoinIndex += 1;
                // Debug.Log("sai dieu kien 1");
            }
        }

        else if (gamecontroller.addItem[waypoinIndex].colorr == Colorr.red)
        {
            if (checkList())
            {
                doubt = true;     
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 0.5f);            
                waypoinIndex += 1;
                //   Debug.Log("dung dieu kien 2");
                StartCoroutine(InitTrue(2f,1f));
            }
            else
            {
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 0.5f);
                hamster.PlayAnimHamster(0);
                Invoke("CheckDie", 2f);
                Invoke("ShibaHide", 2f);
                pause = true;
               
              //  Debug.Log("sai dieu kien 2");
            }
        }
    }

    public bool checkList()
    {
        foreach(bool i in  GameController.instance.check_Chain)
        {
            if(i == false)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator Action(float doubt, float detected, float see)
    {
        yield return new WaitForSeconds(doubt);       
        IsDoubt();
       
        yield return new WaitForSeconds(detected);
        ShibaHide();

        GameController.instance.InitListChair();
      
        yield return new WaitForSeconds(see);
        See();
         
    }

    IEnumerator InitTrue(float detected,float run)
    {
        yield return new WaitForSeconds(detected);
        ShibaHide();
        yield return new WaitForSeconds(run);
        Cat.instance.PlayAnimCat(3);
        GameController.instance.InitListChair();
        yield return new WaitForSeconds(run);
        doubt = false;
    }

    public void See()
    {
        Cat.instance.PlayAnimCat(3);
        if (pause && !die)
        {
            waypoinIndex += 1;
            pause = false;
        }
    }

    public void IsDoubt()
    {
        Cat.instance.PlayAnimCat(2);
    }

    public void ShibaHide()
    {
        if (!die)
        {
            Cat.instance.PlayAnimCat(5);
           
        }
        else
        {
            Cat.instance.PlayAnimCat(6);
           
        }
    }

    public void CheckDie()
    {
        die = true;
    }

    void Die()
    {
        pause = true;
        // Cat.instance.PlayAnimCat(4);
        Hamster.instance.PlayAnimHamster(2);
        GameController.instance.Lose.SetActive(true);
    }
    void Win()
    {
        int ran = Random.Range(3, 6);
        Hamster.instance.hamster.SetAnimation(Hamster.instance.animAction[ran], false, () =>
        {
            Hamster.instance.transform.position = GameController.instance.targets[GameController.instance.targets.Count - 1].position;
            Hamster.instance.hamster.SetAnimation(Hamster.instance.animAction[6], false, () =>
              {
                  Hamster.instance.hamster.SetAnimation(Hamster.instance.animAction[7], true);
              });
        });
       

        //Hamster.instance.hamster.SetAnimation(Hamster.instance.animAction[6], false, () =>
        // {
        //     Hamster.instance.hamster.SetAnimation(Hamster.instance.animAction[7], true);
        // });
    }
}

