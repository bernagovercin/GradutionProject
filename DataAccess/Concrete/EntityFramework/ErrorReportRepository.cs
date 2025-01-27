
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ErrorReportRepository : EfEntityRepositoryBase<ErrorReport, ProjectDbContext>, IErrorReportRepository
    {
        public ErrorReportRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
