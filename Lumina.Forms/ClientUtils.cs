// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Globalization;

namespace Lumina.Forms;

internal static class ClientUtils
{
    private enum CharType
    {
        None,
        Word,
        NonWord
    }

    // Matches the native SHAutoComplete backwards word-selection rule:
    // trailing non-word characters are included, connector punctuation is
    // non-word, and a UTF-16 surrogate terminates the backwards scan.
    internal static int GetWordBoundaryStart(string text, int endIndex)
    {
        bool seenWord = false;
        CharType lastSeen = CharType.None;
        int index = endIndex - 1;
        for (; index >= 0; index--)
        {
            char character = text[index];
            if (char.IsSurrogate(character))
            {
                break;
            }

            bool isWord = char.IsLetterOrDigit(character) ||
                CharUnicodeInfo.GetUnicodeCategory(character) == UnicodeCategory.NonSpacingMark;
            if ((isWord && lastSeen == CharType.NonWord && seenWord) ||
                (!isWord && lastSeen == CharType.Word && index != 0))
            {
                break;
            }

            seenWord |= isWord;
            lastSeen = isWord ? CharType.Word : CharType.NonWord;
        }

        return index + 1;
    }
}
