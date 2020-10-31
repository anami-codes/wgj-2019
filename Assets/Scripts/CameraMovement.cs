using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public Transform maxY;
    public Transform minY;
    public Transform minX;
    public Transform maxX;

    private float x;
    private float y;
    private float z;

    void Update() {
        y = cam.position.y;
        x = cam.position.x;
        z = cam.position.z;

        if (player.position.y < maxY.position.y && player.position.y > minY.position.y) {
            y = player.position.y;
        }
        if (player.position.x < maxX.position.x && player.position.x > minX.position.x) {
            x = player.position.x;
        }

        cam.position = new Vector3(x,y,z);
    }
}
