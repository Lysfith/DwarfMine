using DwarfMine.Interfaces.Util;
using DwarfMine.Models.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DwarfMine.Utils
{
    public class JobService : IJobService
    {
        private Queue<Job> _jobs;
        public List<Job> _activeJobs;
        public int concurrentJob = 5;

        public JobService()
        {
            _jobs = new Queue<Job>();
            _activeJobs = new List<Job>();
        }

        public Job AddJob(Action action)
        {
            var job = new Job(action);
            _jobs.Enqueue(job);

            return job;
        }

        public void Update(GameTime time)
        {
            _activeJobs.RemoveAll(j => j.IsDone || j.IsCanceled);

            if (_activeJobs.Count < concurrentJob && _jobs.Any())
            {
                while(_jobs.Any() && _activeJobs.Count < concurrentJob)
                {
                    var job = _jobs.Dequeue();
                    job.Start();
                    _activeJobs.Add(job);
                }
            }
        }

        public int GetJobsCount()
        {
            return _jobs.Count;
        }

        public int GetActiveJobsCount()
        {
            return _activeJobs.Count;
        }
    }
}
