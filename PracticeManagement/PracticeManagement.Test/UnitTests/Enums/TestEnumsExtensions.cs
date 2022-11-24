using PracticeManagement.Api.Utils;
using PracticeManagement.Dal.Enums;
using FluentAssertions;
using Xunit;
using NSubstitute.ExceptionExtensions;
using System;

namespace PracticeManagement.Test.UnitTests.Enums
{
    public class TestEnumsExtensions
    {

        [Theory]
        [InlineData(PracticeResult.None, PracticeResult.Approved)]
        [InlineData(PracticeResult.Approved, PracticeResult.Rejected)]
        public void PracticeResult_GetNewValue_ShouldReturnCorrectValue(PracticeResult current, PracticeResult next)
        {
            var newValue= current.GetNewValue();
            newValue.Should().Be(next);
        }

        [Theory]
        [InlineData(PracticeStatus.Created, PracticeStatus.InProgress)]
        [InlineData(PracticeStatus.InProgress, PracticeStatus.Completed)]
        public void PracticeStatus_GetNewValue_ShouldReturnCorrectValue(PracticeStatus current, PracticeStatus next)
        {
            var newValue = current.GetNewValue();
            newValue.Should().Be(next);
        }

        [Fact]
        public void PracticeStatus_GetNewValue_ShouldThrowExceptionWhenLastValue()
        {
            Func <PracticeStatus> getNewValueFunct = ()=> PracticeStatus.Completed.GetNewValue();
            getNewValueFunct.Should().Throw< ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PracticeResult_GetNewValue_ShouldThrowExceptionWhenLastValue()
        {
            Func<PracticeResult> getNewValueFunct = () => PracticeResult.Rejected.GetNewValue();
            getNewValueFunct.Should().Throw<ArgumentOutOfRangeException>();
        }

    }


}
