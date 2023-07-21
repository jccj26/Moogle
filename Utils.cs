using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace MoogleEngine;

public class DFilesUtils
{
    public static string RemoveAccentsAndPuntuations(string inputString)
    {
        string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Content");

        foreach (string file in filePaths)
        {
            System.Console.WriteLine($"Reading file:{file}");

            StreamReader sr = new StreamReader(file);
            string Content = sr.ReadToEnd().ToLower();
            sr.Close();
        }
        return Regex.Replace(inputString.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9]+", "");
    }

    static public string[] TxtProcesser(string inputString)
    {
        inputString = inputString.ToLower();
        inputString = RemoveAccentsAndPuntuations(inputString);
        string[] split = inputString.Split(' ');

       

        List<string> almostWords = new List<string>();

        foreach (string word in split)
            if (word != "")
                almostWords.Add(word);

        string[] words = almostWords.ToArray();
        return words;

        
    }

}