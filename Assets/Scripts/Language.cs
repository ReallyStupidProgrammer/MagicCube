using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Language : MonoBehaviour {

    public static int languageIndex = 0;
    public static JSONObject language = null;
    private static string[] filenames = {
        Application.streamingAssetsPath + "/english.json",
        Application.streamingAssetsPath + "/chinese.json"
    };
    private static JSONObject[] languages = {null, null};

    public static string readJson(string filename) {
        FileStream fs = new FileStream(filename, FileMode.Open);
        byte[] bytes = new byte[4096];
        int count = (int) fs.Length;
        fs.Read(bytes, 0, count);
        string str = new UTF8Encoding().GetString(bytes);
        fs.Close();
        return str;
    }

    private void Start() {
        string str = readJson(filenames[0]);
        JSONObject english = new JSONObject(str);
        str = readJson(filenames[1]);
        JSONObject chinese = new JSONObject(str);
        languages[0] = english;
        languages[1] = chinese;
        updateLanguage();
    }

    public static void updateLanguage() {
        language = languages[languageIndex];
    }
}
