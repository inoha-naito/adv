using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;

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
                controllerSettings.AddRecorderSettings(settings);

                recorderController.PrepareRecording();
                recorderController.StartRecording();
            }
        }
    }
}
