using System.Linq;
using BOS.ClientLayer.ApplicationLayer.Monsters;
using BOS.Tests.Infrastructure.TestBases;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BOS.Tests.Application
{
    [TestClass]
    public class ExampleInteractorTests
        : IntegratedFor<ICreateMonsterInteractor>
    {
        [TestMethod]
        public void Call_When_AllIsWell()
        {
            Arrange(() => {});

            var result = Act(() => 
            {
                var request = new CreateMonsterRequest
                {
                    Name = "Rawrgnar",
                    Power = 99
                };
                return SUT.Call(request);
            });

            Assert(() => 
            {
                result.Success.Should().BeTrue("Success was incorrect");
                result.Result.Id.Should().NotBe(default(int), "Id was not assigned");
                result.Result.Name.Should().Be("Rawrgnar", "Name was incorrect");
                result.Result.Power.Should().Be(99, "Power was incorrect");
            });
        }

        [TestMethod]
        public void Call_When_NameIsMissing()
        {
            Arrange(() => {});

            var result = Act(() => 
            {
                var request = new CreateMonsterRequest
                {
                    Power = 99
                };
                return SUT.Call(request);
            });

            Assert(() => 
            {
                result.Success.Should().BeFalse("Success was incorrect");
                result.Messages.Any(x => x.Contains("Name")).Should().BeTrue("It should have returned error messages");
            });
        }

        [TestMethod]
        public void Call_When_PowerIsBad()
        {
            Arrange(() => {});

            var result = Act(() => 
            {
                var request = new CreateMonsterRequest
                {
                    Name = "Rawrgnar"
                };
                return SUT.Call(request);
            });

            Assert(() => 
            {
                result.Success.Should().BeFalse("Success was incorrect");
                result.Messages.Any(x => x.Contains("Power")).Should().BeTrue("It should have returned error messages");
            });
        }
    }
}