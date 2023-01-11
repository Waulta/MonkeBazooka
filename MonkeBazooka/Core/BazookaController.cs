using UnityEngine;
using UnityEngine.XR;
using MonkeBazooka.Utils;

public enum BazookaState
{
    Unprimed,
    Primed,
}

namespace MonkeBazooka.Core
{
    public class BazookaController : MonoBehaviour
    {
        public static float MissileSpeed = 0.8f;
        public static float ExplosionRadius = 6f;

        public XRNode LNode = XRNode.LeftHand;
        public XRNode RNode = XRNode.RightHand;

        public bool RightFiredOnce = false;
        public bool LeftFiredOnce = false;
        
        public float LeftTriggerValue;
        public float RightTriggerValue;
        
        public bool AButtonDown = false;
        public bool XButtonDown = false;

        public float HapticDuration = 0.3f;
        public float HapticStrength = 2.0f;

        public static Vector3 missileSize = new Vector3(0.35f, 0.075f, 0.075f);


        public static BazookaState MyState = BazookaState.Primed;

        void FixedUpdate()
        {
 
            if (MBConfig.Left)
            {
                if (InputDevices.GetDeviceAtXRNode(XRNode.Head).name.Contains("Oculus"))
                {
                    InputDevices.GetDeviceAtXRNode(LNode).TryGetFeatureValue(CommonUsages.trigger, out LeftTriggerValue);
                    InputDevices.GetDeviceAtXRNode(LNode).TryGetFeatureValue(CommonUsages.secondaryButton, out XButtonDown);
                }
                else
                {
                    InputDevices.GetDeviceAtXRNode(LNode).TryGetFeatureValue(CommonUsages.trigger, out LeftTriggerValue);
                    InputDevices.GetDeviceAtXRNode(LNode).TryGetFeatureValue(CommonUsages.primaryButton, out XButtonDown);
                }
                switch (MyState)
                {
                    case BazookaState.Unprimed:
                        if (!XButtonDown && LeftTriggerValue < 0.5f && !LeftFiredOnce)
                        {
                            LeftFiredOnce = true;
                        }
                        if (!XButtonDown || !LeftFiredOnce) return;
                        LeftFiredOnce = false;
                        PrimeMissle();
                        break;
                    case BazookaState.Primed:
                        if (LeftTriggerValue < 0.5f) return;
                        FireMissile();
                        MyState = BazookaState.Unprimed;
                        break;
                }
            }
            else
            {
                if (InputDevices.GetDeviceAtXRNode(XRNode.Head).name.Contains("Oculus"))
                {
                    InputDevices.GetDeviceAtXRNode(RNode).TryGetFeatureValue(CommonUsages.trigger, out RightTriggerValue);
                    InputDevices.GetDeviceAtXRNode(RNode).TryGetFeatureValue(CommonUsages.secondaryButton, out AButtonDown);
                }
                else
                {
                    InputDevices.GetDeviceAtXRNode(RNode).TryGetFeatureValue(CommonUsages.trigger, out RightTriggerValue);
                    InputDevices.GetDeviceAtXRNode(RNode).TryGetFeatureValue(CommonUsages.primaryButton, out AButtonDown);
                }
                switch (MyState)
                {
                    case BazookaState.Unprimed:
                        if (!AButtonDown && RightTriggerValue < 0.5f && !RightFiredOnce)
                        {
                            RightFiredOnce = true;
                        }
                        if (!AButtonDown || !RightFiredOnce) return;
                        RightFiredOnce = false;
                        PrimeMissle();
                        break;
                    case BazookaState.Primed:
                        if (RightTriggerValue < 0.5f) return;
                        FireMissile();
                        MyState = BazookaState.Unprimed;
                        break;
                }
            }
        }

        private void FireMissile()
        {
            GameObject ClonedMissile = Instantiate<GameObject>(MBUtils.MissilePrefab, MBUtils.FakeMissile.transform.position, MBUtils.FakeMissile.transform.rotation);
            MBUtils.FakeMissile.SetActive(false);
            ClonedMissile.AddComponent<MissileController>();

            if(MBConfig.Left)
            {
                ClonedMissile.transform.localScale = new Vector3(ClonedMissile.transform.localScale.x, ClonedMissile.transform.localScale.y, ClonedMissile.transform.localScale.z * -1);
                GorillaTagger.Instance.StartVibration(true, HapticStrength, HapticDuration);
            }
            else
            {
                GorillaTagger.Instance.StartVibration(false, HapticStrength, HapticDuration);
            }
        }

        private void PrimeMissle()
        {
            AudioSource BazookaSpeaker = GetComponentInChildren<AudioSource>();
            BazookaSpeaker.PlayOneShot(BazookaSpeaker.clip);
            MBUtils.FakeMissile.SetActive(true);
            MyState = BazookaState.Primed;
        }
    }
}
