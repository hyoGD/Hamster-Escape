using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private List<Icon> chainIconList = new List<Icon>();
    private List<type> addChain = new List<type>();
    [SerializeField] public List<bool> addOpaque = new List<bool>();
    [SerializeField] public List<GameObject> addItem = new List<GameObject>();
    [SerializeField] public List<bool> check_Chain;
    [SerializeField] Icon chainPrefab;
    private Icon s_Chain; //obj chua obj duoc khoi tao tu prefab
    [SerializeField] public Transform c_tranform, hidd_tranform;
    [SerializeField] public List<Transform> targets;
    [SerializeField] int topIndChain;
    private float distance;



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
        //if (place == null)
        //{
        //    place = GameObject.Find("Cat").GetComponent<MovingPlayer>();
        //}
        //spawnAnim = Instantiate(anim);
        //  spawnPlace = Instantiate(place);
        // spawnTutor = Instantiate(tutor);
        #endregion
        index = PlayerPrefs.GetInt("Index", 0);

        SetUpGame();
    }
    void Start()
    {
        for (int i = 0; i < buttonSwitch.Length; i++)
        {
            int ind = i;
            buttonSwitch[i].onClick.AddListener(() => Click(loaihinh[ind]));
        }


        #region tinh toan khoang cach tu diem start den diem cuoi(diem dich)
        //  targets[targets.Count - 1].transform.position += new Vector3(data.Items[index].distanceTrack, 0, 0);   //dat vi tri ve dich theo data
        distance = (targets[targets.Count - 1].transform.position - MovingPlayer.instance.transform.position).magnitude; //koang cach tu vi tri dau den vi tri ve dich(vi tri cuoi cug)
        s_distance.maxValue = distance;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
        if (chainIconList.Count == 0)
        {
            if (startImage != null)
            {
                startImage.SetActive(true);
            }
            for (int i = 0; i < buttonSwitch.Length; i++)
            {
                int ind = i;
                // buttonSwitch[ind].onClick.RemoveAllListeners();
                buttonSwitch[i].interactable = false;
            }
            Invoke("SetUi", 0.5f);
        }
    }
    private void FixedUpdate()
    {
        distance = (targets[targets.Count - 1].transform.position - MovingPlayer.instance.transform.position).magnitude;
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

        if (index >= data.Items[data.Items.Count - 1].level - 1)
        {
            index = data.Items[data.Items.Count - 1].level - 1;
            PlayerPrefs.SetInt("Index", index);
            txtLevelCurrent.text = "0" + data.Items[data.Items.Count - 1].level.ToString();
            txtLevelNext.text = "0" + data.Items[data.Items.Count - 1].level.ToString();
            Debug.Log("index da max");
        }
        else if (index < data.Items[data.Items.Count - 1].level - 1)
        {
            txtLevelCurrent.text = "0" + data.Items[index].level.ToString();
            txtLevelNext.text = "0" + data.Items[index + 1].level.ToString();
            Debug.Log("index chua max");
        }
        txtLevel.text = "Level " + txtLevelCurrent.text;
        #endregion

        foreach (type t in data.question[index].chainArr)      //add tat ca phan tu type t trong mang type chainArr trong Data
        {
            addChain.Add(t);
        }
        foreach (bool o in data.question[index].color)      //add tat ca phan tu bool o trong mang bool color trong Data
        {
            addOpaque.Add(o);
        }
        foreach (GameObject h in data.Items[index].amountItem)       //add tat ca phan tu gameObject h trong mang amoutItem trong Data
        {
            addItem.Add(h);
        }
        for (int i = 0; i <= index; i++)
        {
            s_Chain = Instantiate(chainPrefab, c_tranform); //khoi tao chuoi bang prefab Chain
            s_Chain.transform.SetParent(c_tranform);
            chainIconList.Add(s_Chain);   //add chuoi vua khoi tao vao list chainIconList de sau de su dung

            s_Chain.t_icon = addChain[i]; //dat kieu type hien thi 
            s_Chain.opaque = addOpaque[i]; // dat color hien thi
        }
        for (int i = 0; i < data.Items[index].amountItem.Length; i++)
        {
            if (i == 0 /*&& i== data.Items[index].amountItem.Length-1*/)
            {
                GameObject test = Instantiate(addItem[i], new Vector2(i, 3), Quaternion.identity);
                test.transform.SetParent(hidd_tranform.transform);
                targets.Add(test.transform.GetChild(0).transform);
            }
            else /*if(i > 0 && i < data.Items[index].amountItem.Length-1)*/
            {
                GameObject test = Instantiate(addItem[i], new Vector2(i * 10f, 2f), Quaternion.identity);
                test.transform.SetParent(hidd_tranform.transform);
                targets.Add(test.transform.GetChild(0).transform);
            }
        }
        //deco item
        for (int i = 0; i <= data.itemDeco.Count - 1; i++)
        {
            GameObject itemDeco = Instantiate(data.itemDeco[i], new Vector2(i + Random.Range(5f, 50f), Random.Range(0.5f, 4.5f)), Quaternion.identity);
            itemDeco.transform.SetParent(hidd_tranform.transform);
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
            if (chainIconList[0].opaque)
            {

                if (c_loaihinh == addChain[topIndChain])
                {
                    chainIconList[0].isTrue = true; //luon luon la phan tu 0 trong list chain vi sau khi cick minh thuc hien xoa phan tu dau
                    Debug.Log("Ban da chon dung");
                    check_Chain.Add(chainIconList[0].isTrue);
                }
                else
                {
                    chainIconList[0].isTrue = false; //luon luon la phan tu 0 trong list chain vi sau khi cick minh thuc hien xoa phan tu dau
                    Debug.Log("Ban da chon sai");
                    check_Chain.Add(chainIconList[0].isTrue);
                }
            }
            else
            {
                /* kiem tra xem type button co bang voi type cua chuoi ky tu hay ko */
                if (c_loaihinh == addChain[topIndChain])
                {
                    chainIconList[0].isTrue = false; //luon luon la phan tu 0 trong list chain vi sau khi cick minh thuc hien xoa phan tu dau
                    check_Chain.Add(chainIconList[0].isTrue);
                    Debug.Log("Ban da chon sai");
                }
                else
                {
                    chainIconList[0].isTrue = true; //luon luon la phan tu 0 trong list chain vi sau khi cick minh thuc hien xoa phan tu dau
                    Debug.Log("Ban da chon dung");
                    check_Chain.Add(chainIconList[0].isTrue);
                }
            }
            chainIconList[0].normal = false;

            topIndChain++;
            #endregion
            /* xoa nut bam dau tien trong list */
            int ind = 0;
            chainIconList.RemoveAt(ind);
        }

    }

    public void SetUi()
    {
        Ui.SetActive(false);
        tutor.SetTutorial();
        //  anim.CatMoving();
        // Anim.instance.HIdeShiba();
        MovingPlayer.instance.isMoving = true;
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


}
