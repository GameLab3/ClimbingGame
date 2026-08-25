using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class SoundEnumGenerator
{
    private const string FilePath = "Assets/_Scripts/Sound/SoundID.cs";

    public static void GenerateSoundIDEnum(SoundDatabase soundDatabase)
    {
        if (!soundDatabase) return;
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("// Auto-generated audio enum. Do not modify directly.");
        sb.AppendLine("public enum SoundID");
        sb.AppendLine("{");
        sb.AppendLine("    None = 0,");

        HashSet<string> addedNames = new HashSet<string>();

        ProcessList(soundDatabase.sfxList, sb, addedNames);
        ProcessList(soundDatabase.musicList, sb, addedNames);

        sb.AppendLine("}");

        string directory = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(FilePath, sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log($"<color=green>SoundID enum generated at {FilePath}</color>");
    }

    private static void ProcessList(List<Sound> sounds, System.Text.StringBuilder sb, HashSet<string> addedNames)
    {
        foreach (Sound sound in sounds)
        {
            if (sound == null || string.IsNullOrWhiteSpace(sound.name)) continue;

            // Strip spaces and special characters into valid C# identifier
            string cleanName = Regex.Replace(sound.name, @"[^a-zA-Z0-9_]", "_");
            if (char.IsDigit(cleanName[0])) cleanName = "_" + cleanName;

            if (addedNames.Add(cleanName))
            {
                sb.AppendLine($"    {cleanName},");
            }
        }
    }
}
