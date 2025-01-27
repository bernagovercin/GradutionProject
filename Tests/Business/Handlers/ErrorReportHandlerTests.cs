
using Business.Handlers.ErrorReports.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ErrorReports.Queries.GetErrorReportQuery;
using Entities.Concrete;
using static Business.Handlers.ErrorReports.Queries.GetErrorReportsQuery;
using static Business.Handlers.ErrorReports.Commands.CreateErrorReportCommand;
using Business.Handlers.ErrorReports.Commands;
using Business.Constants;
using static Business.Handlers.ErrorReports.Commands.UpdateErrorReportCommand;
using static Business.Handlers.ErrorReports.Commands.DeleteErrorReportCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ErrorReportHandlerTests
    {
        Mock<IErrorReportRepository> _errorReportRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _errorReportRepository = new Mock<IErrorReportRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ErrorReport_GetQuery_Success()
        {
            //Arrange
            var query = new GetErrorReportQuery();

            _errorReportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorReport, bool>>>())).ReturnsAsync(new ErrorReport()
//propertyler buraya yazılacak
//{																		
//ErrorReportId = 1,
//ErrorReportName = "Test"
//}
);

            var handler = new GetErrorReportQueryHandler(_errorReportRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ErrorReportId.Should().Be(1);

        }

        [Test]
        public async Task ErrorReport_GetQueries_Success()
        {
            //Arrange
            var query = new GetErrorReportsQuery();

            _errorReportRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ErrorReport, bool>>>()))
                        .ReturnsAsync(new List<ErrorReport> { new ErrorReport() { /*TODO:propertyler buraya yazılacak ErrorReportId = 1, ErrorReportName = "test"*/ } });

            var handler = new GetErrorReportsQueryHandler(_errorReportRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ErrorReport>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ErrorReport_CreateCommand_Success()
        {
            ErrorReport rt = null;
            //Arrange
            var command = new CreateErrorReportCommand();
            //propertyler buraya yazılacak
            //command.ErrorReportName = "deneme";

            _errorReportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorReport, bool>>>()))
                        .ReturnsAsync(rt);

            _errorReportRepository.Setup(x => x.Add(It.IsAny<ErrorReport>())).Returns(new ErrorReport());

            var handler = new CreateErrorReportCommandHandler(_errorReportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorReportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ErrorReport_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateErrorReportCommand();
            //propertyler buraya yazılacak 
            //command.ErrorReportName = "test";

            _errorReportRepository.Setup(x => x.Query())
                                           .Returns(new List<ErrorReport> { new ErrorReport() { /*TODO:propertyler buraya yazılacak ErrorReportId = 1, ErrorReportName = "test"*/ } }.AsQueryable());

            _errorReportRepository.Setup(x => x.Add(It.IsAny<ErrorReport>())).Returns(new ErrorReport());

            var handler = new CreateErrorReportCommandHandler(_errorReportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ErrorReport_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateErrorReportCommand();
            //command.ErrorReportName = "test";

            _errorReportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorReport, bool>>>()))
                        .ReturnsAsync(new ErrorReport() { /*TODO:propertyler buraya yazılacak ErrorReportId = 1, ErrorReportName = "deneme"*/ });

            _errorReportRepository.Setup(x => x.Update(It.IsAny<ErrorReport>())).Returns(new ErrorReport());

            var handler = new UpdateErrorReportCommandHandler(_errorReportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorReportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ErrorReport_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteErrorReportCommand();

            _errorReportRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ErrorReport, bool>>>()))
                        .ReturnsAsync(new ErrorReport() { /*TODO:propertyler buraya yazılacak ErrorReportId = 1, ErrorReportName = "deneme"*/});

            _errorReportRepository.Setup(x => x.Delete(It.IsAny<ErrorReport>()));

            var handler = new DeleteErrorReportCommandHandler(_errorReportRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _errorReportRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

