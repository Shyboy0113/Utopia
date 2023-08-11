// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.OpenAIAddon.ElevenLabs
{

    /// <summary>
    /// Panel to select an ElevenLabs voice actor.
    /// </summary>
    public class SelectVoicePanel : ElevenLabsPanel
    {

        private VoiceList voiceList;
        private string[] voiceNames = null;
        private int voiceIndex = -1;
        private string previewText = "Hello, world.";

        private static GUIContent HeadingLabel = new GUIContent("Select Voice Actor");

        protected override string Operation => "Select Voice";
        private Actor Actor => (asset is Actor) ? asset as Actor : null;
        private bool IsVoiceSelected => voiceNames != null && (0 <= voiceIndex && voiceIndex < voiceNames.Length);

        public SelectVoicePanel(string apiKey, DialogueDatabase database,
            Asset asset, DialogueEntry entry, Field field)
            : base(apiKey, database, asset, entry, field)
        {
            RefreshVoiceList();
        }

        ~SelectVoicePanel()
        {
            DestroyAudioClip();
        }

        public override void Draw()
        {
            base.Draw();
            DrawHeading(HeadingLabel, "Select an ElevenLabs voice actor.");
            DrawVoiceActorDropdown();
            DrawPreviewButton();
            DrawAcceptButton();
        }

        private void DrawVoiceActorDropdown()
        {
            if (IsAwaitingReply)
            {
                DrawStatus();
            }
            else if (voiceNames == null)
            {
                EditorGUILayout.HelpBox("Error retrieving voice actor list.", MessageType.Warning);
            }
            else
            {
                voiceIndex = EditorGUILayout.Popup("Voice Actor", voiceIndex, voiceNames);
            }
        }

        private void DrawPreviewButton()
        {
            if (IsVoiceSelected)
            {
                EditorGUILayout.BeginHorizontal();
                previewText = EditorGUILayout.TextField("Preview Text", previewText);
                if (GUILayout.Button("Preview", GUILayout.Width(60)))
                {
                    PreviewSelectedVoice();
                }
                EditorGUILayout.EndHorizontal();
            }
            if (IsAwaitingReply) return;
        }

        private void RefreshVoiceList()
        {
            IsAwaitingReply = true;
            ProgressText = "Retrieving ElevenLabs voice list.";
            ElevenLabs.GetVoiceList(apiKey, OnReceivedVoiceList);
        }

        private void OnReceivedVoiceList(VoiceList voiceList)
        {
            IsAwaitingReply = false;
            this.voiceList = voiceList;
            if (voiceList == null)
            {
                voiceNames = null;
            }
            else
            {
                var currentActorVoiceID = Actor.LookupValue(DialogueSystemFields.VoiceID);
                voiceNames = new string[voiceList.voices.Count];
                for (int i = 0; i < voiceNames.Length; i++)
                {
                    voiceNames[i] = voiceList.voices[i].name;
                    if (voiceList.voices[i].voice_id == currentActorVoiceID) voiceIndex = i;
                }
            }
            Repaint();
        }

        private void PreviewSelectedVoice()
        {
            DestroyAudioClip();
            if (!(0 <= voiceIndex && voiceIndex < voiceList.voices.Count)) return;
            IsAwaitingReply = true;
            ProgressText = $"Retrieving preview sample for {voiceNames[voiceIndex]}.";
            var voiceData = voiceList.voices[voiceIndex];
            ElevenLabs.GetTextToSpeech(apiKey, voiceData.name, voiceData.voice_id, 0, 0, previewText, OnReceivedTextToSpeech);
        }

        private void OnReceivedTextToSpeech(AudioClip audioClip)
        {
            IsAwaitingReply = false;
            if (audioClip == null) return;
            this.audioClip = audioClip;
            Debug.Log($"Playing voice sample for {voiceNames[voiceIndex]}.");
            EditorAudioUtility.PlayAudioClip(audioClip);
        }

        private void DrawAcceptButton()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!(Actor != null && IsVoiceSelected));
            if (GUILayout.Button("Accept"))
            {
                Field.SetValue(Actor.fields, DialogueSystemFields.Voice, voiceList.voices[voiceIndex].name);
                Field.SetValue(Actor.fields, DialogueSystemFields.VoiceID, voiceList.voices[voiceIndex].voice_id);
                RefreshEditor();
                CloseWindow();
            }
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Cancel"))
            {
                CloseWindow();
            }
            EditorGUILayout.EndHorizontal();
        }

    }
}

#endif
