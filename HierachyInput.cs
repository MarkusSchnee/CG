using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using static Fusee.Engine.Core.Input;

namespace Fusee.Tutorial.Core
{
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _sideArmTransform;

        private TransformComponent _othersideArmTransform;

        private TransformComponent _ersterGreifArmTransform;

        private TransformComponent _zweiterGreifArmTransform;



        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            //roter Arm
            _bodyTransform = new TransformComponent{
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1 ,1, 1),
                //Position roter Arm
                Translation = new float3(0 , 6, 0)
            };


            //grüner Arm
            _upperArmTransform = new TransformComponent{
                Rotation = new float3(0, 0, 0),
                Scale = new float3 (1, 1, 1),
                //Position grüner Arm
                Translation = new float3 (2, 4, 0)
            };

            //blauer Arm
            _sideArmTransform = new TransformComponent{
                Rotation = new float3(0, 0, 0),
                Scale = new float3 (1, 1, 1),
                //Position blauer Arm
                Translation = new float3 (-2, 8, 0)
            };

            //zweiter baluer Arm
            _othersideArmTransform = new TransformComponent{
                Rotation = new float3(0,0,0),
                Scale = new float3(1,1,1),
                Translation = new float3(2,8,0)
            };

            //erster Greifarm
            //_ersterGreifArmTransform = new TransformComponent{
                //Rotation= new float3(0,0,0),
                //Scale = new float3(0.5f,0.5f,1),
                //Translation = new float3(1,12,0)
            //};

            //zweiter Greifarm
            //_zweiterGreifArmTransform = new TransformComponent{
              //  Rotation= new float3(0,0,0),
                //Scale = new float3(0.5f,0.5f,1),
                //Translation = new float3(-1,12,0)
            //};


           

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // MATERIAL COMPONENT
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },

                    //Red Body
                    new SceneNodeContainer {
                        Components = new List<SceneComponentContainer>{
                            _bodyTransform,
                            new MaterialComponent{
                                //Farbe des Cuboiden 
                                Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }

                            },
                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                        },

                        Children = new List<SceneNodeContainer>{


                            
                            // GREEN UPPER ARM
                            new SceneNodeContainer
                            {
                                Components = new List<SceneComponentContainer>
                                {
                                    _upperArmTransform,
                                },
                                Children = new List<SceneNodeContainer>
                                {
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            new TransformComponent
                                            {
                                                Rotation = new float3(0, 0, 0),
                                                Scale = new float3(1, 1, 1),
                                                Translation = new float3(0, 4, 0)
                                            },
                                            new MaterialComponent
                                            {
                                                Diffuse = new MatChannelContainer { Color = new float3(0, 1, 0) },
                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                            },
                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                        }
                                    },
                                    // BLUE FOREARM
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _sideArmTransform,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            }
                                        }
                                    },
                                    //zweiter blauer Arm!
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _othersideArmTransform,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            }
                                        }
                                        

                                    }
                                   
                                }
                            },
                        }                        
                    } 
                }
            };
        }








        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }









        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += 0.05f * Keyboard.LeftRightAxis;
            _bodyTransform.Rotation = new float3 (0, bodyRot, 0);


            float greenarm = _upperArmTransform.Rotation.x;
            greenarm += 0.05f * Keyboard.UpDownAxis;
            _upperArmTransform.Rotation = new float3(greenarm, 0, 0);


            float bluearm = _sideArmTransform.Rotation.x;
            bluearm += 0.05f * Keyboard.WSAxis;
            _sideArmTransform.Rotation = new float3(bluearm, 0, 0);

            float bluearm2 = _othersideArmTransform.Rotation.x;
            bluearm2 += 0.05f * Keyboard.ADAxis;
            _othersideArmTransform.Rotation = new float3(bluearm2,0,0);

            if(Mouse.LeftButton == true){
                _camAngle -= (Mouse.Velocity.x * DeltaTime) / 100;
            }

            //float greifarm1 = _ersterGreifArmTransform.Rotation.x;
           // greifarm1 += 0.05f * Keyboard.ADAxis;
            //_ersterGreifArmTransform.Rotation = new float3(greifarm1,0,0);

            //float greifarm2 = _ersterGreifArmTransform.Rotation.x;
            //greifarm2 += 0.05f * Keyboard.ADAxis;
            //_ersterGreifArmTransform.Rotation = new float3(greifarm2,0,0);




            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }









        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}