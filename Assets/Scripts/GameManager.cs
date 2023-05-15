using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public ImageManager imageManager;
        public bool autoScrollEnabled;

        [System.NonSerialized] public int lineNumber;

        void Awake()
        {
            Instance = this;

            lineNumber = 0;
        }
    }
}
