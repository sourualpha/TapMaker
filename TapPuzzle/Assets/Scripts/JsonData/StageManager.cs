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


    #region jsonファイルの読み込み
    void LoadStageData()
    {
        string filePath = Application.dataPath + "/Json/stages.json";

        if (File.Exists(filePath))
        {
            // ファイルが存在する場合はロード
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
            Debug.LogError("ファイルが見つかりません。");
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

    #region ボタンの生成
    void InstantButton(string stagename)
    {
        var template = Instantiate(tempButton, imageScroll.transform);

        // 名前のセット
        template.transform.GetChild(0).GetComponent<Text>().text = stagename;

        // Button コンポーネントを取得して、ボタンがクリックされたときの処理を設定
        Button button = template.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(stagename));
    }
    #endregion

    #region ボタンがクリックされた時の処理
    void OnButtonClick(string stageName)
    {
        stage = stageName;
        Debug.Log("Button Clicked: " + stage);
        Initiate.Fade("GameScene", Color.white, 1.0f);
        // ここにボタンがクリックされたときの処理を追加
    }
    #endregion
}
