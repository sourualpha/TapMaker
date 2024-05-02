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

    [SerializeField]
    private AudioClip soundEffect; //���ʉ�
    #endregion


    private GameObject[] Brock;
    public int IQ; //IQ = Hp�݂����Ȋ���
    public string stageName; //�X�e�[�W�̖��O
    public bool isOption; //�ݒ���J���Ă邩�ǂ���
    TitleScene title;
    StageManager stageManagerInstance;
    AudioSource audioSource; //BGM


    // Start is called before the first frame update
    void Start()
    {

        title = FindObjectOfType<TitleScene>(); // TitleScene�̃C���X�^���X���擾
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManager�̃C���X�^���X���擾
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

    #region �N���A�`�F�b�N

    //�L���[�u���Q�[�����ɑ��݂��Ȃ��ꍇ�N���A��ʂ��f��
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

    #region �ݒ�

    //��郂�[�h�Őݒ���J���{�^��
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

    //�ۑ������ʂ��o���{�^��
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

    //�v���C���[�h�Őݒ���J���{�^��
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
