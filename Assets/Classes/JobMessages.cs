using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobMessages
{
    public static string[] pickup_message_prefixes =
    {
        "Oi, I need you to take this trailer full of ",
        "Could you take this trailer of ",
        "Take this trailer containing ",
        "Bring this trailer of ",
        "Hey, the last guy didn't make it. Can you take this trailer of ",
        "'Bout time you showed up. I need you to take this trailer of ",
        "This might be an odd request, but could you bring this trailer of ",
        "This might be dangerous, but could you take this trailer of ",
        "Ah, you're here. Could you take this trailer of "
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
        "85\" Touch-Screen Televisions",
        "Glitter Paint",
        "Christmas Crackers",
        "Half-eaten Blueberries",
        "Fireworks",
        "Lightsabers",
        "Odds and Ends",
        "Loo Rolls",
        "Half Melted Easter Eggs",
        "Orks",
        "Catapults",
        "Broken Eggs",
        "Portal Guns",
        "'Just for Men' hair dyes",
        "Bowling Balls",
        "Bananas",
        "Used Socks",
        "Soap",
        "Orange Juice",
        "CD Keys",
        "Lost USB Sticks",
        "Nose Clippers",
        "Bounty Bars",
        "Coconut Shells",
        "Meal Deals",
        "Sour Wine Gums"
    };

    public static string[] pickup_message_suffixes =
    {
        " to the Depot. We need it ASAP.",
        " to the Depot as quickly as possible, I don't mind if it gets banged about a bit.",
        " to the Depot. I need it for ... reasons.",
        " to the Depot. There's a swarm of people here asking for it!",
        " to the Depot, and be discrete about it, if you could.",
        " to the Depot as safely as possible, I need it in tip top condition.",
        " to the Depot by yesterday.",
        " to the Depot. I'd do it myself, but I'm babysitting.",
        " to my house, no wait.. the Depot. I meant the Depot.",
        " to the Depot. Now get out of here, I'm busy playing SlotDrop.",
        " to the Secret Depot. You know the place, right?",
        " to the usual place. It's right by the uh... cargo containers? I forget."
    };

    public static string[] delivery_messages =
    {
        "Grand! Thanks for this, it will come in real handy.",
        "Appreciate the drop-off. A bit late, but I'll handle the paperwork.",
        "This isn't what I ordered at all! Oh well, I'm sure I'll find a use for it. Thanks anyway.",
        "A speedy delivery by today's standards. Good job.",
        "Who are you? Oh, hey. Thanks for the goods. Keep up the good work!",
        "What time do you call this? It was supposed to be here three weeks ago? Oh well thanks anyway.",
        "Well well well, look who it is... Wait? Who are you again? Just leave it there.",
        "Thanks for the delivery. I'll be out in a minute. Or two... Don't wait for me.",
        "Awesome. Thanks for that. I'm surprised you managed to get here, what with the radiation and all."
    };

    public static string[] job_fail_messages =
    {
        "You wrecked the cargo! Damn, that's not going to go down well with my investors.",
        "How did you manage that?! Oh well, I don't think anyone needed it anyway.",
        "You flipped the trailer.. Can't say I'm surprised.",
        "I thought I told you to deliver the trailer, not tip it!",
        "Uhh, just pretend no one saw that. Drive away, quick!",
        "Oh boy I can't wait for this trailer to arrive... Oh.. you flipped it. Nevermind.",
        "That was some expensive cargo! I think... Insurance should cover it. I hope."
    };

}
