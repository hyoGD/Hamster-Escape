using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlayer : MonoBehaviour
{
    public static MovingPlayer instance;
  //  public Anim anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public bool isMoving, pause, doubt, die, win;
    [SerializeField] private Transform check;
   // [SerializeField] private LayerMask checkLayerMask, DoubtLayerMask;
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
                hamster.PlayAnimHamster(1);
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position == target && waypoinIndex <= gamecontroller.check_Chain.Count - 1)
                {
                    CheckGamePlay(gamecontroller, hamster, cat);
                }
                if (transform.position == target && waypoinIndex == gamecontroller.check_Chain.Count)
                {
                    win = true;
                    rb.AddForce(transform.right * 50f);
                    hamster.PlayAnimHamster(3);
                    Invoke("Win", 1f);
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
        if (gamecontroller.addOpaque[waypoinIndex])
        {

            if (gamecontroller.check_Chain[waypoinIndex])
            {
                // cat.PlayAnimCat(4);
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 1f);
                hamster.PlayAnimHamster(0);
                Invoke("ShibaHide", 3f);

                pause = true;

                Invoke("See", 5f);

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
        }
        else
        {
            if (gamecontroller.check_Chain[waypoinIndex])
            {
                // cat.PlayAnimCat(4);
                waypoinIndex += 1;
                // cat.PlayAnimCat(4);
                Debug.Log("dung dieu kien 2");

            }
            else
            {
                cat.PlayAnimCat(4);
                Invoke("IsDoubt", 0.5f);
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
        if (pause)
        {

            waypoinIndex += 1;
            pause = false;
            Cat.instance.PlayAnimCat(3);
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

        rb.velocity = Vector2.zero;
    }
}

