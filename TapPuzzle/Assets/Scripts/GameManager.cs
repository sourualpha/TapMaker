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

    [SerializeField]
    private AudioClip soundEffect; //効果音
    #endregion


    private GameObject[] Brock;
    public int IQ; //IQ = Hpみたいな感じ
    public string stageName; //ステージの名前
    public bool isOption; //設定を開いてるかどうか
    TitleScene title;
    StageManager stageManagerInstance;
    AudioSource audioSource; //BGM


    // Start is called before the first frame update
    void Start()
    {

        title = FindObjectOfType<TitleScene>(); // TitleSceneのインスタンスを取得
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManagerのインスタンスを取得
        audioSource = GetComponent<AudioSource>();

        ClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        IQ = 150;

        if(title.isplayMode == true)
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
        BrockCheck();
        textIQ.text = "IQ:" + string.Format("{0:D3}",IQ);
    }

    #region クリアチェック

    //キューブがゲーム内に存在しない場合クリア画面を映す
    void BrockCheck()
    {
        Brock = GameObject.FindGameObjectsWithTag("Cube");

        if(Brock.Length == 0)
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
    #endregion

    #region 設定

    //作るモードで設定を開くボタン
    public void OptionButton()
    {
        audioSource.PlayOneShot(soundEffect);
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

    //保存する画面を出すボタン
    public void SavePanelButton()
    {
        audioSource.PlayOneShot(soundEffect);
        SavePanel.SetActive(true);
    }

    public void SavePanelBackButton()
    {
        audioSource.PlayOneShot(soundEffect);
        SavePanel.SetActive(false);
    }

    //プレイモードで設定を開くボタン
    public void PlayOptionButton()
    {
        audioSource.PlayOneShot(soundEffect);
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
