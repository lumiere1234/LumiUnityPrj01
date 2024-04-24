using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LumiAudio
{
    public class AudioSourcePool : SingletonAutoMono<AudioSourcePool>
    {
        private Stack<AudioSource> mAudioSources;
        public int mFreeCount { get => mAudioSources.Count; }
        private AudioInfo defaultInfo;

        private void Awake()
        {
            mAudioSources = new Stack<AudioSource>();
            defaultInfo = new AudioInfo();
        }
        public AudioSource GetAudioSource()
        {
            return DoGetAudioSource();
        }
        public AudioSource GetAudioSourceByInfo(AudioInfo info)
        {
            return DoGetAudioSourceWithInfo(info);
        }
        private AudioSource DoGetAudioSource()
        {
            AudioSource retSource = null;
            if (mFreeCount == 0) {
                GenerateAudioSource();
            }
            retSource = mAudioSources.Pop();
            return retSource;
        }
        private AudioSource DoGetAudioSourceWithInfo(AudioInfo info) 
        {
            AudioSource retSource = DoGetAudioSource();
            info?.ShareToSource(retSource);
            return retSource;
        }
        private void GenerateAudioSource()
        {
            var audioSource = AudioMgr.Instance.gameObject.AddComponent<AudioSource>();
            mAudioSources.Push(audioSource);
        }
        public void ReturnPool(AudioSource source) 
        {
            if (source != null) 
            {
                CleanAudioSource(source);
                mAudioSources.Push(source);
            }
        }
        public void CleanAudioSource(AudioSource source)
        {
            if (source != null) 
            {
                 defaultInfo.ShareToSource(source);
            }
        }
    }
}
