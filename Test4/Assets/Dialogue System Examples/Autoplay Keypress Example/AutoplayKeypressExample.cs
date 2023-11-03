using UnityEngine;
using PixelCrushers.DialogueSystem;

public class AutoplayKeypressExample : ConversationControl
{
    public float autoPlayDelay = 0.2f;

    private bool isAutoPlayOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            ToggleAutoPlay();
        }
    }

    public override void ToggleAutoPlay()
    {
        base.ToggleAutoPlay();
        isAutoPlayOn = !isAutoPlayOn;
        foreach (var panel in DialogueManager.standardDialogueUI.conversationUIElements.subtitlePanels)
        {
            panel.GetTypewriter().enabled = !isAutoPlayOn;
        }
        if (isAutoPlayOn) DialogueManager.standardDialogueUI.OnContinueConversation();
    }

    public override void OnConversationLine(Subtitle subtitle)
    {
        if (isAutoPlayOn)
        {
            subtitle.sequence = $"Continue()@{autoPlayDelay}; {subtitle.sequence}";
        }
        else
        {
            base.OnConversationLine(subtitle);
        }
    }

    public override void OnConversationEnd(Transform actor)
    {
        base.OnConversationEnd(actor);
        isAutoPlayOn = false;
    }


}
