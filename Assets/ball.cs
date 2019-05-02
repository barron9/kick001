using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{
    float presstime = 0f;
    // Start is called before the first frame update
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();

    }
        // Update is called once per frame
        void Update()
    {
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("1", LoadSceneMode.Single);
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Handle finger movements based on touch phase.
            GetComponent<LineRenderer>().SetPosition(0,transform.position);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    GetComponent<LineRenderer>().enabled = true;
                    presstime += 7f;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Stationary:
                    presstime += 7f;
                    break;
                case TouchPhase.Moved:
                    presstime += 7f;
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(-(-cam.WorldToScreenPoint(transform.position).x+touch.position.x)/10,2.5f, -(-cam.WorldToScreenPoint(transform.position).y+touch.position.y)/10));

                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    GetComponent<LineRenderer>().enabled = false;

                    GetComponent<Rigidbody>().AddForce(new Vector3(-(-cam.WorldToScreenPoint(transform.position).x + touch.position.x) / 2, presstime/90, -(-cam.WorldToScreenPoint(transform.position).y + touch.position.y) / 2),ForceMode.Impulse);
                    break;
            }
        }
    }
}
