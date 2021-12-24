using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject wholeCube;
    private List<GameObject> leftCubes;
    private List<GameObject> rightCubes;
    private List<GameObject> upCubes;
    private List<GameObject> downCubes;
    private RaycastHit hit;
    private bool hold = false;
    private List<GameObject> temp = null;
    private bool XorY;
    
    private bool getHit() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast((Ray) ray, out hit);
    }

    private GameObject getCube(GameObject current) {
        return current.transform.parent.gameObject;
    }

    private float generateOneAngle(float angle) {
        if (0 <= angle && angle < 45) return 0;
        if (45 <= angle && angle < 90) return 90;
        if (90 <= angle && angle < 135) return 90;
        if (135 <= angle && angle < 180) return 180;
        if (180 <= angle && angle < 225) return 180;
        if (225 <= angle && angle < 270) return 270;
        if (270 <= angle && angle < 315) return 270;
        if (315 <= angle && angle < 360) return 0;
        return 0;
    }

    private void generateRotation(GameObject current) {
        // if (current.name == "GameObject (14)") print(current.transform.rotation.eulerAngles);
        float rotationx = current.transform.rotation.eulerAngles.x;
        float rotationy = current.transform.rotation.eulerAngles.y;
        float rotationz = current.transform.rotation.eulerAngles.z;
        float newx = generateOneAngle(rotationx);
        float newy = generateOneAngle(rotationy);
        float newz = generateOneAngle(rotationz);
        current.transform.localRotation = Quaternion.Euler(newx, newy, newz);
    }

    private void generateCubes() {
        leftCubes.Clear();
        rightCubes.Clear();
        upCubes.Clear();
        downCubes.Clear();
        foreach (Transform cubeTrans in wholeCube.GetComponentInChildren<Transform>()) {
            GameObject cube = cubeTrans.gameObject;
            Transform real_cube = cube.transform.GetChild(0);
            if (Mathf.Abs(real_cube.position.x + 2) < 0.00001) leftCubes.Add(cube);
            if (Mathf.Abs(real_cube.position.x - 2) < 0.00001) rightCubes.Add(cube);
            if (Mathf.Abs(real_cube.position.y - 2) < 0.00001) upCubes.Add(cube);
            if (Mathf.Abs(real_cube.position.y + 2) < 0.00001) downCubes.Add(cube);
        }    
    }

    private void Start() {
        leftCubes = new List<GameObject>();
        rightCubes = new List<GameObject>();
        upCubes = new List<GameObject>();
        downCubes = new List<GameObject>();
        generateCubes();
    }

    private void Update() {
        if (!hold && !getHit()) return;
        GameObject current_object = hit.collider.gameObject;
        GameObject current_cube = getCube(current_object);
        if (Input.GetMouseButton(0)) {
            hold = true;
            float mouseX = Input.GetAxis("Mouse X") * 5;
            float mouseY = Input.GetAxis("Mouse Y") * 5;
            if (temp == null) {
                if (Mathf.Abs(mouseX) > Mathf.Abs(mouseY)) {
                    if (current_object.transform.position.y > 0) temp = upCubes;
                    else temp = downCubes;
                    XorY = true;
                } else if (Mathf.Abs(mouseX) < Mathf.Abs(mouseY)) {
                    if (current_object.transform.position.x > 0) temp = rightCubes;
                    else temp = leftCubes;
                    XorY = false;
                } else return;
            }
            bool flag = false;
            foreach (GameObject cube in temp) {
                if (cube.name == current_cube.name){
                    flag = true;
                    break;
                }
            }
            if (!flag) return;
            if (XorY) mouseY = 0;
            else mouseX = 0;
            foreach (GameObject cube in temp) {
                cube.transform.Rotate(mouseY, -mouseX, 0, Space.World);
            }
        } else if (Input.GetMouseButton(1)) {
            float mouseX = Input.GetAxis("Mouse X") * 5;
            float mouseY = Input.GetAxis("Mouse Y") * 5;
            // print(mouseX);
            hold = true;
            foreach (Transform cube in wholeCube.GetComponentInChildren<Transform>()) {
                cube.Rotate(mouseY, -mouseX, 0, Space.World);
            }
        } else{
            if (hold) {
                hold = false;
                foreach (Transform cube in wholeCube.GetComponentInChildren<Transform>()) {
                    generateRotation(cube.gameObject);
                }
                generateCubes();
                temp = null;
            }

        }
    }
}