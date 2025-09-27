# 3D_MoveStopMove

## Progress
### Day 1 (Done)
- Download Assets. 
- Create Basic map.
- Create Player View.
- <img width="1156" height="795" alt="image" src="https://github.com/user-attachments/assets/0e41f459-a692-42ea-b4cf-0dfe2005d215" />

#### Knowledge
- #### .fbx (FilmBoX)
> A widely used 3D file format that can store models, animations, textures, and more.
> Unity Editor supports importing .fbx files, which are commonly exported from Blender, 3ds Max, Maya, and other 3D software. 

- #### Materials:
> Full set of methods available for manipulating materials via script: [Material class scripting reference](https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Material.html) </br>
> Rendering Mode: [Rendering Mode](https://docs.unity3d.com/2018.2/Documentation/Manual/StandardShaderMaterialParameterRenderingMode.html?utm_source=chatgpt.com) </br>
> Main Maps:
>> - Albedo: Base color or texture of the surface. Albedo stores the visible diffuse color (use sRGB textures). The alpha channel of Albedo can be used for transparency when the Rendering Mode is not Opaque.
>> - Metallic: How “metal-like” the surface is. Values 0 = non-metal (dielectric), 1 = metal. If you use a Metallic map, channels are commonly packed: R = metallic, A = smoothness (when Source = Metallic Alpha).
>> - Smoothness: Controls glossiness / microfacet smoothness. High smoothness → sharp, small specular highlights (mirror-like). Low smoothness → broad, dull highlights (matte).
>> - Normal Map: A texture that fakes small bumps and details by changing surface normals (no extra geometry). Unity usually asks to “Fix Now” if the map isn’t imported as a normal type — accept it. A Normal Scale controls intensity.
>> - Height Map (Parallax): A grayscale height map used for parallax/offset displacement to fake depth (more expensive than normals). Useful on flat surfaces where simulated depth matters (bricks, grooves).
>> - Occlusion (AO): Ambient occlusion map darkens crevices and cavities to increase perceived contact shadows and detail.
>> - Detail Mask: A mask (often alpha) that defines where the secondary/detail maps are applied. Use to limit dirt, scratches, or micro detail to edges or specific regions.
>> - Emission: Makes material self-illuminated (glows). You can set an emission color and intensity or an emission map to control glowing areas. Emission does not light scene geometry unless you use baked GI or specific real-time GI setups.<img width="439" height="71" alt="image" src="https://github.com/user-attachments/assets/e860e6e5-6912-488f-ad59-f34249682987" />
>> - Tiling & Offset: Tiling repeats the texture along U/V (X/Y) axes; Offset moves the UVs. Use to scale or align textures (e.g., bricks, wood grain).
