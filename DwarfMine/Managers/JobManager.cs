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


        public Queue<Job> Jobs { get; private set; }
        public List<Job> ActiveJobs { get; private set; }
        public int ConcurrentJob { get; private set; } = 5;

        public JobManager()
        {
            Jobs = new Queue<Job>();
            ActiveJobs = new List<Job>();
        }

        public override void Load(ContentManager content)
        {
            
        }

        public Job AddJob(Action action)
        {
            var job = new Job(action);
            Jobs.Enqueue(job);

            return job;
        }

        public void Update()
        {
            ActiveJobs.RemoveAll(j => j.IsDone || j.IsCanceled);

            if (ActiveJobs.Count < ConcurrentJob && Jobs.Any())
            {
                while(Jobs.Any() && ActiveJobs.Count < ConcurrentJob)
                {
                    var job = Jobs.Dequeue();
                    job.Start();
                    ActiveJobs.Add(job);
                }
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
