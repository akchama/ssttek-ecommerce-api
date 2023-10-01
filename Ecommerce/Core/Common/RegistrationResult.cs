namespace Ecommerce.Core.Common;

public class RegistrationResult
{
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }

    private RegistrationResult() { }

    public static RegistrationResult Success() => new RegistrationResult { IsSuccess = true };
    public static RegistrationResult Failure(string errorMessage) => new RegistrationResult { IsSuccess = false, ErrorMessage = errorMessage };
}