using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFeedback : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public float hoverScale = 1.08f;
    public Color hoverColor = new Color(0.75f,0.75f,0.75f,1f);

    static AudioSource audioSource;
    Dictionary<Button,Vector3> normalScales =
        new Dictionary<Button,Vector3>();
    Dictionary<Button,Color> normalColors =
        new Dictionary<Button,Color>();

    void Awake()
    {
        CreateAudioSource();
    }

    void Start()
    {
        Button[] buttons =
            FindObjectsByType<Button>(
                FindObjectsInactive.Include
            );

        foreach(Button button in buttons)
        {
            AddFeedback(button);
        }
    }

    void AddFeedback(Button button)
    {
        if(button==null)
            return;

        normalScales[button] =
            button.transform.localScale;

        Graphic graphic =
            button.targetGraphic;

        if(graphic!=null)
        {
            normalColors[button] =
                graphic.color;
        }

        EventTrigger trigger =
            button.GetComponent<EventTrigger>();

        if(trigger==null)
        {
            trigger =
                button
                .gameObject
                .AddComponent<EventTrigger>();
        }

        AddEvent(
            trigger,
            EventTriggerType.PointerEnter,
            () => OnButtonHover(button)
        );

        AddEvent(
            trigger,
            EventTriggerType.PointerExit,
            () => OnButtonExit(button)
        );

        AddEvent(
            trigger,
            EventTriggerType.PointerClick,
            () => PlaySound(clickSound,button)
        );
    }

    void AddEvent(
        EventTrigger trigger,
        EventTriggerType type,
        System.Action action
    )
    {
        EventTrigger.Entry entry =
            new EventTrigger.Entry();

        entry.eventID = type;
        entry.callback.AddListener(
            (data) => action()
        );

        trigger.triggers.Add(entry);
    }

    void OnButtonHover(Button button)
    {
        if(!button.interactable)
            return;

        button.transform.localScale =
            normalScales[button] *
            hoverScale;

        Graphic graphic =
            button.targetGraphic;

        if(graphic!=null)
        {
            graphic.color =
                hoverColor;
        }

        PlaySound(
            hoverSound,
            button
        );
    }

    void OnButtonExit(Button button)
    {
        button.transform.localScale =
            normalScales[button];

        Graphic graphic =
            button.targetGraphic;

        if(graphic!=null && normalColors.ContainsKey(button))
        {
            graphic.color =
                normalColors[button];
        }
    }

    void PlaySound(
        AudioClip clip,
        Button button
    )
    {
        if(clip==null || !button.interactable)
            return;

        audioSource.PlayOneShot(clip);
    }

    void CreateAudioSource()
    {
        if(audioSource!=null)
            return;

        GameObject soundObject =
            new GameObject("ButtonFeedbackAudio");

        DontDestroyOnLoad(soundObject);

        audioSource =
            soundObject
            .AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }
}
