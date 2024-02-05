using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject tempButton;
    [SerializeField]
    GameObject imageScroll;

    public string stage;

    private static StageManager instance;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStageData();
        }
        else
        {
            Destroy(gameObject);
            LoadStageData();
        }
    }


    #region json�t�@�C���̓ǂݍ���
    void LoadStageData()
    {
        string filePath = Application.dataPath + "/Json/stages.json";

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
    void OnButtonClick(string stageName)
    {
        stage = stageName;
        Debug.Log("Button Clicked: " + stage);
        Initiate.Fade("GameScene", Color.white, 1.0f);
        // �����Ƀ{�^�����N���b�N���ꂽ�Ƃ��̏�����ǉ�
    }
    #endregion
}
