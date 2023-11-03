using UnityEngine;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// This script adds the ability to backtrack conversations. To backtrack, call Backtrack(true).
    /// The bool parameter specifies whether to backtrack to an NPC line, which is what you usually
    /// want to do; otherwise if you're in a response menu you'll keep backtracking to the same 
    /// response menu instead of going back to a previous NPC line.
    /// </summary>
    public class Backtracker : MonoBehaviour
    {

        public bool debug;

        private Stack<ConversationState> stack = new Stack<ConversationState>();

        public void OnConversationStart(Transform actor)
        {
            stack.Clear();
            if (debug) Debug.Log("Backtracker: Starting a new conversation. Clearing stack.");
        }

        public void OnConversationLine(Subtitle subtitle)
        {
            stack.Push(DialogueManager.CurrentConversationState);
            if (debug) Debug.Log("Backtracker: Recording dialogue entry " + subtitle.dialogueEntry.conversationID + ":" + subtitle.dialogueEntry.id + " on stack: '" + subtitle.formattedText.text + "' (" + subtitle.speakerInfo.characterType + ").");
        }

        // Call this method to go back:
        public void Backtrack(bool toPreviousNPCLine)
        {
            if (stack.Count < 2) return;
            stack.Pop(); // Pop current entry.
            var destination = stack.Pop(); // Pop previous entry.
            if (toPreviousNPCLine)
            {
                while (!destination.subtitle.speakerInfo.IsNPC && stack.Count > 0)
                {
                    destination = stack.Pop(); // Keep popping until we get an NPC line.
                }
                if (!destination.subtitle.speakerInfo.IsNPC) return;
            }
            if (debug) Debug.Log("Backtracker: Backtracking to " + destination.subtitle.dialogueEntry.conversationID + ":" + destination.subtitle.dialogueEntry.id + " on stack: '" + destination.subtitle.formattedText.text + "' (" + destination.subtitle.speakerInfo.characterType + ").");
            DialogueManager.ConversationController.GotoState(destination);
        }
    }
}
