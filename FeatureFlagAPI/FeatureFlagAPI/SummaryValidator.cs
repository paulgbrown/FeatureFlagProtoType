using FeatureFlagAPI.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.FeatureManagement;

namespace FeatureFlagAPI;

public interface ISummaryValidator
{
    Task<bool> Validate(ISummary dto, ModelStateDictionary modelState);
}

public class SummaryValidator : ISummaryValidator
{
    private readonly string RequireChannelAndUserInfo = "RequireChannelAndUserInfo";
    private readonly string AllowEmptySummary = "AllowEmptySummaryTimeWindow";

    private readonly IFeatureManager _featureManager;

    public SummaryValidator(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task<bool> Validate(ISummary dto, ModelStateDictionary modelState)
    {
        var allowEmptySummary = await _featureManager.IsEnabledAsync(AllowEmptySummary);

        if (!allowEmptySummary)
        {
            if (dto.Summary == "")
            {
                modelState.AddModelError("Summary", "The summary is required");
            }
        }

        return modelState.ErrorCount == 0;
    }
}
