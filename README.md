# NetworkStatusIndicator

A simple and customizable network status indicator for Unity3D. The script displays a series of bars representing the quality of the network connection, where the number of visible bars changes according to the current network latency. No additional image files are needed, as the indicator is generated programmatically.

# Features
Real-time network latency detection.
Customizable bar colors and dimensions.
Easy to integrate with any Unity project.
No additional image files required.

# Installation
Download the NetworkStatusIndicator.cs script from this repository.
Import the script into your Unity project by placing it in your Assets folder.
Create a new GameObject in your scene and attach the NetworkStatusIndicator script to it.
Add a UI Image component to the GameObject and assign it to the currentStatusIcon field in the NetworkStatusIndicator script.

# Usage
After installing the script and setting up the GameObject, the network status indicator will automatically update the bars' visibility and colors based on the current network latency. The indicator sends a request to a specified URL every 5 seconds to measure the latency. You can replace the example URL (http://www.google.com) in the CheckNetworkStatus coroutine with any URL of your choice:
You can also customize the number of bars, their colors, and dimensions by modifying the script's variables.

# License
This project is available under the MIT License.



