// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    /// <summary>
    /// Dialogue System Addon for OpenAI welcome window.
    /// </summary>
    [InitializeOnLoad]
    public class WelcomeWindow : EditorWindow
    {

        private const string ShowOnStartEditorPrefsKey = "PixelCrushers.DialogueSystemOpenAIAddon.ShowWelcomeOnStart";
        private const string USE_OPENAI = "USE_OPENAI";

        private static WelcomeWindow instance;

        private static bool showOnStartPrefs
        {
            get { return EditorPrefs.GetBool(ShowOnStartEditorPrefsKey, true); }
            set { EditorPrefs.SetBool(ShowOnStartEditorPrefsKey, value); }
        }

        [MenuItem("Tools/Pixel Crushers/Dialogue System/Addon for OpenAI/Welcome Window", false, -2)]
        public static void Open()
        {
            instance = GetWindow<WelcomeWindow>(false, "Welcome");
            instance.minSize = new Vector2(350, 200);
            instance.showOnStart = true; // Can't check EditorPrefs when constructing window: showOnStartPrefs;
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            RegisterWindowCheck();
        }

        private static void RegisterWindowCheck()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.update -= CheckShowWelcomeWindow;
                EditorApplication.update += CheckShowWelcomeWindow;
            }
        }

        private static void CheckShowWelcomeWindow()
        {
            EditorApplication.update -= CheckShowWelcomeWindow;
            if (showOnStartPrefs)
            {
                Open();
            }
        }

        private bool showOnStart = true;
        private string openAIKey;
        private string elevenLabsKey;
        private string dialogueSmithKey;
        private GUIStyle heading;
        private GUIStyle labelWordWrapped;
        private GUIStyle labelHyperlink;
        private GUIStyle labelSuccess;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnEnable()
        {
#if USE_OPENAI
            openAIKey = EditorPrefs.GetString(DialogueSystemOpenAIWindow.OpenAIKey);
            elevenLabsKey = EditorPrefs.GetString(DialogueSystemOpenAIWindow.ElevenLabsKey);
            dialogueSmithKey = EditorPrefs.GetString(DialogueSystemOpenAIWindow.DialogueSmithKey);
#endif
        }

        private void OnDisable()
        {
            instance = null;
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            CheckGUIStyles();
            DrawBanner();
            DrawInfoText();
            DrawDefineSection();
            DrawKeySection();
            DrawOpenButton();
            DrawElevenLabsSection();
            DrawDialogueSmithSection();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.LabelField(string.Empty, GUILayout.Height(EditorGUIUtility.singleLineHeight + 8f));
            DrawFooter();
        }

        private void CheckGUIStyles()
        {
            if (heading == null)
            {
                heading = new GUIStyle(GUI.skin.label);
                heading.fontStyle = FontStyle.Bold;
                heading.fontSize = 16;
                heading.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            }
            if (labelWordWrapped == null)
            {
                labelWordWrapped = new GUIStyle(GUI.skin.label);
                labelWordWrapped.wordWrap = true;
            }
            if (labelHyperlink == null)
            {
                labelHyperlink = new GUIStyle(GUI.skin.label);
                labelHyperlink.normal.textColor = EditorGUIUtility.isProSkin ? Color.cyan : Color.blue;
            }
            if (labelSuccess == null)
            {
                labelSuccess = new GUIStyle(GUI.skin.label);
                labelSuccess.normal.textColor = EditorGUIUtility.isProSkin ? new Color(0.7f, 1, 0) : new Color(0, 0.7f, 0);
            }
        }

        private void DrawBanner()
        {
            EditorGUILayout.LabelField("Dialogue System Addon for OpenAI", heading);
            EditorGUILayout.Space();
        }

        private void DrawInfoText()
        {
            EditorGUILayout.LabelField("Welcome to the Dialogue System Addon for OpenAI!", labelWordWrapped);
            EditorGUILayout.LabelField("This addon is a third-party OpenAI API client. It is not affiliated with OpenAI Inc. " +
                "An OpenAI account is required. The addon will use your OpenAI account's API key. " +
                "You are responsible for any usage charges that OpenAI applies to your API key.", labelWordWrapped);
            if (GUILayout.Button("OpenAI Pricing", labelHyperlink))
            {
                Application.OpenURL("https://openai.com/api/pricing/");
            }
        }

        private void DrawDefineSection()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Enable Addon for OpenAI", EditorStyles.boldLabel);
            if (MoreEditorUtility.DoesScriptingDefineSymbolExist(USE_OPENAI))
            {
                EditorGUILayout.LabelField("✓ Enabled. (USE_OPENAI Scripting Define Symbol)", labelSuccess);
            }
            else
            {
                EditorGUILayout.LabelField("Click the button below to add the Scripting Define Symbol USE_OPENAI, " +
                    "which will enable the addon:", labelWordWrapped);
                if (GUILayout.Button("Enable Addon"))
                {
                    MoreEditorUtility.TryAddScriptingDefineSymbols(USE_OPENAI);
                    EditorUtility.DisplayDialog("Enable Addon", "Setting Scripting Define Symbol USE_OPENAI to " +
                        "enable the Dialogue System Addon for OpenAI.", "OK");
                    EditorTools.ReimportScripts();
                    Repaint();
                    GUIUtility.ExitGUI();
                }
            }
        }

        private void DrawKeySection()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Configure OpenAI Access", EditorStyles.boldLabel);
