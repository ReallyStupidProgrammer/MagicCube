using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.name != "littleCube") return;
        print("Congratulations!");
    }
}
