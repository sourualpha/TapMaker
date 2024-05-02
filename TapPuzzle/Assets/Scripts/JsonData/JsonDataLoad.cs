using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonDataLoad : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public string prefabName;
        public GameObject prefab;
    }

    public List<PrefabData> blockPrefabs; // プレハブのリストをUnityインスペクタで設定
    private string currentStage; //現在のステージの名前
    public InputField currentStageName; //現在のステージの名前を入れる場所
    StageManager stageManagerInstance;
    public int blockCount;

    private void Awake()
    {
        
        stageManagerInstance = FindObjectOfType<StageManager>(); // StageManagerのインスタンスを取得
        if (stageManagerInstance != null)
        {
            // リセットと再読み込み
            ResetBlocks();
            // stage変数を参照
            currentStage = stageManagerInstance.stage;
            currentStageName.text = currentStage;//上書き保存のために現在のステージの名前を入れる
            LoadStage(currentStage);

            Debug.Log("Current Stage: " + currentStage);
        }
        else
        {
            Debug.LogError("StageManagerのインスタンスが見つかりません。");
        }
    }

    #region Jsonデータのロード、ブロックの配置
    void LoadStage(string stageName)
    {
        string filePath = Application.dataPath + "/stages.json";

        if (File.Exists(filePath))
        {
            // ファイルが存在する場合はロード
            string json = File.ReadAllText(filePath);
            StageManagement stageManagement = JsonUtility.FromJson<StageManagement>(json);

            // 指定されたステージ名のデータを検索
            SaveData save = stageManagement.stages.Find(x => x.stageName == stageName);

            if (save != null)
            {
                // 保存されたブロック情報を元に再生成
                foreach (BlockData blockData in save.blocks)
                {
                    Vector3 position = new Vector3(blockData.position.x, blockData.position.y, blockData.position.z);

                    // プレハブの名前を元に対応するPrefabを取得
                    GameObject prefab = GetPrefabByName(blockData.prefabName);

                    if (prefab != null)
                    {
                        GameObject newBlock = Instantiate(prefab, position, Quaternion.identity);
                        newBlock.name = blockData.prefabName; // ブロックの名前を設定
                        blockCount++;
                    }
                    else
                    {
                        Debug.LogWarning("Prefabが見つかりませんでした: " + blockData.prefabName);
                    }
                }

                Debug.Log("データのロードが完了しました。");
            }
            else
            {
                Debug.LogWarning("指定されたステージ名のデータが見つかりません: " + stageName);
            }
        }
        else
        {
            Debug.LogError("ファイルが見つかりません。");
        }
    }
    #endregion

    #region プレハブの名前を元に対応するPrefabを取得
    GameObject GetPrefabByName(string prefabName)
    {
        PrefabData prefabData = blockPrefabs.Find(x => x.prefabName == prefabName);
        if (prefabData != null)
        {
            return prefabData.prefab;
        }
        return null;
    }
    #endregion

    #region 配置しているブロックのリセット
    void ResetBlocks()
    {
        // シーン内のすべての GameObject を取得
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        Debug.Log("オブジェクトが破棄されました。");
        currentStage = "";
        // ブロックだけを抽出してリストに追加
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Cube")) // ブロックに関するタグを設定していると仮定
            {
                Destroy(obj);
                Debug.Log("オブジェクトが破棄されました。");
            }
        }
    }
    #endregion
}
