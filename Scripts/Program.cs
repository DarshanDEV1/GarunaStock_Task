using System;
using System.Net;
using UnityEngine;

public class Program : MonoBehaviour
{
    private const string GOOGLE_DRIVE_DOMAIN = "drive.google.com";
    private const string GOOGLE_DRIVE_DOMAIN2 = "https://drive.google.com/file/d/1-rsZL8FzUkJ_u0mamcv2BZH0dpeChkgt/view?usp=drive_link";

    private void Start()
    {
        DownloadFile(GOOGLE_DRIVE_DOMAIN2, @"C:\downloadedFile.json");
    }

    private void DownloadFile(string fileUrl, string savePath)
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFileCompleted += (sender, e) =>
            {
                if (e.Cancelled)
                    Debug.Log("Download cancelled");
                else if (e.Error != null)
                    Debug.Log("Download failed: " + e.Error);
                else
                    Debug.Log("Download completed");
            };

            Uri uri = new Uri(fileUrl);
            if (uri.Host == GOOGLE_DRIVE_DOMAIN || uri.Host == GOOGLE_DRIVE_DOMAIN2)
            {
                // Insert logic here to handle Google Drive links
                // You may need to follow a few redirects to get the actual file
                Debug.Log("Do Something");
            }
            else
            {
                client.DownloadFileAsync(uri, savePath);
            }
        }
    }
}
