using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private float minXField;
    [SerializeField] private float maxXField;
    [SerializeField] private float minYField;
    [SerializeField] private float maxYField;

    private GameObject[] objects;
    private GameObject panel;
    private Button button;

    private int clicks;

    private bool gameActive;

    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;

        panel = transform.parent.gameObject;

        button = gameObject.GetComponent<Button>();

        objects = new GameObject[panel.transform.childCount];

        for(int i = 0; i < panel.transform.childCount; i++)
        {
            if (gameObject == panel.transform.GetChild(i).gameObject) continue;
            objects[i] = panel.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        

        if (clicks >= 5)
        {
            gameActive = true;
            ToggleObjects(false);
        }
        else if (clicks == 0)
        {
            gameActive = false;
            transform.position = startingPos;
            ToggleObjects(true);
        }

    }

    void ButtonGame(){
        transform.localPosition = new Vector3 (Random.Range(minXField, maxXField), Random.Range(minYField, maxYField), 0);
    }

    void ToggleObjects(bool b)
    {
        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;
            obj.SetActive(b);
        }
    }

    public void AddClicks()
    {
        clicks++;
        Invoke("RemoveClicks", 2);
        if (gameActive)
        {
            ButtonGame();
        }
    }

    void RemoveClicks()
    {
        clicks--;
    }

    private void OnMouseEnter()
    {
        Invoke("AddClicks", 1);
        Debug.Log("On Mouse Enter");
    }
}
