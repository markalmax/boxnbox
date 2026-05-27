using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Audio
{
	public class AudioManager : MonoBehaviour
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
		public static void Play(string n)
		{
			soundDictionary[n].source.Play();
		}

		public static void Stop(string n)
		{
			soundDictionary[n].source.Stop();
		}
	}
}
