using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Repositories.Implementations
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository, IDisposable
    {
        private readonly AppDBContext context;

        public ApplicantRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Applicant> Preferences => context.Applicants.OrderByDescending(x => x.ID).ToList();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
