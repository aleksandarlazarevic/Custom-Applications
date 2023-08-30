using CommonTestSteps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class CommonTestSteps : BaseTest
    {
        [Given(@"The website (.*) is started")]
        public void GivenTheWebsiteIsStarted(string website)
        {
            switch (website)
            {
                case "Fcos":
                    RunStep(SharedSteps.Containers.Fcos.GoToFCOS,
                            new TestStepInfo("[BROWSER] - Go to FCOS", false, Importance.High));
                    break;
                case "FcosAzure":
                    RunStep(SharedSteps.Containers.FcosAzure.GoToFCOSAzure,
                            new TestStepInfo("[BROWSER] - Go to FCOS Azure", false, Importance.High));
                    break;
                case "FranchiCzar":
                    RunStep(SharedSteps.Containers.FranchiCzar.GoToFranchiCzar,
                            new TestStepInfo("[BROWSER] - Go to FranchiCzar", false, Importance.High));
                    break;
                case "Iron24":
                    RunStep(SharedSteps.Containers.Iron24.GoToIron24,
                            new TestStepInfo("[BROWSER] - Go to Iron24", false, Importance.High));
                    break;
                case "MathReactor":
                    RunStep(SharedSteps.Containers.MathReactor.GoToMathreactor,
                            new TestStepInfo("[BROWSER] - Go to MathReactor", false, Importance.High));
                    break;
                case "Nael":
                    RunStep(SharedSteps.Containers.Nael.GoToNael,
                            new TestStepInfo("[BROWSER] - Go to Nael", false, Importance.High));
                    break;
                case "Valhallan":
                    RunStep(SharedSteps.Containers.Valhallan.GoToValhallan,
                            new TestStepInfo("[BROWSER] - Go to Valhallan", false, Importance.High));
                    break;
                default:
                    RunStep(SharedSteps.Containers.Global.GoToUrl, 
                            website, 
                            new TestStepInfo("[BROWSER] - Go to Valhallan", false, Importance.High));
                    break;
            }
        }

        [When(@"(.*) start page appears")]
        public void WhenStartPageAppears(string website)
        {
            switch (website)
            {
                case "Fcos":
                    RunStep(SharedSteps.Containers.Fcos.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if FCOS first page loaded", false, Importance.High));
                    break;
                case "FcosAzure":
                    RunStep(SharedSteps.Containers.FcosAzure.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if FCOS Azure first page loaded", false, Importance.High));
                    break;
                case "FranchiCzar":
                    RunStep(SharedSteps.Containers.FranchiCzar.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if FranchiCzar first page loaded", false, Importance.High));
                    break;
                case "Iron24":
                    RunStep(SharedSteps.Containers.Iron24.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if Iron24 first page loaded", false, Importance.High));
                    break;
                case "MathReactor":
                    RunStep(SharedSteps.Containers.MathReactor.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if MathReactor first page loaded", false, Importance.High));
                    break;
                case "Nael":
                    RunStep(SharedSteps.Containers.Nael.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if Nael first page loaded", false, Importance.High));
                    break;
                case "Valhallan":
                    RunStep(SharedSteps.Containers.Valhallan.ValidateFirstPageLoaded,
                            new TestStepInfo("[APP] - Check if Valhallan first page loaded", false, Importance.High));
                    break;
                default:
                    break;
            }
        }
    }
}
