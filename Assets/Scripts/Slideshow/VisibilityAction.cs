using System.Collections.Generic;
using UnityEngine;

namespace Slideshow
{
    public class VisibilityAction : Action
    {
        public bool visible = true;

        public override void OnAction()
        {
            gameObject.SetActive(visible);
            
            foreach (var sibling in siblings)
            {
                sibling.OnAction();
            }
        }
    }
}
