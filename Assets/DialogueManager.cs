using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI avatarName;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;


    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        this.currentActors = actors;
        activeMessage = 0;
        Debug.Log(actors.Length);
        isActive = true;
        Debug.Log("Started Conversation" + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);
    }
    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        Debug.Log(currentActors.Length);
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];

        avatarName.text = actorToDisplay.name;
        avatarImage.sprite = actorToDisplay.sprite;
    }
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();

        }
        else
        {
            Debug.Log("Conversation Ended");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
        }
    }
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isActive == true)
        {
            NextMessage();
        }
    }
}
    

