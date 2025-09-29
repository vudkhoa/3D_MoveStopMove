# 3D_MoveStopMove

## Progress
### Day 1 (Done)
- Download Assets. 
- Create Basic map.
- Create Player View.

### Day 2 
- Implemented player movement (PlayerView.cs).
- Applied design patterns:
> - Singleton. 
> - State (for managing player animations). </br>
![3d_msm_d2_gif](https://github.com/user-attachments/assets/d5c4ab4a-a1be-45d6-aefb-ea813f9e0607)

## Development
### Player
#### View
- ##### Move
> - Imported Joystick Pack: [link](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631?srsltid=AfmBOooW7PpmgXQv_mYQhWFKuACVLgtpYbheqiGIHYlrb8ri45UQOpgI). 
> - Used combination of Rigidbody + CapsuleCollider &rarr; rigidBody.MovePosition(); rigidBody.MoveRotation();
> <img width="490" height="293" alt="image" src="https://github.com/user-attachments/assets/1a6975ad-90cc-42d2-a398-2592ff60735b" /> </br>
> <img width="1109" height="559" alt="image" src="https://github.com/user-attachments/assets/e7cf8e1c-5cab-4688-9246-66f70da1c7af" />

### Design Patter
#### Singleton
>  - SingletonMono
> <img width="902" height="507" alt="image" src="https://github.com/user-attachments/assets/00c54f79-668d-459d-b42d-413171572881" />

#### State
> - IState
> <img width="391" height="275" alt="image" src="https://github.com/user-attachments/assets/b920f669-6365-4e59-bec0-8e3a52f56b7f" />

> - StateMachine
> <img width="606" height="367" alt="image" src="https://github.com/user-attachments/assets/54d164ec-fa72-4472-a900-3a4e2d23b10f" />

### Camrera Follow
> - Using offset + FixedUpdate + SmoothDamp.
>> Note: FixedUpdate &rarr; Synchronized with Moved to avoid jittering (**).
> - Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime): 
>> - ref → directly updates the variable.
>> - Velocity → keeps track of the smoothing velocity (SmoothDamp needs it to remember the previous value).
> <img width="1150" height="555" alt="image" src="https://github.com/user-attachments/assets/1f025663-a125-4aa7-8a82-b6a86a2b0df4" />

### Knowledge
#### .fbx (FilmBoX)
> - A widely used 3D file format that can store models, animations, textures, and more.
> - Unity Editor supports importing .fbx files, which are commonly exported from Blender, 3ds Max, Maya, and other 3D software. 

#### Materials
> - Full set of methods available for manipulating materials via script: [Material class scripting reference](https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Material.html) </br>
> - Rendering Mode: [Rendering Mode](https://docs.unity3d.com/2018.2/Documentation/Manual/StandardShaderMaterialParameterRenderingMode.html?utm_source=chatgpt.com) </br>
> - Main Maps:
>> - Albedo: Base color or texture of the surface. Albedo stores the visible diffuse color (use sRGB textures). The alpha channel of Albedo can be used for transparency when the Rendering Mode is not Opaque.
>> - Metallic: How “metal-like” the surface is. Values 0 = non-metal (dielectric), 1 = metal. If you use a Metallic map, channels are commonly packed: R = metallic, A = smoothness (when Source = Metallic Alpha).
>> - Smoothness: Controls glossiness / microfacet smoothness. High smoothness → sharp, small specular highlights (mirror-like). Low smoothness → broad, dull highlights (matte).
>> - Normal Map: A texture that fakes small bumps and details by changing surface normals (no extra geometry). Unity usually asks to “Fix Now” if the map isn’t imported as a normal type — accept it. A Normal Scale controls intensity.
>> - Height Map (Parallax): A grayscale height map used for parallax/offset displacement to fake depth (more expensive than normals). Useful on flat surfaces where simulated depth matters (bricks, grooves).
>> - Occlusion (AO): Ambient occlusion map darkens crevices and cavities to increase perceived contact shadows and detail.
>> - Detail Mask: A mask (often alpha) that defines where the secondary/detail maps are applied. Use to limit dirt, scratches, or micro detail to edges or specific regions.
>> - Emission: Makes material self-illuminated (glows). You can set an emission color and intensity or an emission map to control glowing areas. Emission does not light scene geometry unless you use baked GI or specific real-time GI setups.<img width="439" height="71" alt="image" src="https://github.com/user-attachments/assets/e860e6e5-6912-488f-ad59-f34249682987" />
>> - Tiling & Offset: Tiling repeats the texture along U/V (X/Y) axes; Offset moves the UVs. Use to scale or align textures (e.g., bricks, wood grain).
#### Joystick Pack 
>> - Joystick Pack scripts only support the Old Input System.
>> - Joystick Prefabs
    >>> • Fixed Joystick – fixed position (bottom-left).
    >>> • Floating Joystick – hidden by default, shown when receiving interaction. 
    >>> • Dynamic Joystick – moves following the drag direction.
    >>> • Variable Joystick – can switch between Fixed, Floating, and Dynamic; editable via scripts.
#### Character Controller
> Not using Rigidbody/Physics (not used in this project).
#### ref → directly updates the variable.
