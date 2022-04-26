using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        int ran = Random.Range(0, 2);
        Cat.instance.PlayAnimCat(ran);

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
        if (win)
        {
            float time = Time.deltaTime;
            if (Time.timeScale > 0.2f)
            {
                Time.timeScale = Time.timeScale - time;
            }
        }
        if (doubt)
        {

        }
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
                    Debug.Log("Start");
                }
                hamster.PlayAnimHamster(1);
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position == target && waypoinIndex < gamecontroller.data.question[gamecontroller.index].soluongchuoi.Length)
                {

                      CheckGamePlay(gamecontroller, hamster, cat);
                 //   waypoinIndex++;
                }
                if (transform.position == gamecontroller.targets[gamecontroller.targets.Count-1].transform.position)
                {
                    win = true;
                   
                    transform.DOMove(transform.position + Vector3.right * 2f, 2f).SetEase(Ease.OutQuad);
                    hamster.PlayAnimHamster(3);
                  
                    pause = true;                  
                    Debug.Log("Finish");
                }
                else
                {
                  
                   // die = Physics2D.OverlapCircle(check.position, 0.1f, checkLayerMask);

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
        //if (gamecontroller.addOpaque[waypoinIndex])
        //{

            if (checkList())
            {
                // cat.PlayAnimCat(4);
                cat.PlayAnimCat(4);
                //  Invoke("IsDoubt", 1f);
                hamster.PlayAnimHamster(0);
                //   Invoke("ShibaHide", 3f);
                pause = true;
                //   Invoke("See", 5f);
                StartCoroutine(Action(1f, 3f, 2f));
                Debug.Log("dung dieu kien 1");


            }
            else
            {
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 0.5f);
                Invoke("CheckDie", 2f);
                // anim.ShowShiba();
                Invoke("ShibaHide", 2f);
                waypoinIndex += 1;
                Debug.Log("sai dieu kien 1");

            }
       // }

        //else
        //{
        //    if (checkList())
        //    {
        //        // cat.PlayAnimCat(4);
        //        waypoinIndex += 1;
        //        Debug.Log("dung dieu kien 2");

        //    }
        //    else
        //    {
        //        cat.PlayAnimCat(4);
        //      //  Invoke("IsDoubt", 0.5f);
        //        hamster.PlayAnimHamster(0);
        //       // Invoke("CheckDie", 2f);
        //      //  Invoke("ShibaHide", 2f);
        //        pause = true;
        //        StartCoroutine(Action(0.5f, 2f, 2f));
        //        Debug.Log("sai dieu kien 2");

        //    }
       // }
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
        //  cat.PlayAnimCat(4);
        IsDoubt();
        //  hamster.PlayAnimHamster(0);
        yield return new WaitForSeconds(detected);
        ShibaHide();
        GameController.instance.InitListChair();
      //  Debug.Log("Init chain");
        yield return new WaitForSeconds(see);
        See();
         
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
            CheckDie();
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

}

