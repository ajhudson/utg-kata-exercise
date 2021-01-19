using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UtgKata.Lib.Tests
{
    public class PostCodeRegExTests
    {
        [Theory]
        [InlineData("PR7 5AQ", true)]
        [InlineData("M15 4LD", true)]
        [InlineData("NW13ED", true)]
        [InlineData("3873dsms", false)]
        public void ShouldValidateUkPostCodeCorrectly(string postCode, bool expectedToBeValid)
        {
            bool result = RegExHelper.IsRegExValid(RegExHelper.UkPostCodePattern, postCode);
            result.ShouldBe(expectedToBeValid);
        }
    }
}
