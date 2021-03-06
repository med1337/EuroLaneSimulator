﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDisplay : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float typing_speed;
    [SerializeField] float deactivate_delay;

    [Header("References")]
    [SerializeField] GameObject chat_panel;
    [SerializeField] Text chat_text;

    private string current_message;
    private float timer;
    private int character_index;
    private bool typing;


    public string DisplayPickupMessage()
    {
        string prefix = JobMessages.pickup_message_prefixes[Random.Range(0,
            JobMessages.pickup_message_prefixes.Length)];

        string cargo = JobMessages.cargo_types[Random.Range(0,
            JobMessages.cargo_types.Length)];

        string suffix = JobMessages.pickup_message_suffixes[Random.Range(0,
            JobMessages.pickup_message_suffixes.Length)];

        PrepForDisplay(prefix + cargo + suffix);

        return cargo;
    }

    
    public void DisplayDeliveryMessage()
    {
        string text = JobMessages.delivery_messages[Random.Range(0,
            JobMessages.delivery_messages.Length)];

        PrepForDisplay(text);
    }


    public void DisplayJobFailedMessage()
    {
        string text = JobMessages.job_fail_messages[Random.Range(0,
            JobMessages.job_fail_messages.Length)];

        PrepForDisplay(text);
    }


    void PrepForDisplay(string _text)
    {
        current_message = _text;

        chat_panel.SetActive(true);
        chat_text.text = "";
        character_index = 0;
        timer = 0;
        typing = true;

        CancelInvoke();
    }


    void Update()
    {
        if (typing && character_index < current_message.Length)
        {
            int prev_char_index = character_index;
            HandleTyping();

            if (character_index >= current_message.Length && prev_char_index < current_message.Length)
            {
                typing = false;
                Invoke("Deactivate", deactivate_delay);
            }
        }
    }


    void HandleTyping()
    {
        timer += Time.deltaTime;

        if (timer >= typing_speed)
        {
            timer = 0;
            chat_text.text += current_message[character_index++];
        }
    }


    void Deactivate()
    {
        chat_panel.SetActive(false);
        typing = false;
        chat_text.text = "";
        current_message = "";
    }

}
