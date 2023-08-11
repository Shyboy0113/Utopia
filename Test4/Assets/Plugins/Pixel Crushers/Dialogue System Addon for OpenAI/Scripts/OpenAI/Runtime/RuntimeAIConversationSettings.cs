// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using UnityEngine;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    public enum ImageSizes { Size256x256, Size512x512, Size1024x1024 }

    // Add to dialogue UI.
    public class RuntimeAIConversationSettings : MonoBehaviour
    {

        public static RuntimeAIConversationSettings Instance { get; private set; }

        [Header("OpenAI Settings")]
        [SerializeField] private string apiKey;
        [SerializeField] private TextModelName textModelName = TextModelName.GPT3_5_Turbo;
        [SerializeField] private float temperature = 0.4f;
        [SerializeField] private int maxTokens = 4097;

        [Header("ElevenLabs Settings")]
        [Tooltip("If you want to generate text to speech, set your ElevenLabs API key here.")]
        [SerializeField] private string elevenLabsApiKey;

        [Header("UI Elements")]
        [Tooltip("Runtime conversations show this icon while waiting for OpenAI responses.")]
        [SerializeField] private GameObject waitingIcon;
        [Tooltip("Input field used for freeform text input conversations.")]
        [SerializeField] private StandardUIInputField chatInputField;
        [Tooltip("This button ends freeform text input conversations.")]
        [SerializeField] private Button goodbyeButton;
        [Tooltip("Shows generated images when playing CYOA (Choose Your Own Adventure) conversations.")]
        [SerializeField] private Image image;
        [SerializeField] private ImageSizes imageSize = ImageSizes.Size256x256;
        [Tooltip("If you want to allow speech input, this button starts recording user's speech.")]
        [SerializeField] private Button recordButton;
        [Tooltip("This button stops recording and submits it to OpenAI for text transcription.")]
        [SerializeField] private Button submitRecordingButton;
        [Tooltip("Optional dropdown for microphone input selection.")]
        [SerializeField] private UIDropdownField microphoneDevicesDropdown;
        [Tooltip("If recording audio, record up to this many seconds.")]
        [SerializeField] private int maxRecordingLength = 10;
        [Tooltip("If recording audio, record at this frequency.")]
        [SerializeField] private int recordingFrequency = 44100;

        public GameObject WaitingIcon => waitingIcon;
        public StandardUIInputField ChatInputField => chatInputField;
        public Button GoodbyeButton => goodbyeButton;
        public Image Image => image;
        public Button RecordButton => recordButton;
        public Button SubmitRecordingButton => submitRecordingButton;
        public UIDropdownField MicrophoneDevicesDropdown => microphoneDevicesDropdown;
        public int MaxRecordingLength => maxRecordingLength;
        public int RecordingFrequency => recordingFrequency;

        public string APIKey { get => apiKey; set => apiKey = value; }
        public Model Model => OpenAI.NameToModel(textModelName);
        public bool IsChatModel => Model.ModelType == ModelType.Chat;
        public float Temperature { get => temperature; set => temperature = value; }
        public int MaxTokens { get => maxTokens; set => maxTokens = value; }
        public string ElevenLabsApiKey { get => elevenLabsApiKey; set => elevenLabsApiKey = value; }

        public string ImageSizeString
        {
            get
            {
                switch (imageSize)
                {
                    default:
                    case ImageSizes.Size256x256: return "256x256";
                    case ImageSizes.Size512x512: return "512x512";
                    case ImageSizes.Size1024x1024: return "1024x1024";
                }
            }
        }

        public int ImageSizeValue
        {
            get
            {
                switch (imageSize)
                {
                    default:
                    case ImageSizes.Size256x256: return 256;
                    case ImageSizes.Size512x512: return 512;
                    case ImageSizes.Size1024x1024: return 1024;
                }
            }
        }

        private void Awake()
        {
            Instance = this;
            HideExtraUIElements();
        }

        private void Start()
        {
            var dialogueUI = GetComponent<StandardDialogueUI>();
            if (dialogueUI == null) return;
            if (dialogueUI.conversationUIElements.mainPanel != null)
            {
                dialogueUI.conversationUIElements.mainPanel.onClose.AddListener(HideExtraUIElements);
            }
        }

        private void HideExtraUIElements()
        {
            if (waitingIcon != null) waitingIcon.SetActive(false);
            if (goodbyeButton != null) goodbyeButton.gameObject.SetActive(false);
        }

    }
}

#endif

