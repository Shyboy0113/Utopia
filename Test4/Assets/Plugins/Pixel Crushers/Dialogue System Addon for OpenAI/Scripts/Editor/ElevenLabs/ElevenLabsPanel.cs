// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using UnityEngine;
using UnityEditor;
using PixelCrushers.DialogueSystem.DialogueEditor;

namespace PixelCrushers.DialogueSystem.OpenAIAddon.ElevenLabs
{

    /// <summary>
    /// Panel to select an ElevenLabs voice actor.
    /// </summary>
    public class ElevenLabsPanel : BasePanel
    {

        protected AudioClip audioClip = null;
        protected virtual string Operation => "ElevenLabs";

        public ElevenLabsPanel(string apiKey, DialogueDatabase database,
            Asset asset, DialogueEntry entry, Field field)
            : base(apiKey, database, asset, entry, field)
        {
        }

        ~ElevenLabsPanel()
        {
            DestroyAudioClip();
        }

        protected virtual void RefreshEditor()
        {
            Undo.RecordObject(database, Operation);
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            DialogueEditorWindow.instance?.Reset();
            DialogueEditorWindow.instance?.Repaint();
            GUIUtility.ExitGUI();
        }

        protected void CloseWindow()
        {
            DialogueSystemOpenAIWindow.Instance.Close();
            GUIUtility.ExitGUI();
            DestroyAudioClip();
        }

        protected void DestroyAudioClip()
        {
            Object.DestroyImmediate(audioClip);
            audioClip = null;
        }

    }
}

#endif
