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
    GameObject ClearPanel; //�N���A���

    [SerializeField]
    GameObject GameOverPanel; //�Q�[���I�[�o�[���

    [SerializeField]
    Text�@textIQ; //HP�̂悤�Ȃ���

    [SerializeField]
    Text  textStageName; //�X�e�[�W�̖��O
    [SerializeField]
    private GameObject createcam; //�N���G�C�g���[�h�̃J����

    [SerializeField]
    private GameObject playcam; //�N���G�C�g���[�h�̃J����


    [SerializeField]
    private GameObject optionpanel; //�ݒ�̃p�l��

    [SerializeField]
    private GameObject playoptionpanel; //�v���C���̐ݒ�p�l��

    [SerializeField]
    private GameObject player; //�v���C���[

    [SerializeField]
    private GameObject playercanvas; //�v���C���[�̃L�����o�X

    [SerializeField]
    GameObject SavePanel;//�Z�[�u�̂��߂̃p�l��


    #endregion


    private GameObject[] Cube;
    public int IQ;//IQ = Hp�݂����Ȋ���
    public string stageName;
    bool isOption;
    TitleScene title;
    StageManager stageManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        title = FindObjectOfType<TitleScene>();
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManager�̃C���X�^���X���擾
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


    #region �ݒ�

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
