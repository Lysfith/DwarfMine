﻿using DwarfMine.Windows.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DwarfMine.Windows.Managers
{
    public class DebugGame
    {
        private static double _updateTime;
        private static double _drawTime;

        private static Stopwatch _swUpdate;
        private static Stopwatch _swDraw;

        public static void Log(string classe, string methode, string message)
        {
#if DEBUG
            Console.WriteLine(string.Format("[{0}][{1}] {2}", classe, methode, message));
#endif
        }

//        public static void StartFpsCounter()
//        {
//#if true
//            if (_swFps != null)
//            {
//                if (_swFps.IsRunning)
//                {
//                    _swFps.Restart();
//                }
//                else
//                {
//                    _swFps.Start();
//                }
//            }
//            else
//            {
//                _swFps = new Stopwatch();
//                _swFps.Start();
//            }
//#endif
//        }

//        public static void StopFpsCounter()
//        {
//#if DEBUG
//            if (_swFps != null && _swFps.IsRunning)
//            {
//                _swFps.Stop();
//            }
//#endif
//        }


        public static void StartUpdate()
        {
#if DEBUG
            if (_swUpdate != null)
            {
                if (_swUpdate.IsRunning)
                {
                    _swUpdate.Restart();
                }
                else
                {
                    _swUpdate.Start();
                }
            }
            else
            {
                _swUpdate = new Stopwatch();
                _swUpdate.Start();
            }
#endif
        }

        public static void StopUpdate()
        {
#if DEBUG
            if (_swUpdate != null && _swUpdate.IsRunning)
            {
                _swUpdate.Stop();
                _updateTime = _swUpdate.ElapsedMilliseconds;
            }
#endif
        }

        public static void StartDraw()
        {
#if DEBUG
            if (_swDraw != null)
            {
                if (_swDraw.IsRunning)
                {
                    _swDraw.Restart();
                }
                else
                {
                    _swDraw.Start();
                }
            }
            else
            {
                _swDraw = new Stopwatch();
                _swDraw.Start();
            }
#endif
        }

        public static void StopDraw()
        {
#if DEBUG
            if (_swDraw != null && _swDraw.IsRunning)
            {
                _swDraw.Stop();
                _drawTime = _swDraw.ElapsedMilliseconds;
            }
#endif
        }

        public static void Draw(CustomSpriteBatch spritebatch, SpriteFont font)
        {
#if DEBUG
            spritebatch.Begin();

            spritebatch.DrawString(font, $"Update : {_updateTime.ToString("0000")} ms", new Vector2(10, 10), Color.Yellow);
            spritebatch.DrawString(font, $"Draw : {_drawTime.ToString("0000")} ms", new Vector2(10, 30), Color.Yellow);
            spritebatch.DrawString(font, $"DrawCalls : {spritebatch.DrawCallsCount}", new Vector2(10, 50), Color.Yellow);

            spritebatch.End();
#endif
        }
    }
}
