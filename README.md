# Bluey Avatar Tools

![Version](https://img.shields.io/badge/version-1.0-blue)
![Unity](https://img.shields.io/badge/unity-2022.3%20LTS-green)
![VRChat SDK](https://img.shields.io/badge/VRChat-SDK3%20Avatars-purple)
![License](https://img.shields.io/badge/license-MIT-yellow)
![Status](https://img.shields.io/badge/status-active-success)

Bluey Avatar Tools is a VRChat avatar optimization tool designed to make creating Quest-compatible avatars simple and safe.

The tool focuses on clarity, accessibility, and automation, allowing creators to optimize avatars without accidentally modifying their original files.

Bluey Tools also includes optional voice guidance to help users who struggle with reading instructions.

---

# Features

## Quest Avatar Optimizer
Convert PC avatars into Quest-ready versions quickly and safely.

## Safe Clone System
Bluey Tools always creates a Quest clone first so the original avatar remains untouched.

## PhysBone Manager
Select which PhysBones to keep and remove the rest to improve Quest performance.

## Material & Texture Tools

• Duplicate materials and textures safely  
• Convert shaders to VRChat/Mobile/Toon Lit  
• Compress textures for Quest compatibility  

## Sound Remover
Automatically removes audio components that can cause Quest compatibility issues.

## Performance Scanner

Displays useful avatar statistics:

• Triangle count  
• Renderer count  
• Material count  
• Texture count  
• PhysBone count  
• Audio sources  

## Download Size Estimator
Estimates avatar download size to help keep avatars under the Quest 10MB target.

## Accessibility
Optional voice guidance can read instructions aloud for users with dyslexia or reading difficulties.

---

# Screenshots

Add screenshots of the tool here to show the interface.

Example folder:

Images  
 ├ optimizer.png  
 ├ physbones.png  
 ├ assets.png  
 └ stats.png  

Then reference them like this:

![Optimizer Tab](Images/optimizer.png)  
![PhysBones Tab](Images/physbones.png)

---

# Installation

Download the latest .unitypackage from the Releases page.

Open your Unity project.

Import the package:

Assets → Import Package → Custom Package

Select the downloaded file.

Open the tool from:

Tools → Bluey Avatar Tools

---

# Requirements

Unity 2022.3 LTS  
VRChat SDK3 (Avatars)

---

# Basic Workflow

1. Select your avatar  
2. Click Create Quest Clone  
3. Choose which PhysBones to keep  
4. Click Make Quest Avatar  
5. Save the Quest prefab  

The original avatar will never be modified.

All changes happen on the Quest clone.

---

# Folder Structure

Bluey Tools automatically creates the following export structure:

Assets  
 └ BlueyTools  
     └ QuestExports  
         └ AvatarName  
             ├ Prefab  
             ├ Materials  
             ├ Textures  
             └ Meshes  

---

# Planned Features

Future updates may include:

• Texture atlas generator  
• Renderer merging (draw call reduction)  
• Material slot merger  
• Quest performance ranking  
• Improved UI workflow  
• VRChat Creator Companion repository support  

---

# Known Limitations

• Voice guidance currently works only on Windows  
• Some complex avatars may require manual adjustments  
• Shader conversion assumes VRChat Mobile shaders are installed  

---

# Contributing

Pull requests and suggestions are welcome.

If you find a bug, please open an Issue and include:

Unity version  
VRChat SDK version  
Screenshots or error logs  

---

# License

This project is released under the MIT License.

You are free to use, modify, and distribute it as long as the license is included.

---

# Credits

Created by Bluey for the VRChat community.

Special thanks to the creators of tools like:

• Avatar Optimizer  
• VRCFury  
• VRChat SDK  

which inspired parts of the workflow.

---

# Support

If you find this tool helpful you can:

⭐ Star the repository  
🐛 Report bugs  
💡 Suggest improvements
