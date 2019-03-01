using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DwarfMine.Models.Util
{
    public class Job
    {
        public bool IsDone { get; private set; }
        public bool IsCanceled { get; private set; }

        private string _error;
        private Action _action;

        public Job(Action action)
        {
            _action = action;
        }

        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Run));
            t.Start();
        }

        public void Run()
        {
            try
            {
                _action();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }

            IsDone = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
