using DossierManagement.Api.Utils;
using DossierManagement.Dal.Enums;
using FluentAssertions;
using Xunit;
using NSubstitute.ExceptionExtensions;
using System;

namespace DossierManagement.Test.UnitTests.Enums
{
    public class TestEnumsExtensions
    {

        [Theory]
        [InlineData(DossierResult.None, DossierResult.Approved)]
        [InlineData(DossierResult.Approved, DossierResult.Rejected)]
        public void DossierResult_GetNewValue_ShouldReturnCorrectValue(DossierResult current, DossierResult next)
        {
            var newValue = current.GetNewValue();
            newValue.Should().Be(next);
        }

        [Theory]
        [InlineData(DossierStatus.Created, DossierStatus.InProgress)]
        [InlineData(DossierStatus.InProgress, DossierStatus.Completed)]
        public void DossierStatus_GetNewValue_ShouldReturnCorrectValue(DossierStatus current, DossierStatus next)
        {
            var newValue = current.GetNewValue();
            newValue.Should().Be(next);
        }

        [Fact]
        public void DossierStatus_GetNewValue_ShouldThrowExceptionWhenLastValue()
        {
            Func<DossierStatus> getNewValueFunct = () => DossierStatus.Completed.GetNewValue();
            getNewValueFunct.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void DossierResult_GetNewValue_ShouldThrowExceptionWhenLastValue()
        {
            Func<DossierResult> getNewValueFunct = () => DossierResult.Rejected.GetNewValue();
            getNewValueFunct.Should().Throw<ArgumentOutOfRangeException>();
        }

    }


}
