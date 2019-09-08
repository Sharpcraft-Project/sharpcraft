﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public List<GameObject> Bubbles;

    public InputField inputField;

    public int MessageLimit = 10;

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
                Bubbles.Remove(Bubbles[Bubbles.Count - 1]);
            }
        }
    }

    public void NewBubble(string str)
    {
        //add new bubble to list
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
            //wsClient.Send(inputField.text);
            inputField.text = "";
            FieldSwitch = false;
            HideInput();
        }
    }
}