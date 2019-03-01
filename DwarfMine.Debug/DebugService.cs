using DwarfMine.Core.Graphics;
using DwarfMine.Interfaces.Debug;
using DwarfMine.Interfaces.Resource;
using DwarfMine.Interfaces.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace DwarfMine.Debug
{
    public class DebugService : IDebugService
    {
        private double _updateTime;
        private double _drawTime;

        private Stopwatch _swUpdate;
        private Stopwatch _swDraw;

        private SpriteFont _font;

        private IJobService _jobService;

        public DebugService(IJobService jobService, IFontService fontService)
        {
            _jobService = jobService;

            _font = fontService.GetFont("PixelMaster-24");
        }

        public void HideScreen()
        {
            throw new NotImplementedException();
        }

        public void ShowScreen()
        {
            throw new NotImplementedException();
        }

        public void StartUpdate()
        {
#if true
            if (_swUpdate != null)
            {
                if (_swUpdate.IsRunning)
                {
                    _swUpdate.Restart();
                }
                else
                {
                    _swUpdate.Restart();
                }
            }
            else
            {
                _swUpdate = new Stopwatch();
                _swUpdate.Start();
            }
#endif
        }

        public void StopUpdate()
        {
#if true
            if (_swUpdate != null && _swUpdate.IsRunning)
            {
                _swUpdate.Stop();
                _updateTime = _swUpdate.ElapsedMilliseconds;
            }
#endif
        }

        public void StartDraw()
        {
#if true
            if (_swDraw != null)
            {
                if (_swDraw.IsRunning)
                {
                    _swDraw.Restart();
                }
                else
                {
                    _swDraw.Restart();
                }
            }
            else
            {
                _swDraw = new Stopwatch();
                _swDraw.Start();
            }
#endif
        }

        public void StopDraw()
        {
#if true
            if (_swDraw != null && _swDraw.IsRunning)
            {
                _swDraw.Stop();
                _drawTime = _swDraw.ElapsedMilliseconds;
            }
#endif
        }

        public void Draw(GameTime time, CustomSpriteBatch spritebatch)
        {
#if true
            var drawCount = spritebatch.DrawCallsCount;

            spritebatch.Begin();

            spritebatch.SpriteBatch.DrawString(_font, $"Update : {_updateTime.ToString("0000")} ms", new Vector2(10, 10), Color.Yellow);
            spritebatch.SpriteBatch.DrawString(_font, $"Draw : {_drawTime.ToString("0000")} ms", new Vector2(10, 30), Color.Yellow);
            spritebatch.SpriteBatch.DrawString(_font, $"DrawCalls : {drawCount}", new Vector2(10, 50), Color.Yellow);
            spritebatch.SpriteBatch.DrawString(_font, $"Jobs : {_jobService.GetJobsCount()}", new Vector2(10, 70), Color.Yellow);
            spritebatch.SpriteBatch.DrawString(_font, $"Active Jobs : {_jobService.GetActiveJobsCount()}", new Vector2(10, 90), Color.Yellow);

            spritebatch.End();
#endif
        }
    }
}
