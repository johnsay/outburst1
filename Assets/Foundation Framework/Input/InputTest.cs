using UnityEngine;

namespace FoundationFramework.MultiplatformInput
{
    public class InputTest : MonoBehaviour 
    {
        private void Start ()
        {
            TouchManager.OnTouchDown+= OnFingerDown;
            TouchManager.OnTouch+= OnFingerSet;
            TouchManager.OnTouchUp+= OnFingerUp;
		
            TouchManager.OnSwipe+= OnFingerSwipe;
            TouchManager.OnTap+= OnFingerTap;
		
        }

        private void OnFingerTap(Finger finger)
        {
            print("OnFingerTap "+finger.StartedOverGui+finger.TapCount);
        }

        private void OnFingerSwipe(Finger finger)
        {
            print("OnFingerSwipe "+finger.SwipeScreenDelta);
        }

        private void OnFingerUp(Finger finger)
        {
            print("OnFingerUp");
        }

        private void OnFingerSet(Finger finger)
        {
            print("OnFingerSet"+finger.StartedOverGui);
        }

        private void OnFingerDown(Finger finger)
        {
            print("OnFingerDown");
        }


    }


}
