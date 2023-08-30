using CommonTestSteps.Contracts;
using SeleniumCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestSteps.TestSteps.Fcos
{
    public class FcosTestSteps : GlobalTestSteps, IFcosTestSteps
    {
        public void GoToFCOS()
        {
            TestInMemoryParameters.Instance.Url = "https://app.fcos.com/";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }
    }
}
