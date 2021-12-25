using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    public static int correctNum = 0;
    public string color;

    private void OnTriggerEnter(Collider other) {
        if (other.name != color + "LittleCube") return;
        correctNum ++;
    }

    private void OnTriggerExit(Collider other) {
        if (other.name != color + "LittleCube") return;
        correctNum --;
    }
}
