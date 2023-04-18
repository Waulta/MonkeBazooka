using System.IO;
using System.Reflection;
using UnityEngine;
using Photon.Pun;
using MonkeBazooka.Utils;

namespace MonkeBazooka.Core
{
	public class BazookaManager : MonoBehaviourPunCallbacks
	{
		public static BazookaManager Instance { get; private set; }

		public bool initialized = false;

		internal void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(this);
			else
				Instance = this;

			if (!initialized)
				Initialize();
		}

		public void Toggle(bool thing)
		{
			if (MBConfig.Modded)
			{
				switch (thing)
				{
					case true:
						MBUtils.Bazooka.SetActive(true);
						MBUtils.BazookaController.enabled = true;
						UpdateHand();
						break;

					case false:
						UpdateHand();
						MBUtils.Bazooka.SetActive(false);
						MBUtils.BazookaController.enabled = false;
						break;
				}
			}
			else
			{
				UpdateHand();
				MBUtils.Bazooka.SetActive(false);
				MBUtils.BazookaController.enabled = false;
			}
		}

		public void UpdateEnabled()
		{
			MBConfig.Enabled = !MBConfig.Enabled;
			Toggle(MBConfig.Enabled);
		}

		public void UpdateHandState()
		{
			MBConfig.Left = !MBConfig.Left;
			Toggle(MBConfig.Enabled);
		}

		public void UpdateForce(bool isPlus)
		{
			switch (isPlus)
			{
				case true:
					if (MBConfig.ExplosionForce >= 15) break;
					MBConfig.ExplosionForce += 0.5f;
					break;
				case false:
					if (MBConfig.ExplosionForce <= 0) break;
					MBConfig.ExplosionForce -= 0.5f;
					break;
			}

			MBConfig.ExplosionForce = Mathf.Round(MBConfig.ExplosionForce * 10.0f) * 0.1f;
		}

		public void UpdateHand()
		{
			Transform BazookaTransform = MBUtils.Bazooka.transform;

			if (MBConfig.Left)
			{
				BazookaTransform.SetParent(MBUtils.LeftHandTransform, false);
				BazookaTransform.localPosition = new Vector3(0.005f, 0.015f, 0.01f);
				BazookaTransform.localScale = new Vector3(0.2f, 0.2f, -0.2f);
				BazookaTransform.localEulerAngles = new Vector3(352.0f, 353.0f, 300.2845f);
			}
			else
			{
				BazookaTransform.SetParent(MBUtils.RightHandTransform, false);
				BazookaTransform.localPosition = new Vector3(-0.015f, 0.015f, -0.0125f);
				BazookaTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				BazookaTransform.localEulerAngles = new Vector3(8.4124f, 83.1033f, 300.2845f);
			}
		}

		public void Initialize()
		{
			Debug.Log("Initializing MonkeBazooka...");

			if (!initialized)
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeBazooka.Resources.launcher");
				AssetBundle assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);

				MBUtils.BazookaPrefab = assetBundle.LoadAsset("Launcher") as GameObject;
				MBUtils.MissilePrefab = assetBundle.LoadAsset("realMissile") as GameObject;
				MBUtils.ExplosionPrefab = assetBundle.LoadAsset("explosion") as GameObject;

				MBUtils.Bazooka = Object.Instantiate(MBUtils.BazookaPrefab);
				MBUtils.BazookaController = MBUtils.Bazooka.AddComponent<BazookaController>();

				assetBundle.Unload(false);

				if (!MBUtils.LeftHandTransform)
					MBUtils.GetVariables();

				if (MBUtils.Bazooka != null)
				{
					Toggle(MBConfig.Enabled);
					initialized = true;

					Debug.Log("Successfully initialized MonkeBazooka.");
				}
				else
				{
					initialized = false;

					Debug.Log("Failed to initialize MonkeBazooka.");
				}
			}
			else
			{
				if (MBUtils.Bazooka != null) Toggle(MBConfig.Enabled);

				Debug.Log($"MonkeBazooka already initialized setting Bazooka to {(MBConfig.Enabled ? "active." : "disabled.")}");
			}
		}
	}
}