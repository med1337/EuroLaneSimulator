using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionUIElement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Color positive_color;
    [SerializeField] Color negative_color;

    [Header("References")]
    [SerializeField] Text amount_display;
    [SerializeField] Text note_display;
    [SerializeField] List<FadableGraphic> fades;

    private float fade_duration;


    public void Init(int _amount, string _note, float _fade_delay, float _fade_duration)
    {
        amount_display.text = EvaluateAmountText(_amount);
        note_display.text = _note;

        ProcessColor(_amount);

        fade_duration = _fade_duration;
        Invoke("StartFade", _fade_delay);

        fades[0].listener_module.AddListener(this.gameObject);
    }


    void StartFade()
    {
        foreach (FadableGraphic fade in fades)
            fade.FadeOut(fade_duration);
    }


    void FadableGraphicDone()
    {
        Destroy(this.gameObject);
    }


    string EvaluateAmountText(int _amount)
    {
        string str = "";

        if (_amount > 0)
        {
            str += "+";
            AudioManager.PlayOneShot("Cash_Gained");
        }
        else
        {
            str += "-";
            AudioManager.PlayOneShot("Citation");
        }
        str += "$" + Mathf.Abs(_amount).ToString();

        return str;
    }


    void ProcessColor(int _amount)
    {
        Color color = _amount > 0 ? positive_color : negative_color;

        amount_display.color = color;
        note_display.color = color;
    }

}
