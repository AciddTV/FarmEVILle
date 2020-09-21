# FarmEVILle
Factory farming game for the members of Okay Coders, codenamed FarmEVILle, for the subject XBCGD7319

Guides to start coding in Cryengine:

So I've put in the whole file, now starting it is different from how you'd start a project in Unity.

In the file, you'll see a .cryproject file, this is NOT the file you want to open.

Below that, you'll see a shortcut to a .sln file simply titled "Game".

Open this shortcut with the visual studio that you used for the setting up CryEngine tutorial
I made for you guys.

Once in here, you can see I've added a single class called FarmhouseA, this thing holds basically
everything you need to know about making an entity in CryEngine.

It holds how to create the actual entity, how to define properties for that entity,
and that you MUST have the ongameplaystart() and onupdate() functions.

Please do take note of the naming conventions I've used for the properties, it's mostly just
_propertyDisplay and that's about it.

When you want to check if it works in CryEngine, I put a screenshot of where to look, I can't
remember what it's called so I put a link:

https://imgur.com/a/GP8CCPW

The dropdown where it says "Debug", make sure it says "Debug Sandbox". This will open a new instance
of CryEngine with this new code.

Yes you have to start up CryEngine every single time you want to test some code.

Once it opens, open the levels folder and then click on the example level.

From there, there will be an empty entity, or you can make your own to test the code in. Console
is in the top left of the editor if you want to test debug.log type stuff.

If there's something I've left out of here and you guys want some clarification, let me know
and I'll update this Readme.

Keep looking at this Readme, because if there's any new developments, probably how to connect assets
to specific classes, which I will, I'll probably put it in here.

Good luck guys.
