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
        // �V�[�����̂��ׂĂ� GameObject ���擾
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // �u���b�N�̃��X�g
        List<GameObject> blockList = new List<GameObject>();

        // �u���b�N�����𒊏o���ă��X�g�ɒǉ�
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Cube")) // �u���b�N�Ɋւ���^�O��ݒ肵�Ă���Ɖ���
            {
                blockList.Add(obj);
            }
        }

        SaveData save = new SaveData(); // SaveData�N���X���C���X�^���X��
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

            // �v���n�u�̏���ǉ�
            save.blocks[i].prefabName = blockList[i].name;
        }

        SaveStagesData(save);
        SavePanel.SetActive(false);
    }

    // �X�e�[�W�f�[�^�̕ۑ�
    void SaveStagesData(SaveData saveData)
    {
        string filePath = Application.dataPath + "/Json/stages.json";

        StageManagement stageManagement;
        if (File.Exists(filePath))
        {
            // �t�@�C�������݂���ꍇ�̓��[�h���Ēǉ�
            string json = File.ReadAllText(filePath);
            stageManagement = JsonUtility.FromJson<StageManagement>(json);
        }
        else
        {
            // �t�@�C�������݂��Ȃ��ꍇ�͐V�K�쐬
            stageManagement = new StageManagement();
            stageManagement.stages = new List<SaveData>();
        }

        // �������O�̃X�e�[�W�����łɑ��݂���ꍇ�͏㏑��
        int existingIndex = stageManagement.stages.FindIndex(s => s.stageName == saveData.stageName);
        if (existingIndex != -1)
        {
            stageManagement.stages[existingIndex] = saveData;
        }
        else
        {
            // ���݂��Ȃ��ꍇ�͒ǉ�
            stageManagement.stages.Add(saveData);
        }

        // JSON�t�@�C���ɕۑ�
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
