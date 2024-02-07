using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    #region SerializeField
    [SerializeField]
    GameObject ClearPanel; //クリア画面

    [SerializeField]
    GameObject GameOverPanel; //ゲームオーバー画面

    [SerializeField]
    Text　textIQ; //HPのようなもの

    [SerializeField]
    Text  textStageName; //ステージの名前
    [SerializeField]
    private GameObject createcam; //クリエイトモードのカメラ

    [SerializeField]
    private GameObject playcam; //クリエイトモードのカメラ


    [SerializeField]
    private GameObject optionpanel; //設定のパネル

    [SerializeField]
    private GameObject playoptionpanel; //プレイ中の設定パネル

    [SerializeField]
    private GameObject player; //プレイヤー

    [SerializeField]
    private GameObject playercanvas; //プレイヤーのキャンバス

    [SerializeField]
    GameObject SavePanel;//セーブのためのパネル


    #endregion


    private GameObject[] Cube;
    public int IQ;//IQ = Hpみたいな感じ
    public string stageName;
    bool isOption;
    TitleScene title;
    StageManager stageManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        title = FindObjectOfType<TitleScene>();
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManagerのインスタンスを取得
        ClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        IQ = 150;

        if(title.playMode == true)
        {
            createcam.SetActive(false);
            playcam.SetActive(true);
            playercanvas.SetActive(false);
            playoptionpanel.SetActive(false);
            textStageName.text = stageName;
        }

        Destroy(title.gameObject);
        Destroy(stageManagerInstance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CubeCheck();
        textIQ.text = "IQ:" + string.Format("{0:D3}",IQ);
    }

    void CubeCheck()
    {
        Cube = GameObject.FindGameObjectsWithTag("Cube");

        if(Cube.Length == 0)
        {
            ClearPanel.SetActive(true);
        }
    }

    public void DecreaseIQ(int amount)
    {
        IQ -= amount;

        if(IQ <= 0)
        {
            GameOverPanel.SetActive(true);
        }
    }


    #region 設定

    public void OptionButton()
    {
        if (isOption == false)
        {
            isOption = true;
            optionpanel.SetActive(true);
        }
        else
        {
            isOption = false;
            optionpanel.SetActive(false);
        }
    }

    public void SavePanelButton()
    {
        SavePanel.SetActive(true);
    }

    public void SavePanelBackButton()
    {
        SavePanel.SetActive(false);
    }

    public void PlayOptionButton()
    {
        if (isOption == false)
        {
            isOption = true;
            playoptionpanel.SetActive(true);
        }
        else
        {
            isOption = false;
            playoptionpanel.SetActive(false);
        }
    }
    #endregion

}
