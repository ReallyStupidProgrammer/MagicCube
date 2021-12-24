using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step : MonoBehaviour {

    public static int stepNum = 0;

    private void Start() {
        stepNum = 0;
    }

    private void Update() {
        gameObject.GetComponent<Text>().text = stepNum.ToString();
    }
}