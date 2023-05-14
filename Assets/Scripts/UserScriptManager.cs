using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace NovelGame
{
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile;

        List<string> _sentences = new List<string>();

        void Awake()
        {
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }
        
        public int GetSentencesCount()
        {
            return _sentences.Count;
        }
    }
}
