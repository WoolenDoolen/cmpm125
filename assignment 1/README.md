# Week 1 - Hello World

Open this folder in Unity Hub with **Unity 6000.6.3f1**. Open `Assets/Scenes/HelloWorld.unity`, set the Game view to **16:9**, press Play, and click the Game view.

Three objects: Peter, a cube, and a capsule. Each can be moved, rotated, scaled, and turned off/on with the **Input System**.

| Key | Action |
| --- | --- |
| 1 / 2 / 3 | Select Peter / cube / capsule |
| WASD or arrow keys | Move the selected object |
| Q / E | Rotate left / right |
| Z / X | Shrink / grow |
| Space | Turn the selected object off / on |

The controls stay active when an object is off. Press Space again to bring it back. Stop and restart Play mode to reset the scene.

`Assets/Scripts/ObjectControls.cs` is the only gameplay script. `Assets/Input/SceneControls.inputactions` contains the keyboard bindings; the scene's **Scene Controls** object has the Player Input component set to **Send Messages**.

The script uses separate Peter, Cube, and Capsule fields, basic variables, input methods, if/else statements, Transform changes, and SetActive. The assignment screenshot does not specify which scripting features were covered in class; check those against your week's material.

**Submission GIF:** [Recordings/HelloWorld-Demo.gif](Recordings/HelloWorld-Demo.gif) (18 seconds). It shows all four controls on each object, captured from the running Unity scene using simulated keyboard input through the Input System. Upload and embed this GIF in the assignment. The full Unity project is not required.

**Checked in Unity:** selection, movement, rotation in both directions, growing/shrinking, and off/on for all three objects; arrow-key movement; scale limits. Results are in `Recordings/verification.txt`.

Peter image: [matching transparent meme image on Know Your Meme](https://knowyourmeme.com/photos/2553548-family-guy-death-pose-peter-falls-down-the-stairs), used unchanged. Family Guy character/art belongs to its respective owners. The other objects are Unity primitives.
