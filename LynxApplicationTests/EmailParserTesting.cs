using System.Linq;
using Lynx.Application.Common.Extensions;
using Lynx.Application.Handlers.Commands.EmailWorkerCmds;
using Lynx.Commands.EmailWorkerCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasqR;

namespace LynxApplicationTests
{
    [TestClass]
    public class EmailParserTesting
    {
        [TestMethod]
        public void CanParseGlobeEmail()
        {
            using (var services = TestServiceProvider.InMemoryContext())
            {
                #region Arrange
                var dbContext = services.GetService<ILynxDbContext>();
                var tasqR = services.GetService<ITasqR>();

                Email email = new Email
                {
                    ID = 1,
                    N_Body = new EmailBody
                    {
                        Html = "<label>Amount: </label> <label>31.00</label>",
                        Text = "Amount: 31.00"
                    }
                };

                dbContext.Emails.Add(email);

                dbContext.SaveChanges();
                #endregion

                #region Act
                var userBill = tasqR.UsingAsHandler(typeof(ReadUserBillFromGlobeEmailCmdHandler))
                    .Run(new ReadUserBillFromEmailCmd(1));
                #endregion

                #region Assert
                Assert.AreEqual(31, userBill.Amount); 
                #endregion
            }
        }
    }
}
