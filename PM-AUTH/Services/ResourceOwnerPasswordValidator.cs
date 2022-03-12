using IdentityModel;
using IdentityServer4.Validation;
using PM_DAL.Interface;

namespace PM_AUTH.Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            bool isValid = await _userRepository.ValidateCredentialsAsync(context.UserName, context.Password);

            if (isValid)
            {
                var user = _userRepository.FindByUsernameAsync(context.UserName);

                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
