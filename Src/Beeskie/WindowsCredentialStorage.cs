using System;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using BlueskyClient.Services;
using Windows.Storage;
using System.Diagnostics;

namespace BlueskyClient
{
    public class WindowsCredentialStorage : ISecureCredentialStorage
    {
        private static ApplicationDataContainer localSettings;
        private static StorageFolder localFolder;


        private string fname;

        public WindowsCredentialStorage()
        {
            //Init
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        }

        public WindowsCredentialStorage(string fname)
        {
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            this.fname = fname;
        }

        public string GetCredential(string id)
        {
            if (localSettings != null && localFolder != null)
            {
                try
                {
                    return (string)localSettings.Values[id];
                }
                catch
                {
                    Debug.WriteLine("No save data located");
                };
            }
            else DebugWarnNotInitialised();

            return null;

        }

        public void SetCredential(string id, string v)
        {
            if (localSettings != null && localFolder != null)
            {
                localSettings.Values[id] = v;
            }
            else DebugWarnNotInitialised();

        }

        private static void DebugWarnNotInitialised()
        {
            Debug.WriteLine(@"Have you called ""StorageManager.Init()""?");
        }

    }
}