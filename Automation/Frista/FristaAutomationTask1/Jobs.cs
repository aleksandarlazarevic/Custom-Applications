using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages;
using SharedSteps;
using System;

namespace Tests
{
    [TestClass]
    public class Jobs
    {
        [TestMethod]
        public void FilterAllJobs()
        {
            JobSearchPage jobsPage = new JobSearchPage();
            GlobalSteps.OpenBrowser();
            GlobalSteps.GoToUrl(JobSearchPage.CareersUrl);
            jobsPage.ChooseCategoryAll().ClickSearchButton();
            GlobalSteps.CloseBrowser();
        }

        [TestMethod]
        public void FilterITSAPJobs()
        {
            JobSearchPage jobsPage = new JobSearchPage();
            GlobalSteps.OpenBrowser();
            GlobalSteps.GoToUrl(JobSearchPage.CareersUrl);
            jobsPage.ChooseCategoryITSAP().ClickSearchButton();
            GlobalSteps.CloseBrowser();
        }
    }
}
