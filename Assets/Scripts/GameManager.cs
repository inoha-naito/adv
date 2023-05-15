using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

namespace NovelGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public ImageManager imageManager;
        public bool autoScrollEnabled;
        public bool recordingEnabled;

        [System.NonSerialized] public int lineNumber;
        [System.NonSerialized] public RecorderController recorderController;

        void Awake()
        {
            Instance = this;

            lineNumber = 0;

            if (recordingEnabled)
            {
                var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
                recorderController = new RecorderController(controllerSettings);

                var settings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
                settings.ImageInputSettings = new GameViewInputSettings
                {
                    OutputWidth = 1520,
                    OutputHeight = 720
                };
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                settings.OutputFile = Path.Combine(Application.dataPath, "..", "Recordings", timestamp);
                controllerSettings.AddRecorderSettings(settings);

                recorderController.PrepareRecording();
                recorderController.StartRecording();
            }
        }
    }
}
