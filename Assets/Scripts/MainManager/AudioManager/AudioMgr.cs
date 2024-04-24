using CoreManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LumiAudio
{ 
    public enum EAudioInfoType
    {
        DEFAULT,
        NORMAL,
        BGM,
    }

    public class AudioMgr : SingletonAutoMono<AudioMgr>
    {
        // ��Ϸ������ 0-1
        private float _totalVolume = 1;
        public float mTotalVolume 
        {
            get { return _totalVolume; }
            set {
                if (Mathf.Abs(_totalVolume - value) > 1e-3)
                {
                    _totalVolume = Mathf.Clamp(value, 0, 1);
                    DoOnChangeTotalVolume();
                    EventMgr.Instance.Invoke(EventDef.AudioTotalVolumeChangedEvent, _totalVolume);
                }
            }
        }
        // ��Ƶ��Ϣ����
        private bool _isOverWrite = false;
        public bool bIsOverWrite { get { return _isOverWrite; } set { _isOverWrite = value; } }

        // ��Ƶ��Ϣ����
        private Dictionary<string, AudioInfo> audioInfos;
        // ��Ƶ��������
        public int mAudioCount => audioInfos.Count;
        // �����б�
        public string[] mAudioInfoNames => audioInfos.Keys.ToArray();

        private string mCurBGMName = string.Empty;
        private AudioSource _uniqueAudioSource;
        private AudioSource mUniqueAudioSource {
            get 
            {
                if (_uniqueAudioSource == null)
                {
                    _uniqueAudioSource = gameObject.AddComponent<AudioSource>();
                }
                return _uniqueAudioSource;
            }            
        }
        // ��ʼ��
        private void Awake()
        {
            _uniqueAudioSource = null;
            InitParameters();
        }
        private void Start()
        {
            gameObject.hideFlags = HideFlags.None;
        }
        private void DoOnChangeTotalVolume()
        {
            if (audioInfos != null)
            {
                foreach(var info in audioInfos)
                {
                    info.Value.ChangeTotalVolumeEvent();
                }
            }
        }
        public void PlayBGM(string audioName, bool bForceStop = false)
        {
            if (bForceStop || !audioName.Equals(mCurBGMName))
                StopBGM();

            if (!audioInfos.ContainsKey(audioName))
            {
                if (!LoadGameAudioClip(audioName, EAudioInfoType.BGM))
                    return;
            }
            AudioInfo info = audioInfos[audioName];
            info.mAudioSource = mUniqueAudioSource;
            info.ShareToSource(mUniqueAudioSource);
            info.Play();
            mCurBGMName = audioName;
        }
        // ������Ƶ
        public void Play(string audioName)
        {
            if (!audioInfos.ContainsKey(audioName))
            {
                if (!LoadGameAudioClip(audioName))
                    return;
            }
            AudioInfo info = audioInfos[audioName];
            if (info.mInfoType == EAudioInfoType.BGM)
            {
                PlayBGM(audioName);
                return;
            }

            AudioSource audioSource = AudioSourcePool.Instance.GetAudioSourceByInfo(info);
            info.mAudioSource = audioSource;
            info.Play();
        }
        // ����ָ�����Ƶ���Ƶ����������������
        public void PlayWithStereoTransition(string audioName)
        {
            if (!audioInfos.ContainsKey(audioName))
            {
                if (!LoadGameAudioClip(audioName))
                    return;
            }
            AudioInfo info = audioInfos[audioName];
            AudioSource source = AudioSourcePool.Instance.GetAudioSourceByInfo(info);
            info.mAudioSource = source;
            StartCoroutine(info.StereoPanTransition());
            info.Play();
        }
        // ����ָ�����Ƶ���Ƶ����������������
        // audioName stereoTransitionValues, stereoTimeSpan
        public void PlayWithStereoTransition(string audioName, float[] values, float timeSpan)
        {
            if (!audioInfos.ContainsKey(audioName))
            {
                if (!LoadGameAudioClip(audioName))
                    return;
            }
            AudioInfo info = audioInfos[audioName];
            AudioSource source = AudioSourcePool.Instance.GetAudioSourceByInfo(info);
            info.mAudioSource = source;
            info.bStereoTransition = true;
            info.mStereoTransitionValues = values;
            info.mStereoTransitionTimeSpan = timeSpan;
            StartCoroutine(info.StereoPanTransition());
            info.Play();
        }
        public void StopBGM()
        {
            if (mCurBGMName.Equals(string.Empty))
                return;
            if (!audioInfos.ContainsKey(mCurBGMName))
                return;
            AudioInfo info = audioInfos[mCurBGMName];
            StopCoroutine(info.StereoPanTransition());
            info.Pause();
            mCurBGMName = string.Empty;
        }
        // ��ͣ��Ƶ
        public void StopMusic(string audioName)
        {
            if (!audioInfos.ContainsKey(audioName))
            {
                return;
            }
            AudioInfo info = audioInfos[audioName];
            StopCoroutine(info.StereoPanTransition());
            info.Pause();
            AudioSourcePool.Instance.ReturnPool(info.mAudioSource);
        }
        // �����Ƶ��Ϣ
        public void AddAudioInfo(AudioInfo audioInfo)
        {
            DoAddAudioInfo(audioInfo);
        }
        public bool DeleteAudioInfo(string audioName)
        {
            return DoDeleteAudioInfo(audioName);
        }
        private void DoAddAudioInfo(AudioInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.mAudioName))
                return;
            string audioName = info.mAudioName;
            if (bIsOverWrite) 
                audioInfos[audioName] = info;
            else if (!audioInfos.ContainsKey(audioName)) 
            {
                audioInfos.Add(audioName, info);
            }
        }
        private bool DoDeleteAudioInfo(string audioName)
        {
            if(!string.IsNullOrEmpty(audioName) && audioInfos.ContainsKey(audioName))
            {
                audioInfos.Remove(audioName);
                return true;
            }
            return false;
        }
        private bool InitParameters()
        {
            audioInfos = new Dictionary<string, AudioInfo>();
            _isOverWrite = true;
            return true;
        }
        private bool LoadGameAudioClip(string name, EAudioInfoType eType = EAudioInfoType.DEFAULT)
        {
            bool ret = false;
            AudioClip clip = (AudioClip)ResManager.Instance.GetAsset(name, typeof(AudioClip));
            if (clip != null) 
            {
                AddAudioInfo(new AudioInfo(name, clip, eType));
                ret = true; 
            }
            return ret;
        }
    }
}

