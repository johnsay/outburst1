﻿using UnityEngine;
using System.Collections.Generic;

namespace FoundationFramework.Audio
{
	[CreateAssetMenu(fileName = "SoundConfig", menuName = "Foundation/Audio/Create_SoundConfig", order = 1)]
	public class SoundConfig : ScriptableObject 
	{
		[Header("Parameters:")]
		[Range(0f, 1f)]public float Volume = 1f;
		public float MinPitch = 1f;
		public float MaxPitch = 1f;
		public float DelayAtStart;
		public float FadeInTime;
		public float FadeOutTime;
		public bool Looping;
		public int PoolSize; 
		[Header("Components:")]
		public List<AudioClip> Clips = new List<AudioClip>();
		public AudioSource Source;
		public AudioClip RandomizedClip(){ return Clips.RandomItem(); }
	    public float RandomizedPitch(){ return Random.Range(MinPitch, MaxPitch); }
	}
}