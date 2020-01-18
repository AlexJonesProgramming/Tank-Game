using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChanger : MonoBehaviour
{
    public enum buttonType { Play, Controls, Donate}
    public buttonType type = buttonType.Play;
    public Material red, blue;
    public GameObject levelSelect;
    private void Update()
    {
        if (true)//Input.touchCount > 0)
        {
            if (Input.GetMouseButtonDown(0))//Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        if (type == buttonType.Play)
                            levelSelect.SetActive(true);//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
            }
            
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material = red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = blue;
    }
}
