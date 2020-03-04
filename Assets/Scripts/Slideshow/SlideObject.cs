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
