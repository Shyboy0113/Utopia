// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace PixelCrushers.DialogueSystem.OpenAIAddon.ElevenLabs
{

    /// <summary>
    /// Handles web requests to ElevenLabs API.
    /// </summary>
    public static class ElevenLabs
    {

        public const string VoiceListURL = "https://api.elevenlabs.io/v1/voices";
        public const string TextToSpeechURL = "https://api.elevenlabs.io/v1/text-to-speech";

        public static bool IsApiKeyValid(string apiKey)
        {
            return !string.IsNullOrEmpty(apiKey);
        }

        /// <summary>
        /// Gets a list of all available voices for a user.
        /// </summary>
        /// <param name="apiKey">ElevenLabs API key.</param>
        /// <param name="callback">List of voices.</param>
        /// <returns></returns>
        public static UnityWebRequestAsyncOperation GetVoiceList(string apiKey, Action<VoiceList> callback)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(VoiceListURL + "?token=" + apiKey);
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;

            UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

            asyncOp.completed += (op) =>
            {
                var success = webRequest.result == UnityWebRequest.Result.Success;
                var text = success ? webRequest.downloadHandler.text : string.Empty;
                if (!success) Debug.Log($"{webRequest.error}\n{webRequest.downloadHandler.text}");
                webRequest.Dispose();
                webRequest = null;

                VoiceList voiceList = null;
                if (!string.IsNullOrEmpty(text))
                {
                    voiceList = JsonUtility.FromJson<VoiceList>(text);
                }
                callback?.Invoke(voiceList);
            };

            return asyncOp;
        }

        /// <summary>
        /// Converts text into speech using a voice of your choice and returns audio.
        /// </summary>
        /// <param name="apiKey">ElevenLabs API key.</param>
        /// <param name="voice_name">Name of voice to use.</param>
        /// <param name="voice_id">ID of voice to use.</param>
        /// <param name="stability"></param>
        /// <param name="similarity_boost"></param>
        /// <param name="text">Text to convert to speech audio.</param>
        /// <param name="callback">Resulting audio clip.</param>
        /// <returns></returns>
        public static UnityWebRequestAsyncOperation GetTextToSpeech(string apiKey, string voice_name, string voice_id,
            float stability, float similarity_boost, string text, Action<AudioClip> callback)
        {
            var url = $"{TextToSpeechURL}/{voice_id}";

            var ttsRequest = new TextToSpeechRequest(text, new VoiceSettings(stability, similarity_boost));
            string jsonData = JsonUtility.ToJson(ttsRequest);

            byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

#if UNITY_2022_2_OR_NEWER
            UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, jsonData);
#else
            UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonData);
#endif
            webRequest.uploadHandler.Dispose();
            webRequest.uploadHandler = new UploadHandlerRaw(postData);
            webRequest.downloadHandler.Dispose();
            webRequest.downloadHandler = new DownloadHandlerAudioClip(url, AudioType.MPEG);
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("xi-api-key", apiKey);

            UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

            asyncOp.completed += (op) =>
            {
                AudioClip audioClip = null;
                var success = webRequest.result == UnityWebRequest.Result.Success;
                if (success)
                {
                    audioClip = (webRequest.downloadHandler as DownloadHandlerAudioClip).audioClip;
                }
                else
                {
                    Debug.Log($"{webRequest.error}");
                }
                webRequest.Dispose();
                webRequest = null;

                callback?.Invoke(audioClip);
            };

            return asyncOp;
        }

    }
}

#endif
