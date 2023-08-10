using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.FeatureManagement;
using Moq;

namespace FeatureFlagAPI.UnitTests;

public class GivenEmptySummaryIsAllowed
{
    [Fact]
    public async Task ValidateReturnsTrue()
    {
        var featureManagerMock = new Mock<IFeatureManager>();
        featureManagerMock.Setup(x => x.IsEnabledAsync(It.IsAny<string>())).ReturnsAsync(true);

        var dto = new WeatherForecastDTO()
        {
            Summary = ""
        };
        var modelState = new ModelStateDictionary();

        var target = new SummaryValidator(featureManagerMock.Object);

        var actual = await target.Validate(dto, modelState);

        Assert.True(actual);
    }
}

public class GivenEmptySummaryIsNotAllowed
{
    [Fact]
    public async Task ValidateReturnsFalseAndAnErrorIsAdded()
    {
        var featureManagerMock = new Mock<IFeatureManager>();
        featureManagerMock.Setup(x => x.IsEnabledAsync(It.IsAny<string>())).ReturnsAsync(false);

        var dto = new WeatherForecastDTO()
        {
            Summary = ""
        };

        var modelState = new ModelStateDictionary();

        var target = new SummaryValidator(featureManagerMock.Object);

        var actual = await target.Validate(dto, modelState);

        Assert.False(actual);
        Assert.Equal(1, modelState.ErrorCount);
    }
}
