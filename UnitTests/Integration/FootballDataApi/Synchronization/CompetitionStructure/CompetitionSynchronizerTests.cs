using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using NUnit.Framework;
using Persistence.Abstract;
using Persistence.Entities;
using System.Linq.Expressions;
using AutoMapper;
using Integration;
using Integration.FootballDataOrgApi.FootballDataDto;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure;
using Integration.FootballDataOrgApi.Synchronization.CompetitionStructure.Abstract;
using Microsoft.Extensions.Logging;


namespace UnitTests.Integration.FootballDataApi.Synchronization.CompetitionStructure
{
    [TestFixture]
    public class CompetitionSynchronizerTests
    {
        private ICompetitionSynchronizer _competitionSynchronizer;
        private readonly Mock<IStratosphereUnitOfWork> _mockStratosUoW = new Mock<IStratosphereUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICompetitionSetupSynchronizer> _mockCompetitionSetupSynchronizer = new Mock<ICompetitionSetupSynchronizer>();
        private readonly Mock<ILogger<BaseSynchronizer>> _mockLogger = new Mock<ILogger<BaseSynchronizer>>();

        public CompetitionSynchronizerTests()
        {
            _mockStratosUoW.Setup(m => m.Competitions.Add(It.IsAny<Competition>())).Verifiable();
            _mockStratosUoW.Setup(m => m.Complete()).Verifiable();
            _mockCompetitionSetupSynchronizer.Setup(m => m.UpdateExisitingCompetitionSetup(It.IsAny<CompetitionDto>(), It.IsAny<long>()))
                .Verifiable();
            _mockCompetitionSetupSynchronizer.Setup(m => m.CreateCompetitionSetup(It.IsAny<CompetitionDto>(), It.IsAny<long>()))
                .Verifiable();
        }

        [Test]
        public void CreateUpdateCompetition_WithNoExistingCompetition_CreationInvoked()
        {
            //Arrange
            ResetMockCalls();
     
            var competitionDto = new CompetitionDto { };
            Competition competition = null;
            _mockStratosUoW.Setup(m => m.Competitions.SingleOrDefault(It.IsAny<Expression<Func<Competition, bool>>>()))
                .Returns(competition);

            _mockMapper.Setup(m => m.Map<CompetitionDto, Competition>(It.IsAny<CompetitionDto>()))
                .Returns(new Competition());

            //Act 
            _competitionSynchronizer = new CompetitionSynchronizer(_mockCompetitionSetupSynchronizer.Object, _mockStratosUoW.Object, 
                _mockMapper.Object, _mockLogger.Object);
            _competitionSynchronizer.CreateUpdateCompetition(competitionDto);

            //Assert 
            _mockStratosUoW.Verify(m => m.Competitions.Add(It.IsAny<Competition>()), Times.Once);
            _mockStratosUoW.Verify(m => m.Complete(), Times.Exactly(2));
            _mockCompetitionSetupSynchronizer.Verify(m => m.CreateCompetitionSetup(It.IsAny<CompetitionDto>(), It.IsAny<long>()), Times.Once);
            _mockCompetitionSetupSynchronizer.Verify(m => m.UpdateExisitingCompetitionSetup(It.IsAny<CompetitionDto>(), It.IsAny<long>()), Times.Never);
        }

        private void ResetMockCalls()
        {
            _mockCompetitionSetupSynchronizer.ResetCalls();
            _mockStratosUoW.ResetCalls();
            _mockCompetitionSetupSynchronizer.ResetCalls();
        }
    }
}
