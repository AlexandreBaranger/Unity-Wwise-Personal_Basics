using UnityEngine;
using AK.Wwise;
using System.IO;
using UnityEditor;
public class RTPCSynthPresetState : MonoBehaviour
{
    public enum PresetType
    {
        Preset1,
        Preset2,
        Preset3,
        Preset4,
        Preset5,
        Preset6,
        Preset7,
        Preset8,
        Preset9,
        Preset10
    }

    [System.Serializable]
    public class Preset
    {
        [Range(-96f, 0f)]
        public float sliderRtpcVolume;
        [Range(0f, 100f)]
        public float sliderRtpcGen1;
        [Range(0f, 100f)]
        public float sliderRtpcGen2;
        [Range(0f, 100f)]
        public float sliderRtpcGen3;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth4;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth5;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth6;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth7;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth8;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth9;
        [Range(-1f, 1f)]
        public float sliderRtpcSynth10;
    }

    public AK.Wwise.RTPC rtpcVolume;
    public AK.Wwise.RTPC rtpcGen1;
    public AK.Wwise.RTPC rtpcGen2;
    public AK.Wwise.RTPC rtpcGen3;
    public AK.Wwise.RTPC rtpcSynth4;
    public AK.Wwise.RTPC rtpcSynth5;
    public AK.Wwise.RTPC rtpcSynth6;
    public AK.Wwise.RTPC rtpcSynth7;
    public AK.Wwise.RTPC rtpcSynth8;
    public AK.Wwise.RTPC rtpcSynth9;
    public AK.Wwise.RTPC rtpcSynth10;

    public Preset preset1;
    public Preset preset2;
    public Preset preset3;
    public Preset preset4;
    public Preset preset5;
    public Preset preset6;
    public Preset preset7;
    public Preset preset8;
    public Preset preset9;
    public Preset preset10;

    [SerializeField]
    private bool preset1Selected;
    [SerializeField]
    private bool preset2Selected;
    [SerializeField]
    private bool preset3Selected;
    [SerializeField]
    private bool preset4Selected;
    [SerializeField]
    private bool preset5Selected;
    [SerializeField]
    private bool preset6Selected;
    [SerializeField]
    private bool preset7Selected;
    [SerializeField]
    private bool preset8Selected;
    [SerializeField]
    private bool preset9Selected;
    [SerializeField]
    private bool preset10Selected;

    private bool previousPreset1Selected;
    private bool previousPreset2Selected;
    private bool previousPreset3Selected;
    private bool previousPreset4Selected;
    private bool previousPreset5Selected;
    private bool previousPreset6Selected;
    private bool previousPreset7Selected;
    private bool previousPreset8Selected;
    private bool previousPreset9Selected;
    private bool previousPreset10Selected;

    [System.Serializable]
    public class AnimationEventInfo
    {
        public AnimationClip clip;
        public float[] times;
        public PresetType[] presets; // Use enum instead of string
    }

    public AnimationEventInfo preset1Animation;
    public AnimationEventInfo preset2Animation;
    public AnimationEventInfo preset3Animation;
    public AnimationEventInfo preset4Animation;
    public AnimationEventInfo preset5Animation;
    public AnimationEventInfo preset6Animation;
    public AnimationEventInfo preset7Animation;
    public AnimationEventInfo preset8Animation;
    public AnimationEventInfo preset9Animation;
    public AnimationEventInfo preset10Animation;

    [Range(0f, 100f)]
    public float volumeRandomRange = 2f;
    [Range(0f, 100f)]
    public float gen1RandomRange = 2f;
    [Range(0f, 100f)]
    public float gen2RandomRange = 2f;
    [Range(0f, 100f)]
    public float gen3RandomRange = 2f;
    [Range(0f, 1f)]
    public float synth4RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth5RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth6RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth7RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth8RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth9RandomRange = 0.1f;
    [Range(0f, 1f)]
    public float synth10RandomRange = 0.1f;

