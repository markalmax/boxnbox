using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.Netcode;

namespace Audio
{
	public class AudioManager : NetworkBehaviour
	{
		public Sound[] sounds;
		public static AudioManager instance;
		private static Dictionary<string, Sound> soundDictionary;

		private void Awake()
		{
			instance = this;
			soundDictionary = new Dictionary<string, Sound>();
			Sound[] array = sounds;
			foreach (Sound sound in array)
			{
				if (sound.name ==null)sound.name = sound.clip.name;
				sound.source = base.gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.loop = sound.loop;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
				soundDictionary.Add(sound.name, sound);
			}
		}

		public void MuteMusic()
		{
			soundDictionary["Song"].source.volume = 0f;
		}

		public void UnmuteMusic()
		{
			soundDictionary["Song"].source.volume = 1.15f;
		}
		[ClientRpc]
		public void PlayClientRpc(string n)
		{
			soundDictionary[n].source.Play();
		}
		[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
		public void PlayServerRpc(string n)
		{
			if (IsServer)
			{
				PlayClientRpc(n);
			}
		}
		[ClientRpc]
		public void StopClientRpc(string n)
		{
			soundDictionary[n].source.Stop();
		}
		[Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
		public void StopServerRpc(string n)
		{
			if (IsServer)
			{
				StopClientRpc(n);
			}
		}
	}
}
