using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum type
{
    vuong,
    tron,
    tamgiac,
}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [Header("Button")]
    [SerializeField] private Button[] buttonSwitch;
    [SerializeField] private Slider s_distance;

    [Header("UI")]
    [SerializeField] private GameObject Ui;
    [SerializeField] private GameObject Win;
    [SerializeField] public GameObject Lose;
    [SerializeField] private GameObject startImage;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtLevelCurrent;
    [SerializeField] private TextMeshProUGUI txtLevelNext;

    [Header("Scripts, Data")]
    [SerializeField] private Tutorial tutor;
    [SerializeField] public Data data;

    [Header("Object va bien dem")]
    [SerializeField] public int index;
    [SerializeField] public type[] loaihinh;
    public List<Icon> chainIconList = new List<Icon>();
    public List<type> addChain = new List<type>();
    [SerializeField] public List<bool> addOpaque = new List<bool>();
    [SerializeField] public List<GameObject> addItem = new List<GameObject>();
    [SerializeField] public List<bool> check_Chain;
    [SerializeField] Icon chainPrefab;
    private Icon s_Chain; //obj chua obj duoc khoi tao tu prefab
    [SerializeField] public Transform hidd_tranform;
    [SerializeField] public RectTransform c_tranform;// endRectranform;
    [SerializeField] public List<Transform> targets;
    [SerializeField] int topIndChain;
    private float distance;
    private bool pause;
    private bool normalSpeed = true;
   public int indexIcon;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        #region search scripts
        //if (anim == null)
        //{
        //    anim = GameObject.Find("SettingAnim").GetComponent<Anim>();
        //}
        if (tutor == null)
        {
            tutor = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        }
        #endregion

        index = PlayerPrefs.GetInt("Index", 0);
        foreach (type t in data.chainArr)      //add tat ca phan tu type t trong mang type chainArr trong Data
        {
            addChain.Add(t);
        }
       
    }
    void Start()
    {
        for (int i = 0; i < buttonSwitch.Length; i++)
        {
            int ind = i;
            buttonSwitch[i].onClick.AddListener(() => Click(loaihinh[ind]));
        }

        SetUpGame();

        #region tinh toan khoang cach tu diem start den diem cuoi(diem dich)
        //  targets[targets.Count - 1].transform.position += new Vector3(data.Items[index].distanceTrack, 0, 0);   //dat vi tri ve dich theo data
        distance = ((targets[targets.Count - 1].transform.position + Vector3.right) - MovingPlayer.instance.transform.position).magnitude; //koang cach tu vi tri dau den vi tri ve dich(vi tri cuoi cug)
        s_distance.maxValue = distance;
        #endregion

        //  c_tranform.DOAnchorPos(endRectranform.anchoredPosition, 1f).SetEase(Ease.OutQuart);

    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
        if (/*chainIconList.Count == 0*/ topIndChain == data.question[index].soluongchuoi[indexIcon - 1])
        {
            pause = false;

            for (int i = 0; i < buttonSwitch.Length; i++)
            {
                int ind = i;
                // buttonSwitch[ind].onClick.RemoveAllListeners();
                buttonSwitch[i].interactable = false;
            }

            Invoke("SetUi", 0.5f);
        }
        else
        {
            for (int i = 0; i < buttonSwitch.Length; i++)
            {
               // int ind = i;
                // buttonSwitch[ind].onClick.RemoveAllListeners();
                buttonSwitch[i].interactable = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            InitListChair();
        } 
    }
    private void FixedUpdate()
    {
        distance = ((targets[targets.Count - 1].transform.position + Vector3.right) - MovingPlayer.instance.transform.position).magnitude;
        s_distance.value = distance;

        if (distance <= 0.1f)
        {
            int lv = index;
            lv++;         
            PlayerPrefs.SetInt("Index", lv);
            Win.SetActive(true);
        }
    }

    public void SetUpGame()
    {
        if (index == 0)
        {
            for (int i = 0; i < buttonSwitch.Length; i++)
            {
                buttonSwitch[1].gameObject.SetActive(false);
                buttonSwitch[2].gameObject.SetActive(false);
            }
        }


        #region tinh toan thong so trong Game bang voi du lieu trong data 

        if (index >= data.question[data.question.Count - 1].id - 1)
        {
            index = data.question[data.question.Count - 1].id - 1;
            PlayerPrefs.SetInt("Index", index);

            txtLevelCurrent.text = data.question[data.question.Count - 1].id.ToString();
            txtLevelNext.text = data.question[data.question.Count - 1].id.ToString();

            Debug.Log("index da max");
        }
        else if (index < data.question[data.question.Count - 1].id - 1)
        {
            if (data.question[index].id < 9)
            {
                txtLevelCurrent.text = "0" + data.question[index].id.ToString();
                txtLevelNext.text = "0" + data.question[index + 1].id.ToString();
            }
            else if (data.question[index].id >= 9)
            {
                txtLevelCurrent.text = data.question[index].id.ToString();
                txtLevelNext.text = data.question[index + 1].id.ToString();
            }
            Debug.Log("index chua max");
        }
        txtLevel.text = "Level " + txtLevelCurrent.text;
        #endregion

        #region InitListChain
        for (int i = 0; i < data.question[index]./*chainArr.Length - 1*/soluongchuoi[indexIcon]; i++)
        {
            s_Chain = Instantiate(chainPrefab, c_tranform); //khoi tao chuoi bang prefab Chain
                                                            //  s_Chain.transform.SetParent(c_tranform);
            chainIconList.Add(s_Chain);   //add chuoi vua khoi tao vao list chainIconList de sau de su dung

            // s_Chain.t_icon = addChain[i]; //dat kieu type hien thi 
            //  s_Chain.opaque = addOpaque[i]; // dat color hien thi
          
           
            if (index == 0)
            {
                s_Chain.t_icon = addChain[2];
                s_Chain.opaque = data.question[i].colorr;
            }
            else
            {
                if (index < 10)
                {
                    s_Chain.t_icon = addChain[Random.Range(0, addChain.Count)];
                    s_Chain.opaque = data.question[i].colorr = true;
                }
                else if (index == 10)
                {
                    s_Chain.t_icon = addChain[2];
                    s_Chain.opaque = !data.question[i].colorr;
                }
                else
                {
                    s_Chain.t_icon = addChain[Random.Range(0, addChain.Count)];
                    int a = Random.Range(0, 2);
                    s_Chain.opaque = data.question[i].colorr = a == 1 ? true : false;
                }

            }
            addOpaque.Add(s_Chain.opaque);
        }
        indexIcon++;
        #endregion

        #region Init Obj
        for (int i = 0; i < data.question[index].Obj; i++)
        {
            if (i == 0)
            {
                GameObject test = Instantiate(/*addItem[i]*/data.start, new Vector2(i + 0.4f, 2), Quaternion.identity);
                test.transform.SetParent(hidd_tranform.transform);
                targets.Add(test.transform.GetChild(0).transform);
            }
            else
            {
                if (i < data.question[index].Obj - 1)
                {
                    int n = 0;
                    if (addOpaque[n] == true)
                    {
                        var ran = Random.Range(0, data.Obj.Length - 1);
                        GameObject test = Instantiate(/*addItem[i]*/data.Obj[ran], new Vector2(i * 10f, 1f), Quaternion.identity);
                        test.transform.SetParent(hidd_tranform.transform);
                        targets.Add(test.transform.GetChild(0).transform);
                    }
                    else
                    {
                        var ran = Random.Range(0, data.ObjHide.Length - 1);
                        GameObject test = Instantiate(/*addItem[i]*/data.ObjHide[ran], new Vector2(i * 10f, 1f), Quaternion.identity);
                        test.transform.SetParent(hidd_tranform.transform);
                        targets.Add(test.transform.GetChild(0).transform);
                    }
                    n++;

                }
                else if (i == data.question[index].Obj - 1)
                {
                    GameObject test = Instantiate(/*addItem[i]*/ data.Finish, new Vector2(i * 10f, 1f), Quaternion.identity);
                    test.transform.SetParent(hidd_tranform.transform);
                    targets.Add(test.transform.GetChild(0).transform);
                }
            }
        }
        #endregion

        #region Inite DecoItem
        for (int i = 0; i <= data.itemDeco.Count - 1; i++)
        {
            GameObject itemDeco;
            float x = Random.Range(5f, 50f);
            //  if(data.itemDeco[i].transform.position ) check dk de iteam deco ko nam len nhau
            itemDeco = Instantiate(data.itemDeco[i], new Vector2(i + x, 3f), Quaternion.identity);
            itemDeco.transform.SetParent(hidd_tranform.transform);
        }
        #endregion
    }
   
  

    public void InitListChair()
    {
        if (indexIcon < data.question[index].soluongchuoi.Length)
        {

            pause = true;
            topIndChain = 0;
            foreach (Icon i in chainIconList)
            {
                Destroy(i.gameObject);
            }
            chainIconList.Clear();
            addOpaque.Clear();
            check_Chain.Clear();

            for (int i = 0; i < data.question[index]./*chainArr.Length - 1*/soluongchuoi[indexIcon]; i++)
            {
                s_Chain = Instantiate(chainPrefab, c_tranform); //khoi tao chuoi bang prefab Chain
                                                                //  s_Chain.transform.SetParent(c_tranform);
                chainIconList.Add(s_Chain);   //add chuoi vua khoi tao vao list chainIconList de sau de su dung

                // s_Chain.t_icon = addChain[i]; //dat kieu type hien thi 
                //  s_Chain.opaque = addOpaque[i]; // dat color hien thi
                s_Chain.t_icon = addChain[Random.Range(0, addChain.Count - 1)];
                int a = Random.Range(0, 2);
                // s_Chain.opaque = data.question[i].colorr = a == 1 ? true : false;
                if (index <= 10)
                {
                    s_Chain.opaque = data.question[i].colorr = true;
                }
                else
                {
                    s_Chain.opaque = data.question[i].colorr = a == 1 ? true : false;
                }
                addOpaque.Add(s_Chain.opaque);
            }
            indexIcon++;
            Debug.Log("Init chain");
        }
        else
        {
            return;
        }
    }
    public void Click(type c_loaihinh)
    {

        if (chainIconList.Count != 0)
        {
            #region test type button
            //if (c_loaihinh == type.tron)
            //{
            //    Debug.Log("Day la hinh tron");
            //}
            //else if (c_loaihinh == type.tamgiac)
            //{
            //    Debug.Log("Day la hinh tam giac");
            //}
            //else if (c_loaihinh == type.vuong)
            //{
            //    Debug.Log("Day la hinh vuong");
            //}
            #endregion

            #region kiem tra xem type button co bang voi type cua chuoi ky tu hay ko */
            if (chainIconList[topIndChain].opaque)
            {

                if (c_loaihinh == /*addChain[topIndChain]*/chainIconList[topIndChain].t_icon)
                {
                    chainIconList[topIndChain].isTrue = true;
                    Debug.Log("Ban da chon dung");
                    check_Chain.Add(chainIconList[topIndChain].isTrue);
                }
                else
                {
                    chainIconList[topIndChain].isTrue = false;
                    Debug.Log("Ban da chon sai");
                    check_Chain.Add(chainIconList[topIndChain].isTrue);
                }
            }
            else
            {
                /* kiem tra xem type button co bang voi type cua chuoi ky tu hay ko */
                if (c_loaihinh ==/* addChain[topIndChain]*/chainIconList[topIndChain].t_icon)
                {
                    chainIconList[topIndChain].isTrue = false;
                    check_Chain.Add(chainIconList[topIndChain].isTrue);
                    Debug.Log("Ban da chon sai");
                }
                else
                {
                    chainIconList[topIndChain].isTrue = true;
                    Debug.Log("Ban da chon dung");
                    check_Chain.Add(chainIconList[topIndChain].isTrue);
                }
            }
            chainIconList[topIndChain].normal = false;

            topIndChain++;
            #endregion

        }

    }

    public void SetUi()
    {
        s_distance.gameObject.SetActive(true);
        Ui.SetActive(false);
        tutor.SetTutorial();
        // Anim.instance.HIdeShiba();
        ////  MovingPlayer.instance.isMoving = true;
        if (startImage != null)
        {
            startImage.SetActive(true);
        }

        if (!pause)
        {
            Ui.SetActive(false);
            MovingPlayer.instance.isMoving = true;
        }
        else
        {
            Ui.SetActive(true);
            MovingPlayer.instance.isMoving = false;
        }
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

        if (index == 9)
        {
         //   tutor.gameObject.SetActive(true);
            tutor.fistPlay = false;
            PlayerPrefs.SetInt("Tutorial", tutor.fistPlay ? 1 : 0);
        }
    }

   



    public void TimeSpeed()
    {
        if (normalSpeed)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        normalSpeed = !normalSpeed;
    }
}
