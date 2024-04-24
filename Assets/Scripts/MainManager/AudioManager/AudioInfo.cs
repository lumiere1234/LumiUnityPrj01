using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LumiAudio
{
    public class AudioInfo
    {
        public AudioClip mAudioClip { get; private set; }
        public string mAudioName { get; private set; } = string.Empty;
        public AudioSource mAudioSource { get; set; }
        private float _volume = 1.0f;
        public float mVolume { get { return _volume; } set { _volume = Mathf.Clamp(value, 0, 1); } } // 播放音量
        private float _pitch = 1.0f;
        public float mPitch { get { return _pitch; } set { _pitch = Mathf.Clamp(value, -3, 3); } } // 播放速度
        private int _priority = 128;
        public int mPriority { get { return _priority; } set { _priority = Mathf.Clamp(value, 0, 256); } }
        public bool bLoop { get; set; } = false;
        public bool bIgnoreTotalVolume { get; set; } = false;
        public bool bPlayOnAwake { get; set; } = true;
        private float _stereoPan = 1; // 调整左右声道
        public float mStereoPan { get { return _stereoPan; } set { _stereoPan = Mathf.Clamp(value, -1, 1); } }

        // 立体声过度属性
        public bool bStereoTransition { get; set; } = false;
        public float[] mStereoTransitionValues { get; set; }
        private float _stereoTransitionTimeSpan = 0.5f;
        public float mStereoTransitionTimeSpan 
        { 
            get => _stereoTransitionTimeSpan;
            set => _stereoTransitionTimeSpan = Mathf.Clamp(value, 0.1f, 5f);
        }
        //private float actualVolume; // 实际音量
        public EAudioInfoType mInfoType = EAudioInfoType.DEFAULT;

        public AudioInfo()
        {
            InitAudioInfo(EAudioInfoType.DEFAULT);
        }
        public AudioInfo(string audioName, AudioClip clip, EAudioInfoType eType = EAudioInfoType.DEFAULT)
        {
            mInfoType = eType;
            InitAudioInfo(eType);
            this.mAudioName = audioName;
            this.mAudioClip = clip;
        }
        private void InitAudioInfo(EAudioInfoType eType)
        {
            mAudioClip = null;
            mAudioName = "Audio";
            mVolume = 1;
            mPitch = 1;
            mStereoPan = 0;
            mStereoTransitionValues = null;
            mStereoTransitionTimeSpan = 0.5f;
            bStereoTransition = false;
            bPlayOnAwake = true;
            if (eType == EAudioInfoType.BGM)
            {
                bLoop = true;
                mPriority = 32;
            }
            else
            {
                bLoop = false;
                mPriority = 128;
            }
            mAudioSource = null;
        }

        // 指定的AudioSource组件信息记录在新的AudioInfo
        public static AudioInfo Record(AudioSource audioSource)
        {
            AudioInfo info = new AudioInfo();
            if (audioSource != null) { }
            return info;
        }
        private float GetActualVolume()
        {
            if(!bIgnoreTotalVolume) 
            {
                return AudioMgr.Instance.mTotalVolume * mVolume;
            }
            return mVolume;
        }
        // 开始音频播放
        public void Play()
        {
            if (mAudioSource != null && !mAudioSource.isPlaying)
            {
                mAudioSource.volume = GetActualVolume();
                mAudioSource.Play();
            }
        }
        // 暂停音频播放
        public void Pause()
        {
            if (mAudioSource != null && mAudioSource.isPlaying)
            {
                mAudioSource.Pause();
                //mAudioSource.volume = mVolume;
            }
        }
        public void ShareToSource(AudioSource audioSource)
        {
            if (audioSource != null)
            {
                audioSource.clip = mAudioClip;
                audioSource.volume = mVolume;
                audioSource.pitch = mPitch;
                audioSource.panStereo = mStereoPan;
                audioSource.priority = mPriority;
                audioSource.playOnAwake = bPlayOnAwake;
                audioSource.loop = bLoop;
            }
        }
        public void CopyFormSource(AudioSource audioSource)
        { 
            if (audioSource != null)
            {
                 mAudioClip = audioSource.clip;
                 mVolume = audioSource.volume;
                 mPitch = audioSource.pitch;
                 mStereoPan = audioSource.panStereo;
                 mPriority = audioSource.priority;
                 bPlayOnAwake = audioSource.playOnAwake;
                 bLoop = audioSource.loop;
                 mAudioSource = audioSource;
            }
        }
        public void ChangeTotalVolumeEvent()
        {
            if (mAudioSource == null)
                return;
            mAudioSource.volume = GetActualVolume();
        }
        public IEnumerator StereoPanTransition()
        {
            int currentIndex = 0;
            while (true)
            {
                if (mAudioSource == null || !bStereoTransition || mStereoTransitionValues == null || mStereoTransitionValues.Length == 0)
                    yield break;
                mAudioSource.panStereo = mStereoTransitionValues[currentIndex];
                yield return new WaitForSeconds(mStereoTransitionTimeSpan);
                currentIndex = (currentIndex + 1) % mStereoTransitionValues.Length;
                if (currentIndex == 0)
                {
                    mStereoTransitionValues = mStereoTransitionValues.Reverse().ToArray();
                }
            }
        }
    }
}
