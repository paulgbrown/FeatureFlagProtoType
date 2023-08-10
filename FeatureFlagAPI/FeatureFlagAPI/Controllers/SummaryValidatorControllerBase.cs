using Microsoft.AspNetCore.Mvc;

namespace FeatureFlagAPI.Controllers;

public class SummaryValidatorControllerBase : ControllerBase
{
    protected readonly ISummaryValidator _summaryValidator;

    public SummaryValidatorControllerBase(ISummaryValidator summaryValidator)
    {
        _summaryValidator = summaryValidator;
    }
}
