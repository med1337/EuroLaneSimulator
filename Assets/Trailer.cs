using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Trailer : Vehicle
{
    public int CargoValue = 100000;
    public Text CargoValText;


    void OnMouseClick()
    {
        CargoValue -= 1337;
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (CargoValText != null)
            UpdateCargoValueText();

        base.Update();
    }

    private void UpdateCargoValueText()
    {
        var f = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberDecimalDigits = 0
        };
        var s = CargoValue.ToString("n", f);
        CargoValText.text = "$" + s;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}