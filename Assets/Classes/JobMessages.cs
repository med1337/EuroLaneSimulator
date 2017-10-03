using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobMessages
{
    public static string[] pickup_message_prefixes =
    {
        "Oi, I need you to take this trailer full of ",
        "Could you take take this trailer of ",
        "Take this trailer containing "
    };

    public static string[] cargo_types =
    {
        "Unattended Luggage",
        "Dogs",
        "Mysterious Boxes",
        "Packing Supplies",
        "Coolaid",
        "Broken VR Headsets",
        "Strange Meat",
        "NES Controllers",
        "Bottlecaps",
        "E.T. Cartridges",
        "Fancy Chairs",
        "85\" Touch-Screen Televisions"
    };

    public static string[] pickup_message_suffixes =
    {
        " to the Depot. We need it ASAP.",
        " to the Depot as quickly as possible, I don't mind if it gets banged about a bit.",
        " to the Depot. I need it for ... reasons."
    };


    public static string[] delivery_messages =
    {
        "Grand! Thanks for this, it will come in real handy.",
        "Appreciate the drop-off. A bit late, but I'll handle the paperwork.",
        "This isn't what I ordered at all! Oh well, I'm sure I'll find a use for it. Thanks anyway."
    };

}