    public bool Preset1
    {
        get => preset1Selected;
        set
        {
            preset1Selected = value;
            if (value)
            {
                ApplyPreset(preset1);
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset2
    {
        get => preset2Selected;
        set
        {
            preset2Selected = value;
            if (value)
            {
                ApplyPreset(preset2);
                Preset1 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset3
    {
        get => preset3Selected;
        set
        {
            preset3Selected = value;
            if (value)
            {
                ApplyPreset(preset3);
                Preset1 = false;
                Preset2 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset4
    {
        get => preset4Selected;
        set
        {
            preset4Selected = value;
            if (value)
            {
                ApplyPreset(preset4);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset5
    {
        get => preset5Selected;
        set
        {
            preset5Selected = value;
            if (value)
            {
                ApplyPreset(preset5);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset6
    {
        get => preset6Selected;
        set
        {
            preset6Selected = value;
            if (value)
            {
                ApplyPreset(preset6);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset7
    {
        get => preset7Selected;
        set
        {
            preset7Selected = value;
            if (value)
            {
                ApplyPreset(preset7);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset8 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset8
    {
        get => preset8Selected;
        set
        {
            preset8Selected = value;
            if (value)
            {
                ApplyPreset(preset8);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset9 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset9
    {
        get => preset9Selected;
        set
        {
            preset9Selected = value;
            if (value)
            {
                ApplyPreset(preset9);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset10 = false;
            }
        }
    }

    public bool Preset10
    {
        get => preset10Selected;
        set
        {
            preset10Selected = value;
            if (value)
            {
                ApplyPreset(preset10);
                Preset1 = false;
                Preset2 = false;
                Preset3 = false;
                Preset4 = false;
                Preset5 = false;
                Preset6 = false;
                Preset7 = false;
                Preset8 = false;
                Preset9 = false;
            }
        }
    }

    private void Update()
    {
        if (preset1Selected && !previousPreset1Selected)
        {
            ApplyPreset(preset1);
        }
        else if (preset2Selected && !previousPreset2Selected)
        {
            ApplyPreset(preset2);
        }
        else if (preset3Selected && !previousPreset3Selected)
        {
            ApplyPreset(preset3);
        }
        else if (preset4Selected && !previousPreset4Selected)
        {
            ApplyPreset(preset4);
        }
        else if (preset5Selected && !previousPreset5Selected)
        {
            ApplyPreset(preset5);
        }
        else if (preset6Selected && !previousPreset6Selected)
        {
            ApplyPreset(preset6);
        }
        else if (preset7Selected && !previousPreset7Selected)
        {
            ApplyPreset(preset7);
        }
        else if (preset8Selected && !previousPreset8Selected)
        {
            ApplyPreset(preset8);
        }
        else if (preset9Selected && !previousPreset9Selected)
        {
            ApplyPreset(preset9);
        }
        else if (preset10Selected && !previousPreset10Selected)
        {
            ApplyPreset(preset10);
        }

        previousPreset1Selected = preset1Selected;
        previousPreset2Selected = preset2Selected;
        previousPreset3Selected = preset3Selected;
        previousPreset4Selected = preset4Selected;
        previousPreset5Selected = preset5Selected;
        previousPreset6Selected = preset6Selected;
        previousPreset7Selected = preset7Selected;
        previousPreset8Selected = preset8Selected;
        previousPreset9Selected = preset9Selected;
        previousPreset10Selected = preset10Selected;
    }

    private void ApplyPreset(Preset preset)
    {
        float volume = preset.sliderRtpcVolume + Random.Range(-volumeRandomRange, volumeRandomRange);
        float gen1 = preset.sliderRtpcGen1 + Random.Range(-gen1RandomRange, gen1RandomRange);
        float gen2 = preset.sliderRtpcGen2 + Random.Range(-gen2RandomRange, gen2RandomRange);
        float gen3 = preset.sliderRtpcGen3 + Random.Range(-gen3RandomRange, gen3RandomRange);
        float synth4 = preset.sliderRtpcSynth4 + Random.Range(-synth4RandomRange, synth4RandomRange);
        float synth5 = preset.sliderRtpcSynth5 + Random.Range(-synth5RandomRange, synth5RandomRange);
        float synth6 = preset.sliderRtpcSynth6 + Random.Range(-synth6RandomRange, synth6RandomRange);
        float synth7 = preset.sliderRtpcSynth7 + Random.Range(-synth7RandomRange, synth7RandomRange);
        float synth8 = preset.sliderRtpcSynth8 + Random.Range(-synth8RandomRange, synth8RandomRange);
        float synth9 = preset.sliderRtpcSynth9 + Random.Range(-synth9RandomRange, synth9RandomRange);
        float synth10 = preset.sliderRtpcSynth10 + Random.Range(-synth10RandomRange, synth10RandomRange);

        volume = Mathf.Clamp(volume, -96f, 0f);
        gen1 = Mathf.Clamp(gen1, 0f, 100f);
        gen2 = Mathf.Clamp(gen2, 0f, 100f);
        gen3 = Mathf.Clamp(gen3, 0f, 100f);
        synth4 = Mathf.Clamp(synth4, -1f, 1f);
        synth5 = Mathf.Clamp(synth5, -1f, 1f);
        synth6 = Mathf.Clamp(synth6, -1f, 1f);
        synth7 = Mathf.Clamp(synth7, -1f, 1f);
        synth8 = Mathf.Clamp(synth8, -1f, 1f);
        synth9 = Mathf.Clamp(synth9, -1f, 1f);
        synth10 = Mathf.Clamp(synth10, -1f, 1f);

        Debug.Log($"Applying preset: Volume = {volume}, Synth1 = {gen1}, Synth2 = {gen2}, Synth3 = {gen3}, Synth4 = {synth4}, Synth5 = {synth5}, Synth6 = {synth6}, Synth7 = {synth7}, Synth8 = {synth8}, Synth9 = {synth9}, Synth10 = {synth10}");

        rtpcVolume.SetGlobalValue(volume);
        rtpcGen1.SetGlobalValue(gen1);
        rtpcGen2.SetGlobalValue(gen2);
        rtpcGen3.SetGlobalValue(gen3);
        rtpcSynth4.SetGlobalValue(synth4);
        rtpcSynth5.SetGlobalValue(synth5);
        rtpcSynth6.SetGlobalValue(synth6);
        rtpcSynth7.SetGlobalValue(synth7);
        rtpcSynth8.SetGlobalValue(synth8);
        rtpcSynth9.SetGlobalValue(synth9);
        rtpcSynth10.SetGlobalValue(synth10);
    }

    public void ApplyPresetByType(PresetType presetType)
    {
        switch (presetType)
        {
            case PresetType.Preset1:
                Preset1 = true;
                break;
            case PresetType.Preset2:
                Preset2 = true;
                break;
            case PresetType.Preset3:
                Preset3 = true;
                break;
            case PresetType.Preset4:
                Preset4 = true;
                break;
            case PresetType.Preset5:
                Preset5 = true;
                break;
            case PresetType.Preset6:
                Preset6 = true;
                break;
            case PresetType.Preset7:
                Preset7 = true;
                break;
            case PresetType.Preset8:
                Preset8 = true;
                break;
            case PresetType.Preset9:
                Preset9 = true;
                break;
            case PresetType.Preset10:
                Preset10 = true;
                break;
            default:
                Debug.LogWarning($"Preset '{presetType}' not found.");
                break;
        }
    }

    void Start()
    {
        SetAnimationEvents(preset1Animation);
        SetAnimationEvents(preset2Animation);
        SetAnimationEvents(preset3Animation);
        SetAnimationEvents(preset4Animation);
        SetAnimationEvents(preset5Animation);
        SetAnimationEvents(preset6Animation);
        SetAnimationEvents(preset7Animation);
        SetAnimationEvents(preset8Animation);
        SetAnimationEvents(preset9Animation);
        SetAnimationEvents(preset10Animation);
    }

    void SetAnimationEvents(AnimationEventInfo animationEventInfo)
    {
        if (animationEventInfo.clip != null && animationEventInfo.presets.Length == animationEventInfo.times.Length)
        {
            for (int i = 0; i < animationEventInfo.times.Length; i++)
            {
                AnimationEvent evt = new AnimationEvent();
                evt.time = animationEventInfo.times[i];
                evt.functionName = "InvokePreset";
                evt.intParameter = (int)animationEventInfo.presets[i];
                animationEventInfo.clip.AddEvent(evt);
            }
        }
        else
        {
            Debug.LogWarning("AnimationEventInfo times and presets length mismatch or clip is null");
        }
    }

    void InvokePreset(int presetIndex)
    {
        ApplyPresetByType((PresetType)presetIndex);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RTPCSynthPresetState))]
    public class RTPCSynthPresetStateEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RTPCSynthPresetState script = (RTPCSynthPresetState)target;

            if (GUILayout.Button("Save Preset to CSV"))
            {
                SavePresetToCSV(script);
            }
        }

        private string FormatFloat(float value)
        {
            return value.ToString("F6").Replace(',', '.');
        }
        private void SavePresetToCSV(RTPCSynthPresetState script)
        {
            string path = EditorUtility.SaveFilePanel("Save Preset", "", "preset.csv", "csv");
            if (string.IsNullOrEmpty(path)) return;

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Volume" + "," + script.rtpcVolume + "," + FormatFloat(script.rtpcVolume.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Gen1" + "," + script.rtpcGen1 + "," + FormatFloat(script.rtpcGen1.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Gen2" + "," + script.rtpcGen2 + "," + FormatFloat(script.rtpcGen2.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Gen3" + "," + script.rtpcGen3 + "," + FormatFloat(script.rtpcGen3.GetValue(script.gameObject))+  "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth4" + "," + script.rtpcSynth4 + "," + FormatFloat(script.rtpcSynth4.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth5" + "," + script.rtpcSynth5 + "," + FormatFloat(script.rtpcSynth5.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth6" + "," + script.rtpcSynth6 + "," + FormatFloat(script.rtpcSynth6.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth7" + "," + script.rtpcSynth7 + "," + FormatFloat(script.rtpcSynth7.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth8" + "," + script.rtpcSynth8 + "," + FormatFloat(script.rtpcSynth8.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth9" + "," + script.rtpcSynth9 + "," + FormatFloat(script.rtpcSynth9.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
                writer.WriteLine("Synth10" + "," + script.rtpcSynth10 + "," + FormatFloat(script.rtpcSynth10.GetValue(script.gameObject))+ "," + "0.0" + "," + "0.0");
            }

            Debug.Log("Preset saved to " + path);
            AssetDatabase.Refresh();
        }
    }
#endif
}
