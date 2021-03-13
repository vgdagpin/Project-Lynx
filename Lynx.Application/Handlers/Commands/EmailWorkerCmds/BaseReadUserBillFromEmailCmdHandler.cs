using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Commands.EmailWorkerCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.EmailWorkerCmds
{
    public class BaseReadUserBillFromEmailCmdHandler : TasqHandler<ReadUserBillFromEmailCmd, UserBillVM>
    {
        protected DbContext m_DbContext;
        protected DbSet<Email> m_Emails;

        protected BaseReadUserBillFromEmailCmdHandler() { }

        public BaseReadUserBillFromEmailCmdHandler(ILynxDbContext dbContext)
        {
            m_DbContext = (DbContext)dbContext;
            m_Emails = (DbSet<Email>)dbContext.Emails;
        }

        protected virtual string ReadEmail(long emailID, bool asHTML = false)
        {
            var email = m_Emails
                .Include(a => a.N_Body)
                .AsNoTracking()
                .SingleOrDefault(a => a.ID == emailID);

            if (email == null)
            {
                return null;
            }

            return asHTML ? email.N_Body.Html : email.N_Body.Text;
        }

        public override UserBillVM Run(ReadUserBillFromEmailCmd request)
        {
            // override this per type of email worker
            throw new NotImplementedException();
        }
    }
}
