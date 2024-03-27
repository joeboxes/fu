using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalData
{
   public string DataPath
   {
      get
      {
         return Application.persistentDataPath + "/LocalData/data.yaml"; // json / yaml
      }
   }
   public bool saveBool(string key, string value)
   {
      // PlayerPrefs.SetInt(key, 3);
      // PlayerPrefs.Save();
      return false;
   }

   public void Save()
   {
      string relPath = DataPath;
      WriteStringToFile("data", relPath, this.SaveComplete);

   }
   
   public void Load()
   {
      string relPath = DataPath;
      ReadStringFromFile("data", relPath, LoadComplete);

   }

   public void SaveComplete(string path, bool success)
   {
      Debug.Log($"save complete: {path} - {success}");
   }
   public void LoadComplete(string path, bool success)
   {
      Debug.Log($"save complete: {path} - {success}");
   }
   
   
   
   public delegate void WriteCompleteHandler(System.Object data, System.Object context);
   
   //public static void WriteStringToFile(string data, string path, WriteCompleteHandler completeHandler)
   public static void WriteStringToFile(string data, string path, Action<string, bool> completeHandler)
   {
      // completeHandler(path, true);
      completeHandler.Invoke(path, true);
   }
   
   public delegate void ReadCompleteHandler(System.Object data, System.Object context);
   //public static void ReadStringFromFile(string data, string path, ReadCompleteHandler completeHandler)
   public static void ReadStringFromFile(string data, string path, Action<string, bool> completeHandler)
   {
      completeHandler.Invoke(path, true);
      //completeHandler(path, true);
   }
}
