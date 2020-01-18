using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    private float camX = 0, camY = 0, previousX = 0, previousY = 0, zoom = 0;

    private bool started = false;

    public float sensitivity = 3, zoomSense = 3, zoomMin = 10, zoomMax = 20;

    void Start()
    {
        zoom = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (!started)
            {
                started = true;
                previousX = Input.mousePosition.x;
                previousY = Input.mousePosition.y;
                camX = this.transform.position.x;
                camY = this.transform.position.z;
            }
            else
            {
                float newX = (Input.mousePosition.x - previousX) * -sensitivity;
                float newY = (Input.mousePosition.y - previousY) * -sensitivity;

                this.transform.position = new Vector3(camX + newX, zoom, camY + newY);
            }

        }
        else
        {
            started = false;
        }

        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            zoom -= scroll * zoomSense;
            zoom = Mathf.Max(zoom, zoomMin);
            zoom = Mathf.Min(zoom, zoomMax);
            Vector3 old = this.transform.position;

            this.transform.position = new Vector3(old.x, zoom, old.z);
        }

    }
}
