// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PixelCrushers.DialogueSystem.DialogueEditor;
using PixelCrushers.DialogueSystem.OpenAIAddon.DialogueSmith;

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    /// <summary>
    /// Dialogue System Addon for OpenAI window.
    /// Actual functionality is handled through panels (subclasses of DialogueSystemOpenAIWindowPanel).
    /// </summary>
    public class DialogueSystemOpenAIWindow : EditorWindow
    {
        
        public static DialogueSystemOpenAIWindow Instance { get; private set; }

        public const string OpenAIKey = "PixelCrushers.OpenAIKey"; // OpenAI API key stored in EditorPrefs.
        public const string ElevenLabsKey = "PixelCrushers.ElevenLabsKey"; // ElevenLabs API key stored in EditorPrefs.
        public const string DialogueSmithKey = "PixelCrushers.DialogueSmithKey"; // Dialogue Smith API key stored in EditorPrefs.

        [MenuItem("Tools/Pixel Crushers/Dialogue System/Addon for OpenAI/Main Window")]
        public static void OpenMain()
        {
            Open(AIRequestType.General, DialogueEditorWindow.GetCurrentlyEditedDatabase(), null, null, null);
        }

        public static void Open(AIRequestType request, DialogueDatabase database, Asset asset, DialogueEntry entry, Field field)
        {
            var window = GetWindow<DialogueSystemOpenAIWindow>("Dialogue AI");

            // Open connection to OpenAI:
            var apiKey = EditorPrefs.GetString(OpenAIKey);
            var elevenLabsKey = EditorPrefs.GetString(ElevenLabsKey);
            var dialogueSmithKey = EditorPrefs.GetString(DialogueSmithKey);

            if (!OpenAI.IsApiKeyValid(apiKey))
            {
                if (request != AIRequestType.ConfigureApi) window.lastRequest = request;
                window.panel = new SettingsPanel(apiKey, database);
            }
            else if (request == AIRequestType.OriginalRequest && window.lastRequest != AIRequestType.OriginalRequest)
            {
                Open(window.lastRequest, database, asset, entry, field);
            }
            else
            {
                window.panel = GetPanel(request, apiKey, elevenLabsKey, dialogueSmithKey, database, asset, entry, field);
            }
            window.scrollPosition = Vector2.zero;
        }

        private static BasePanel GetPanel(AIRequestType request, string apiKey, string elevenLabsKey, string dialogueSmithKey,
            DialogueDatabase database, Asset asset, DialogueEntry entry, Field field)
        {
            switch (request)
            {
                default:
                case AIRequestType.ConfigureApi:
                    return new SettingsPanel(apiKey, database);
                case AIRequestType.General:
                    return new GeneralPanel(apiKey, database);
                case AIRequestType.GenerateConversation:
                    return new GenerateConversationPanel(apiKey, database);
                case AIRequestType.ExtendConversation:
                    return new ExtendConversationPanel(apiKey, database, asset, entry, field);
                case AIRequestType.ReviseText:
                    return new ReviseTextPanel(apiKey, database, asset, entry, field);
                case AIRequestType.LocalizeField:
                    return new TranslateFieldPanel(apiKey, database, asset, entry, field);
                case AIRequestType.LocalizeDatabase:
                    return new TranslateDatabasePanel(apiKey, database);
                case AIRequestType.Freeform:
                    return new FreeformChatPanel(apiKey, database);

                case AIRequestType.SelectVoice:
                    return new ElevenLabs.SelectVoicePanel(elevenLabsKey, database, asset, entry, field);
                case AIRequestType.GenerateVoice:
                    return new ElevenLabs.GenerateVoicePanel(elevenLabsKey, database, asset, entry, field);

                case AIRequestType.GeneratePortraits:
                    return (asset is Actor) ? new GeneratePortraitsPanel(apiKey, database, asset)
                        : new GeneralPanel(apiKey, database);

                case AIRequestType.BranchingDialogue:
                    return new BranchingDialoguePanel(dialogueSmithKey, database, asset as Conversation);
            }
        }

        private static Vector2 MinWindowSize = new Vector2(400, 400);

        private AIRequestType lastRequest = AIRequestType.General;
        private BasePanel panel;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnEnable()
        {
            Instance = this;
            minSize = MinWindowSize;
        }

        private void OnDisable()
        {
            Instance = null;
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            if (panel == null) OpenMain();
            panel.Draw();
            EditorGUILayout.EndScrollView();
        }

        public void SetLocalizationLanguages(List<string> languages)
        {
            (panel as TranslateDatabasePanel)?.SetLanguages(languages);
        }

    }
}

#endif
