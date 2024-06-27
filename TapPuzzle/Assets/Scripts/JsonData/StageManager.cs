using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    #region �{�^���֘A
    [SerializeField]
    GameObject tempButton;
    [SerializeField]
    GameObject imageScroll;
    #endregion

    #region �t�F�[�h�֘A
    [SerializeField]
    private Fade fade; //FadeCanvas�擾
    [SerializeField]
    private float fadeTime;  //�t�F�[�h���ԁi�b�j
    #endregion

    [SerializeField]
    GameObject LoadingPanel;//���[�h���
    public string stage;

    private static StageManager instance;

    AudioSource audioSource; //BGM

    [SerializeField]
    private AudioClip soundEffect;

    void Awake()
    {
        //�V�[���J�n���Ƀt�F�[�h���|����
        fade.FadeOut(fadeTime);

        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStageData();//json�t�@�C���̓ǂݍ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(fade.cutoutRange);
        Loading();
    }
    void Loading()
    {
        if(fade.cutoutRange > 1)
        {
            LoadingPanel.SetActive(true);
        }
        else
        {
            LoadingPanel.SetActive(false);
        }
    }

    #region json�t�@�C���̓ǂݍ���
    void LoadStageData()
    {
        string filePath = Application.dataPath + "/stages.json";

        if (File.Exists(filePath))
        {
            // �t�@�C�������݂���ꍇ�̓��[�h
            string json = File.ReadAllText(filePath);
            StageManagement stageManagement = JsonUtility.FromJson<StageManagement>(json);
            foreach (Stage stage in stageManagement.stages)
            {
                Debug.Log("Stage Name: " + stage.stageName);
                InstantButton(stage.stageName);
            }
        }
        else
        {
            Debug.LogError("�t�@�C����������܂���B");
        }
    }

    [Serializable]
    public class Stage
    {
        public string stageName;
    }

    [Serializable]
    public class StageManagement
    {
        public List<Stage> stages;
    }
    #endregion

    #region �{�^���̐���
    void InstantButton(string stagename)
    {
        var template = Instantiate(tempButton, imageScroll.transform);

        // ���O�̃Z�b�g
        template.transform.GetChild(0).GetComponent<Text>().text = stagename;

        // Button �R���|�[�l���g���擾���āA�{�^�����N���b�N���ꂽ�Ƃ��̏�����ݒ�
        Button button = template.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(stagename));
    }
    #endregion

    #region �{�^�����N���b�N���ꂽ���̏���
    public void OnButtonClick(string stageName)
    {
        audioSource.PlayOneShot(soundEffect);
        stage = stageName;
        Debug.Log("Button Clicked: " + stage);
        //�t�F�[�h���|���Ă���V�[���J�ڂ���
        fade.FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
        // �����Ƀ{�^�����N���b�N���ꂽ�Ƃ��̏�����ǉ�
    }
    #endregion

    public void ChangeGameMode()
    {
        SceneManager.LoadScene("GameScene");
    }
}
