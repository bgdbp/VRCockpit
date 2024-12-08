**VR Cockpit Client v1.0.0**

This is the client side of our VR Cockpit project, it is responsible for displaying
the virtual cockpit in 'Mixed Reality', as well as sending and receiving control states to and from the VR Cockpit Server.
This repository contains a Unity 6 Project and was tested and demonstrated using a Meta Quest 3 VR Headset, 
all though other headsets may work if it has hand tracking and camera passthrough capabilities. 

The VR Cockpit Server is built to a Raspberry Pi 5 and Arduino Uno R3, its repository
is located here: https://github.com/bgdbp/VRCockpitServer

**Build Instructions**

This project was tested and demonstrated using Unity editor version 6000.0.23f1. To install this,
you must create a Unity account, download Unity Hub, and then install this version of Unity from
Unity Hub. Clone this repository onto your machine, and in Unity Hub, click 'Open Existing Project',
and then navigate to your copy of the repository.

Once the Unity project is open, navigate to File->Build Profiles, in this window, select the
Android platform, make sure to click the 'Switch Platform' button. To build this project
to the headset, connect the headset to your computer using a USB C cable, you'll need to 
put your headset into developer mode, otherwise the next step will fail. 

In the Build Profiles window, ensure your VR headset device is showing up in the
Run Device dropdown menu, and then click Patch. Unity will now build this project
to your headset. After the build completes, the VR Cockpit app can be ran
from the headset, you may now unplug the USB C cable. 

If a server is present on the local private network, the VR Cockpit client app
will discover it and connect. If the connection is successful, the controls
between the client and server will be synchronized. 
