using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace NovelGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;
        int _displayedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;
        float _waitTime;

        // Start is called before the first frame update
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;
            _waitTime = 0.5f;

            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
                GoToTheNextLine();
            }
            DisplayText();
        }

        // Update is called once per frame
        void Update()
        {
            _time += Time.deltaTime;
            if (!CanGoToTheNextLine())
            {
                if (_time >= _feedTime)
                {
                    _time -= _feedTime;
                    _displayedSentenceLength++;
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                }
            }
            else
            {
                if (_time >= _waitTime)
                {
                    _time = _waitTime + Time.deltaTime;
                }
            }

            if (GameManager.Instance.autoScrollEnabled)
            {
                if (CanGoToTheNextLine() && _time > _waitTime)
                {
                    int count = GameManager.Instance.userScriptManager.GetSentencesCount();
                    if (GameManager.Instance.lineNumber < count - 1)
                    {
                        GoToTheNextLine();
                        DisplayText();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (CanGoToTheNextLine() && _time > _waitTime)
                    {
                        int count = GameManager.Instance.userScriptManager.GetSentencesCount();
                        if (GameManager.Instance.lineNumber < count - 1)
                        {
                            GoToTheNextLine();
                            DisplayText();
                        }
                    }
                    else
                    {
                        _displayedSentenceLength = _sentenceLength;
                    }
                }
            }

            if (GameManager.Instance.recordingEnabled)
            {
                if (GameManager.Instance.recorderController != null)
                {
                    if (CanGoToTheNextLine() && _time > _waitTime)
                    {
                        int count = GameManager.Instance.userScriptManager.GetSentencesCount();
                        if (GameManager.Instance.lineNumber == count - 1)
                        {
                            GameManager.Instance.recorderController.StopRecording();
                            EditorApplication.isPlaying = false;
                        }
                    }
                }
            }
        }

        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _sentenceLength = sentence.Length;
            return _displayedSentenceLength > sentence.Length;
        }

        public void GoToTheNextLine()
        {
            _displayedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
                GoToTheNextLine();
            }
        }

        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _mainTextObject.text = sentence;
        }
    }
}
