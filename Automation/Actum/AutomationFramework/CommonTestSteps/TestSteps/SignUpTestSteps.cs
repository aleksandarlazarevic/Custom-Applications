using CommonTestSteps.Contracts;
using UIMappings.Pages;

namespace CommonTestSteps.TestSteps
{
    public class SignUpTestSteps : GlobalTestSteps, ISignUpTestSteps
    {
        public void SignUpToWebsite(string website, string username, string password)
        {
            switch (website)
            {
                case "demoblaze":
                    SignUpToDemoblaze(username, password);
                    break;
                default:
                    break;
            }
        }

        public void SignUpToDemoblaze(string email, string password)
        {
            GetPage<DemoblazeLogin>().WaitForPageReady()
                                .ClickOnSignUpLink()
                                .EnterCredentials(email, password)
                                .ClickOnSignUp();
        }
    }
}