#if !USE_OPENAI
            EditorGUILayout.LabelField("Enable Addon first.", labelWordWrapped);
#else
            if (!OpenAI.IsApiKeyValid(openAIKey))
            {
                EditorGUILayout.LabelField("If you don't have an OpenAI API key, click here to create one:", labelWordWrapped);
            }
            else
            {
                EditorGUILayout.LabelField("✓ OpenAI Key Accepted.", labelSuccess);
            }
            if (GUILayout.Button("Create New OpenAI API Key"))
            {
                Application.OpenURL("https://platform.openai.com/account/api-keys");
            }

            EditorGUILayout.BeginHorizontal();
            openAIKey = EditorGUILayout.TextField("Open API Key", openAIKey);
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(openAIKey) || !openAIKey.StartsWith("sk-"));
            var connectButtonWidth = GUI.skin.button.CalcSize(new GUIContent("Connect")).x;
            if (GUILayout.Button("Connect", GUILayout.Width(connectButtonWidth)))
            {
                EditorPrefs.SetString(DialogueSystemOpenAIWindow.OpenAIKey, openAIKey);
            }
            EditorGUILayout.EndHorizontal();
#endif
        }

        private void DrawOpenButton()
        {
            EditorGUILayout.Space();
#if USE_OPENAI
            EditorGUI.BeginDisabledGroup(!(MoreEditorUtility.DoesScriptingDefineSymbolExist(USE_OPENAI)));
#else
            EditorGUI.BeginDisabledGroup(true);
#endif
            if (GUILayout.Button("OpenAI Addon Window"))
            {
#if USE_OPENAI
                DialogueSystemOpenAIWindow.OpenMain();
#endif
            }
            EditorGUI.EndDisabledGroup();
        }

        private void DrawElevenLabsSection()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Configure ElevenLabs Access (Optional)", EditorStyles.boldLabel);
#if !USE_OPENAI
            EditorGUILayout.LabelField("Enable Addon first.", labelWordWrapped);
#else
            if (!ElevenLabs.ElevenLabs.IsApiKeyValid(elevenLabsKey))
            {
                EditorGUILayout.LabelField("If you want to use ElevenLabs text to speech and don't have an ElevenLabs API key, click here to create one:", labelWordWrapped);
            }
            else
            {
                EditorGUILayout.LabelField("✓ ElevenLabs Key Accepted.", labelSuccess);
            }
            if (GUILayout.Button("Create New ElevenLabs API Key"))
            {
                Application.OpenURL("https://docs.elevenlabs.io/authentication/01-xi-api-key");
            }

            EditorGUI.BeginChangeCheck();
            elevenLabsKey = EditorGUILayout.TextField("ElevenLabs API Key", elevenLabsKey);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(DialogueSystemOpenAIWindow.ElevenLabsKey, elevenLabsKey); 
            }
#endif
        }

        private void DrawDialogueSmithSection()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Configure Dialogue Smith Access (Optional)", EditorStyles.boldLabel);
#if !USE_OPENAI
            EditorGUILayout.LabelField("Enable Addon first.", labelWordWrapped);
#else
            if (!DialogueSmith.DialogueSmith.IsApiKeyValid(dialogueSmithKey))
            {
                EditorGUILayout.LabelField("If you want to use Dialogue Smith for branching dialogue and don't have a Dialogue Smith API key, click here:", labelWordWrapped);
            }
            else
            {
                EditorGUILayout.LabelField("✓ Dialogue Smith Key Accepted.", labelSuccess);
            }
            if (GUILayout.Button("Create Dialogue Smith API Key"))
            {
                Application.OpenURL("https://dialoguesmith.com/");
            }

            EditorGUI.BeginChangeCheck();
            dialogueSmithKey = EditorGUILayout.TextField("Dialogue Smith API Key", dialogueSmithKey);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(DialogueSystemOpenAIWindow.DialogueSmithKey, dialogueSmithKey);
            }
#endif
        }

        private void DrawFooter()
        {
            var newShowOnStart = EditorGUI.ToggleLeft(new Rect(5, position.height - 5 - EditorGUIUtility.singleLineHeight, position.width - (70 + 150), EditorGUIUtility.singleLineHeight), "Show at start", showOnStart);
            if (newShowOnStart != showOnStart)
            {
                showOnStart = newShowOnStart;
                showOnStartPrefs = newShowOnStart;
            }
            if (GUI.Button(new Rect(position.width - 80, position.height - 5 - EditorGUIUtility.singleLineHeight, 70, EditorGUIUtility.singleLineHeight), new GUIContent("Support", "Contact the developer for support")))
            {
                Application.OpenURL("http://www.pixelcrushers.com/support-form/");
            }
        }

    }

}
