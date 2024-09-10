using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsQA.Drivers;
    

namespace MarsQA.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Initialize the WebDriver before each scenario
            CommonDriver.InitializeDriver("chrome");
        }

        [AfterScenario(Order =1)]
        public void AfterScenario()
        {
            // Clean up and close the WebDriver after each scenario
            CommonDriver.CloseDriver();
        }
    }

}
