﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public List<GameObject> Bubbles;

    public InputField inputField;

    public GameObject ChatBack;

    public GameObject BubblePrefab;

    public int MessageLimit = 10;

    public int LettersPerLine = 10;

    public float BubbleHeight = 19.8f;

    private void Awake()
    {
        HideInput();
    }

    private void Start()
    {
        HideInput();
    }

    public void HideInput()
    {
        inputField.text = "";
        inputField.gameObject.SetActive(false);
        //make all bubbles lighter
    }

    public void ShowInput()
    {
        inputField.gameObject.SetActive(true);
        //make all bubbles darker
    }

    bool isChat(string str)
    {
        if (wsClient.Split(str)[0] == "MSG")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void onMessage(string str)
    {
        if (isChat(str))
        {
            NewBubble(str);

            if (Bubbles.Count > MessageLimit)
            {
                GameObject Removed = Bubbles[Bubbles.Count - 1];
                //Bubbles.Remove(Bubbles[Bubbles.Count - 1]);
                Destroy(Removed);
            }
        }
    }

    public void NewBubble(string str)
    {
        GameObject bubble = Instantiate(BubblePrefab);
        bubble.transform.SetParent(ChatBack.transform);
        float newHeight = BubbleHeight;// * (str.Length / LettersPerLine);

        bubble.GetComponent<RectTransform>().sizeDelta = new Vector2(ChatBack.GetComponent<RectTransform>().sizeDelta.x, newHeight);
        bubble.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, bubble.GetComponent<RectTransform>().sizeDelta.y / 2);
        bubble.transform.GetChild(0).GetComponent<Text>().text = str;

        //move all bubbles up
        for(int i = 0; i < Bubbles.Count; i++)
        {
            Bubbles[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, bubble.GetComponent<RectTransform>().sizeDelta.y);
        }

        Bubbles.Add(bubble);
    }

    public void DisplayBubbles()
    {
        //display all bubbles out of Bubbles list
    }

    bool FieldSwitch = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !inputField.isFocused)
        {
            if (FieldSwitch)
            {
                FieldSwitch = false;
                HideInput();
            }
            else
            {
                FieldSwitch = true;
                ShowInput();

                inputField.Select();
                inputField.ActivateInputField();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(inputField.text != "")
            {
                //wsClient.Send(inputField.text);
                NewBubble(inputField.text);
                inputField.text = "";
                FieldSwitch = false;
                HideInput();
            }
            else
            {
                FieldSwitch = false;
                HideInput();
            }
        }
    }
}