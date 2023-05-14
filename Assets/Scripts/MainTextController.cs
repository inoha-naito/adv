using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NovelGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;

        // Start is called before the first frame update
        void Start()
        {
            DisplayText();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                GoToTheNextLine();
                DisplayText();
            }
        }

        public void GoToTheNextLine()
        {
            int count = GameManager.Instance.userScriptManager.GetSentencesCount();
            if (GameManager.Instance.lineNumber < count - 1)
            {
                GameManager.Instance.lineNumber++;
            }
        }

        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _mainTextObject.text = sentence;
        }
    }
}