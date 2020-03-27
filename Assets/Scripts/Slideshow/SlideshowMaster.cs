using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Slideshow
{
    public class SlideshowMaster : MonoBehaviour
    {
        public float lockTime = 0.5f;
        private float _timer;
        public string nextSceneName = "NULL";
    
        //Counting on that each object has a SlideObject component attached
        private int _currentSlideIndex = 0;
        public List<GameObject> slides = new List<GameObject>();

        private void Start()
        {
            foreach (var slide in slides)
            {
                slide.SetActive(false);
            }
            slides[0].SetActive(true);
            
            _timer = lockTime;
        }

        private void OnNewSlide(GameObject slide)
        {
            slide.SetActive(true);
            foreach (var action in slide.GetComponent<SlideObject>().immediateActions)
            {
                action.OnAction();
            }
        }

        private void Update()
        {
            if (_timer <= 0 && Input.anyKeyDown)
            {
                if (slides[_currentSlideIndex] != null && !slides[_currentSlideIndex].GetComponent<SlideObject>().TickSlide())
                {
                    SlideExit(slides[_currentSlideIndex]);
                }
                _timer = lockTime;
            }
            else if (_timer > 0)
                _timer -= Time.deltaTime;
        }

        private void SlideExit(GameObject slide)
        {
            if (_currentSlideIndex >= slides.Count - 1)
                ShowcaseExit();
            else
            {
                Debug.Log("Switched  Slide");
                slide.SetActive(false);
                _currentSlideIndex++;
                OnNewSlide(slides[_currentSlideIndex]);   
            }
        }

        private void ShowcaseExit()
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

