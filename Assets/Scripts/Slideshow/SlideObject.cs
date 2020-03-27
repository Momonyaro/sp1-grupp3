using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slideshow
{
    public class SlideObject : MonoBehaviour
    {
        private int _currentActionIndex = 0;
        public List<Action> immediateActions = new List<Action>();
        public List<Action> actions = new List<Action>();

        public bool TickSlide()
        {
            for (int i = 0; i < immediateActions.Count; i++)
            {
                immediateActions[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < _currentActionIndex; i++)
            {
                actions[i].gameObject.SetActive(false);
            }

            if (_currentActionIndex >= actions.Count) return false;
            
            actions[_currentActionIndex].OnAction();
            _currentActionIndex++;
            return true;
        }
    }

    public abstract class Action : MonoBehaviour
    {
        [SerializeField] public List<Action> siblings = new List<Action>();
        
        public abstract void OnAction();
    }
}
