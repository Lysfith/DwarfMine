using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DwarfMine.Managers
{
    public class JobManager : BaseManager
    {
        private static JobManager _instance;

        public static JobManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JobManager();
                }

                return _instance;
            }
        }


        public List<Job> Jobs { get; private set; }

        public JobManager()
        {
            Jobs = new List<Job>();
        }

        public override void Load(ContentManager content)
        {
            
        }

        public Job AddJob(Action action)
        {
            var job = new Job(action);
            Jobs.Add(job);

            Thread t = new Thread(new ThreadStart(job.Run));
            t.Start();

            return job;
        }

        public void Update()
        {
            if (Jobs.Count > 0)
            {
                var jobsToRemove = new List<Job>();

                foreach (var job in Jobs.ToList())
                {
                    if (job.IsDone)
                    {
                        jobsToRemove.Add(job);
                    }
                    else if (job.IsCanceled)
                    {
                        jobsToRemove.Add(job);
                    }
                }

                Jobs.RemoveAll(j => jobsToRemove.Contains(j));
            }
        }
    }

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
