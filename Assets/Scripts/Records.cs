using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class Records : MonoBehaviour {

    public GameObject content;
    public GameObject textPrefab;
    
    private void writeJson(string filename, string jsonString) {
        FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush();
        fs.Close();
    }


    private void Start() {
        string filename = Application.streamingAssetsPath + "/records.json";
        JSONObject records = new JSONObject(Language.readJson(filename));
        JSONObject new_records = new JSONObject();
        JSONObject new_records_records = new JSONObject();
        int temp_num = 0;
        int record_rank = -1;
        int length = (int) records["length"].n;
        new_records.AddField("length", length + 1);

        for (int i = 0; i < length; i ++) {
            JSONObject current = records["records"][i.ToString()];
            if ((int) current["steps"].n >= Step.stepNum) {
                temp_num = 1;
                record_rank = i;
                JSONObject new_current_record = new JSONObject();
                new_current_record.AddField("time", System.DateTime.Now.ToString("yyyy:MM:dd"));
                new_current_record.AddField("steps", Step.stepNum);
                new_records_records.AddField(i.ToString(), new_current_record);
            }
            JSONObject new_one_record = new JSONObject();
            new_one_record.AddField("time", current["time"].str);
            new_one_record.AddField("steps", (int)current["steps"].n);
            new_records_records.AddField((i + temp_num).ToString(), new_one_record);
        }

        if (record_rank == -1) {
            record_rank = length;
            JSONObject new_current_record = new JSONObject();
            new_current_record.AddField("time", System.DateTime.Now.ToString("yyyy:MM:dd"));
            new_current_record.AddField("steps", Step.stepNum);
            new_records_records.AddField(record_rank.ToString(), new_current_record);
        }

        new_records.AddField("records", new_records_records);
        writeJson(filename, new_records.Print());

        Color text_color = Color.white;

        for (int i = 0; i < length + 1; i ++) {
            if (i == record_rank) text_color = Color.yellow;
            else text_color = Color.white;

            JSONObject current = new_records["records"][i.ToString()];

            GameObject rank_text = Instantiate(textPrefab);
            rank_text.transform.SetParent(content.transform);
            rank_text.GetComponent<Text>().text = i.ToString();
            rank_text.GetComponent<Text>().color = text_color;

            
            GameObject time_text = Instantiate(textPrefab);
            time_text.transform.SetParent(content.transform);
            time_text.GetComponent<Text>().text = current["time"].str;
            time_text.GetComponent<Text>().color = text_color;

            GameObject step_text = Instantiate(textPrefab);
            step_text.transform.SetParent(content.transform);
            step_text.GetComponent<Text>().text = current["steps"].n.ToString();
            step_text.GetComponent<Text>().color = text_color;

        }

    }
}