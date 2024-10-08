using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SubtitleEntry
{
    public float duration;
    public string text;
}

public class SubtitleManager : MonoBehaviour
{
    public bool showSubtitles = false; 
    public TMP_Text subtitleText; 
    public List<SubtitleEntry> subtitles = new List<SubtitleEntry>(); 

    private float displayEndTime; 
    private int currentSubtitleIndex = -1; 

    void Start()
    {
        subtitleText.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (showSubtitles)
        {
            float timer = Time.timeSinceLevelLoad;

            if (currentSubtitleIndex < subtitles.Count)
            {
                if (currentSubtitleIndex >= 0)
                {
                    if (timer >= displayEndTime)
                    {
                        HideSubtitle(); 
                    }
                }

                if (currentSubtitleIndex + 1 < subtitles.Count && timer >= displayEndTime)
                {
                    currentSubtitleIndex++;
                    var nextSubtitle = subtitles[currentSubtitleIndex];
                    DisplaySubtitle(nextSubtitle.text, nextSubtitle.duration); 
                }
            }
        }
        else
        {
            HideSubtitle();
        }
    }

    public void DisplaySubtitle(string text, float duration)
    {
        subtitleText.text = text;
        subtitleText.gameObject.SetActive(true);
        displayEndTime = Time.timeSinceLevelLoad + duration; 
    }

    public void HideSubtitle()
    {
        subtitleText.gameObject.SetActive(false);
    }

    public void LoadSubtitlesFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string timePart = "";
        string subtitleText = "";

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();

            if (trimmedLine.StartsWith("[DURATION=\"") && trimmedLine.EndsWith("\"]"))
            {
                try
                {
                    timePart = trimmedLine.Replace("[DURATION=\"", "").Replace("\"]", "").Trim();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Failed to parse time: " + trimmedLine + " Error: " + ex.Message);
                }
            }
            else if (trimmedLine.StartsWith("[SUBTITLE=\"") && trimmedLine.EndsWith("\"]"))
            {
                try
                {
                    subtitleText = trimmedLine.Replace("[SUBTITLE=\"", "").Replace("\"]", "").Trim();

                    if (!string.IsNullOrEmpty(timePart) && !string.IsNullOrEmpty(subtitleText))
                    {
                        float durationInSeconds = ParseTimeToSeconds(timePart);

                        if (!SubtitleExists(durationInSeconds, subtitleText))
                        {
                            subtitles.Add(new SubtitleEntry { duration = durationInSeconds, text = subtitleText });
                            Debug.Log($"Subtitle added: {durationInSeconds}s - {subtitleText}");
                        }
                        else
                        {
                            Debug.Log($"Subtitle already exists: {durationInSeconds}s - {subtitleText}");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Failed to parse subtitle: " + trimmedLine + " Error: " + ex.Message);
                }
            }
            else if (!string.IsNullOrEmpty(trimmedLine))
            {
                Debug.LogWarning("Unrecognized format in line: " + trimmedLine);
            }
        }

        Debug.Log("Subtitles loaded: " + subtitles.Count);
    }

    private bool SubtitleExists(float duration, string text)
    {
        foreach (var entry in subtitles)
        {
            if (entry.duration == duration && entry.text == text)
            {
                return true; 
            }
        }
        return false; 
    }

    private float ParseTimeToSeconds(string time)
    {
        string[] timeParts = time.Split(':');
        if (timeParts.Length == 2)
        {
            int minutes = int.Parse(timeParts[0]);
            int seconds = int.Parse(timeParts[1]);
            return minutes * 60 + seconds;
        }
        else
        {
            Debug.LogError("Time format is incorrect: " + time);
            return 0;
        }
    }
}
