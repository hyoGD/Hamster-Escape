using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class MovingPlayer : MonoBehaviour
{
    public static MovingPlayer instance;
    public Anim anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public bool isMoving, pause, doubt, die, win;
    [SerializeField] private Transform check;
    [SerializeField] private LayerMask checkLayerMask, DoubtLayerMask;
    [SerializeField] public float speed;
    [SerializeField] public int waypoinIndex;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Application.targetFrameRate = 60;
        doubt = Physics2D.OverlapCircle(check.position, 2f, DoubtLayerMask);
        if (doubt)
        {
            Cat.instance.PlayAnimCat(2);
        }
        //else
        //{
        //    Cat.instance.PlayAnimCat(1);
        //}
       
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
            if(Time.timeScale > 0.2f)
            {
                Time.timeScale = Time.timeScale - time;
            }
        }
    }

    public void Moving(GameController gamecontroller, Hamster hamster, Cat cat)
    {
        #region xử lý khi đi đến 1 vật thể check điều kiện với ký tự bấm trước đó
        if (isMoving && !pause && !die)
        {
            if (waypoinIndex <= gamecontroller.targets.Count - 1)
            {
                hamster.PlayAnimHamster(1);
              //  cat.PlayAnimCat(1);
                transform.position = Vector2.MoveTowards(transform.position, gamecontroller.targets[waypoinIndex + 1].transform.position, speed * Time.deltaTime);
                if (transform.position == gamecontroller.targets[waypoinIndex + 1].transform.position && waypoinIndex <= gamecontroller.check_Chain.Count - 1)
                {
                    CheckGamePlay(gamecontroller, hamster, cat);
                }

                if (transform.position == gamecontroller.targets[waypoinIndex + 1].transform.position && waypoinIndex == gamecontroller.check_Chain.Count)
                {
                    win = true;
                    rb.AddForce(transform.right * 50f);
                    hamster.PlayAnimHamster(4);
                    Invoke("Win", 1f);
                    pause = true;
                    //  ShibaHide();
                    Debug.Log("Finish");
                }
                else
                {
                    die = Physics2D.OverlapCircle(check.position, 0.1f, checkLayerMask);

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
        if (gamecontroller.addOpaque[waypoinIndex])
        {

            if (gamecontroller.check_Chain[waypoinIndex])
            {
                cat.PlayAnimCat(2);
                hamster.PlayAnimHamster(0);
                Invoke("ShibaHide", 3f);

                pause = true;

                Invoke("See", 5f);

                Debug.Log("dung dieu kien 1");
            }
            else
            {
                cat.PlayAnimCat(2);
                Invoke("CheckDie", 2f);
                // anim.ShowShiba();
                Invoke("ShibaHide", 2f);
                waypoinIndex += 1;
                Debug.Log("sai dieu kien 1");

            }
        }
        else
        {

            if (gamecontroller.check_Chain[waypoinIndex])
            {

                waypoinIndex += 1;
                cat.PlayAnimCat(1);
                Debug.Log("dung dieu kien 2");

            }
            else
            {
                cat.PlayAnimCat(2);
                hamster.PlayAnimHamster(0);
                //StartCoroutine(See());
                // anim.ShowShiba();
                Invoke("CheckDie", 2f);
                Invoke("ShibaHide", 2f);
                pause = true;
                // Invoke("See", 5f);

                Debug.Log("sai dieu kien 2");

            }
        }
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
        if (!die)
        {
            Cat.instance.PlayAnimCat(3);

        }
        else
        {
            Cat.instance.PlayAnimCat(4);
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
        Hamster.instance.PlayAnimHamster(3);
        GameController.instance.Lose.SetActive(true);
      
        // isMoving = false;
       
    }
    void Win()
    {
      
        rb.velocity = Vector2.zero;
    }
}

