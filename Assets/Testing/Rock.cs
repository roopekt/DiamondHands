using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rock : MonoBehaviour
{
    private string savePath;

    private class SaveState {
        public float x = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePath = Application.dataPath + "/Testing/savefile.json";
        Debug.Log(savePath);
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            transform.position = Vector3.right * Random.Range(-2f, 2f);
            Save();
        }
    }

    void Load() {
        SaveState saveState = new SaveState();
        if (File.Exists(savePath)) {
            string saveString = File.ReadAllText(savePath);
            saveState = JsonUtility.FromJson<SaveState>(saveString);
        }

        transform.position = Vector3.right * saveState.x;
    }

    void Save() {
        var saveState = new SaveState {
            x = transform.position.x
        };

        string saveString = JsonUtility.ToJson(saveState);
        File.WriteAllText(savePath, saveString);
    }
}
