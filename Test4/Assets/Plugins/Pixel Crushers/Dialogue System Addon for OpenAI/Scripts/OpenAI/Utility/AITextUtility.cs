// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using System.Globalization;

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    /// <summary>
    /// Utility methods for OpenAI addon.
    /// </summary>
    public static class AITextUtility
    {

        /// <summary>
        /// Gets English language name from a language code. 
        /// If can't determine English name, returns language code itself.
        /// </summary>
        public static string DetermineLanguage(string languageCode)
        {
            var cultureInfo = new CultureInfo(languageCode);
            return (cultureInfo == null || string.IsNullOrEmpty(cultureInfo.Name)) ? languageCode : cultureInfo.EnglishName;
        }

        /// <summary>
        /// Removes "speaker:", "speaker says:", or surrounding quotes around lines.
        /// </summary>
        public static string RemoveSpeaker(string speaker, string line)
        {
            if (line.StartsWith($"{speaker}:"))
            {
                return RemoveSurroundingQuotes(line.Substring(speaker.Length + 2));
            }
            else if (line.StartsWith($"{speaker} says: "))
            {
                return RemoveSurroundingQuotes(line.Substring(speaker.Length + 7));
            }
            else if (line.Contains(": \""))
            {
                var pos = line.IndexOf(": \"");
                return RemoveSurroundingQuotes(line.Substring(pos + 2));
            }
            else
            {
                return line;
            }
        }

        /// <summary>
        /// Removes double quotes around string if present.
        /// </summary>
        public static string RemoveSurroundingQuotes(string text)
        {
            if (text.StartsWith("\""))
            {
                return text.Substring(1, text.Length - 2);
            }
            else
            {
                return text;
            }
        }

        public static string DoubleQuotesToSingle(string text)
        {
            return text.Replace('"', '\'');
        }

    }

}

#endif
