# Assignment 3: Generative World

Unity 6000.6.3f1. A basic 3D object spawning demo using a plane, cubes, and spheres.

Open `Assets/Scenes/GenerativeWorld.unity` and press Play. You can also run `Build/Assignment3.exe`.

- Click a shape to collect it.
- Space: spawn another shape.
- R: clear the active objects. Automatic spawning continues.
- The same controls are available as buttons.

The assignment requirements are covered by:

| Requirement | Implementation |
| --- | --- |
| Manager class | `WorldManager` creates the pools, runs spawning, handles clicks, and tracks collected objects. |
| Multiple forms of random generation | Shape type, X/Z position, and size change randomly each time an object spawns. |
| Coroutine | `SpawnLoop()` waits 0.8 seconds between spawns using `WaitForSeconds`. |
| Object pooling | Ten cubes and ten spheres are created at startup. `PooledObject.Activate()` enables an existing object; collecting, clearing, or reaching its six-second lifetime disables it. |
| Bonus: weighted random table | The manager's Spawn Types table assigns Cube weight 80 and Sphere weight 20. Cubes are four times as likely as spheres. |

While playing, expand World Manager, Cube Pool, and Sphere Pool in the Hierarchy to see the same objects turn on and off. Select World Manager to see the weights and spawning settings in the Inspector.

Submission files are in `Submission`: the scripts zip and a demonstration video. The video shows gameplay, random shapes/positions/sizes, collecting and clearing, the manager settings and object pools, and the coroutine code.
