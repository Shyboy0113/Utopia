// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    /// <summary>
    /// Actions that the OpenAI addon can handle:
    /// </summary>
    public enum AIRequestType
    {
        ConfigureApi,
        General,
        GenerateConversation,
        ExtendConversation,
        ReviseText,
        LocalizeField,
        LocalizeDatabase,
        Freeform,

        // DALL-E
        GeneratePortraits,

        // ElevenLabs
        SelectVoice,
        GenerateVoice,

        // Dialogue Smith
        BranchingDialogue,

        OriginalRequest
    }

}

#endif