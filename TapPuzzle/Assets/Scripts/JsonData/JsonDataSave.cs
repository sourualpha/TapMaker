using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonDataSave : MonoBehaviour
{

    [SerializeField]
    InputField stagename;

    [SerializeField]
    GameObject SavePanel;
    public void SaveButton()
    {
        // シーン内のすべての GameObject を取得
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // ブロックのリスト
        List<GameObject> blockList = new List<GameObject>();

        // ブロックだけを抽出してリストに追加
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Cube")) // ブロックに関するタグを設定していると仮定
            {
                blockList.Add(obj);
            }
        }

        SaveData save = new SaveData(); // SaveDataクラスをインスタンス化
        save.stageName = stagename.text;
        save.blocks = new BlockData[blockList.Count];

        for (int i = 0; i < blockList.Count; i++)
        {
            save.blocks[i] = new BlockData();
            save.blocks[i].position = new Vector3Data(
                blockList[i].transform.position.x,
                blockList[i].transform.position.y,
                blockList[i].transform.position.z
            );

            // プレハブの情報を追加
            save.blocks[i].prefabName = blockList[i].name;
        }

        SaveStagesData(save);
        SavePanel.SetActive(false);
    }

    // ステージデータの保存
    void SaveStagesData(SaveData saveData)
    {
        string filePath = Application.dataPath + "/Json/stages.json";

        StageManagement stageManagement;
        if (File.Exists(filePath))
        {
            // ファイルが存在する場合はロードして追加
            string json = File.ReadAllText(filePath);
            stageManagement = JsonUtility.FromJson<StageManagement>(json);
        }
        else
        {
            // ファイルが存在しない場合は新規作成
            stageManagement = new StageManagement();
            stageManagement.stages = new List<SaveData>();
        }

        // 同じ名前のステージがすでに存在する場合は上書き
        int existingIndex = stageManagement.stages.FindIndex(s => s.stageName == saveData.stageName);
        if (existingIndex != -1)
        {
            stageManagement.stages[existingIndex] = saveData;
        }
        else
        {
            // 存在しない場合は追加
            stageManagement.stages.Add(saveData);
        }

        // JSONファイルに保存
        string stagesJson = JsonUtility.ToJson(stageManagement, true);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(stagesJson);
        streamWriter.Flush();
        streamWriter.Close();
    }
}

[System.Serializable]
public class StageManagement
{
    public List<SaveData> stages;
}


[System.Serializable]
public class SaveData
{
    public string stageName;
    public BlockData[] blocks;
}

[System.Serializable]
public class BlockData
{
    public Vector3Data position;
    public string prefabName;
}

[System.Serializable]
public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
