package common.utilities;

import common.core.TestInMemoryParameters;
import common.core.tests.JMeterTest;
import org.apache.jmeter.config.Arguments;
import org.apache.jmeter.config.gui.ArgumentsPanel;
import org.apache.jmeter.control.LoopController;
import org.apache.jmeter.control.gui.LoopControlPanel;
import org.apache.jmeter.control.gui.TestPlanGui;
import org.apache.jmeter.engine.StandardJMeterEngine;
import org.apache.jmeter.modifiers.UserParameters;
import org.apache.jmeter.modifiers.gui.UserParametersGui;
import org.apache.jmeter.protocol.http.control.gui.HttpTestSampleGui;
import org.apache.jmeter.protocol.http.sampler.HTTPSamplerProxy;
import org.apache.jmeter.reporters.ResultCollector;
import org.apache.jmeter.save.SaveService;
import org.apache.jmeter.testelement.TestElement;
import org.apache.jmeter.testelement.TestPlan;
import org.apache.jmeter.testelement.property.BooleanProperty;
import org.apache.jmeter.testelement.property.CollectionProperty;
import org.apache.jmeter.threads.ThreadGroup;
import org.apache.jmeter.threads.gui.ThreadGroupGui;
import org.apache.jmeter.util.JMeterUtils;
import org.apache.jmeter.visualizers.SummaryReport;
import org.apache.jorphan.collections.HashTree;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.LinkedList;

public class JMeterManager {
    private static StandardJMeterEngine jMeterEngine;

    public static void setupJMeter() {
        jMeterEngine = new StandardJMeterEngine();
        String jMeterLocation = "C:\\apache-jmeter-5.6.3\\bin";

        JMeterUtils.setJMeterHome(new File(jMeterLocation).getPath());
        JMeterUtils.loadJMeterProperties(new File(jMeterLocation + "\\jmeter.properties").getPath());
        JMeterUtils.initLocale();
        JMeterUtils.initLogging();
    }

    public static void setJMeterParametersAndRun(JMeterTest testParameters) {
        HashTree testPlanTree = new HashTree();
        HTTPSamplerProxy sampler = new HTTPSamplerProxy();
        sampler.setDomain(testParameters.domain);
        sampler.setPort(8888);
        sampler.setPath(testParameters.path);
        sampler.setMethod(testParameters.setMethod);
        sampler.setName(testParameters.testDesc);
        sampler.setProperty(TestElement.TEST_CLASS, HTTPSamplerProxy.class.getName());
        sampler.setProperty(TestElement.GUI_CLASS, HttpTestSampleGui.class.getName());

        UserParameters userParameters = new UserParameters();
        userParameters.setName("User Parameters");
        userParameters.setProperty(new BooleanProperty("UserParameters.per_iteration", false));
        userParameters.setProperty(new CollectionProperty("UserParameters.names", new LinkedList<>()));
        userParameters.setProperty(new CollectionProperty("UserParameters.thread_values", new LinkedList<>()));
        userParameters.setProperty(TestElement.TEST_CLASS, UserParameters.class.getName());
        userParameters.setProperty(UserParameters.GUI_CLASS, UserParametersGui.class.getName());

        LoopController loopController = new LoopController();
        loopController.setLoops(1);
        loopController.setFirst(true);
        loopController.setProperty(TestElement.TEST_CLASS, LoopController.class.getName());
        loopController.setProperty(TestElement.GUI_CLASS, LoopControlPanel.class.getName());
        loopController.initialize();

        ThreadGroup threadGroup = new ThreadGroup();
        threadGroup.setName(testParameters.testDesc + " Thread Group");
        threadGroup.setNumThreads(testParameters.noOfUsers);
        threadGroup.setRampUp(testParameters.rampUp);
        threadGroup.setScheduler(true);
        threadGroup.setDuration(testParameters.duration);
        threadGroup.setSamplerController(loopController);
        threadGroup.setProperty(TestElement.TEST_CLASS, ThreadGroup.class.getName());
        threadGroup.setProperty(TestElement.GUI_CLASS, ThreadGroupGui.class.getName());

        TestPlan testPlan = new TestPlan(testParameters.testDesc + " - Test Plan");
        testPlan.setProperty(TestElement.TEST_CLASS, TestPlan.class.getName());
        testPlan.setProperty(TestElement.GUI_CLASS, TestPlanGui.class.getName());
        testPlan.setUserDefinedVariables((Arguments) new ArgumentsPanel().createTestElement());
        testPlanTree.add(testPlan);
        HashTree threadGroupHashTree = testPlanTree.add(testPlan, threadGroup);
        threadGroupHashTree.add(sampler, userParameters);
        String testResultsDir = TestInMemoryParameters.getInstance().getTestResultsDirectory();

        try {
            SaveService.saveTree(testPlanTree, new FileOutputStream( testResultsDir + testParameters.testDesc + "testPlan.jmx"));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        String logFile = testResultsDir + testParameters.testDesc + "JMeterResult.jtl";
        ResultCollector logger = new ResultCollector();
        logger.setFilename(logFile);
        testPlanTree.add(testPlanTree.getArray()[0], logger);
        SummaryReport summary = new SummaryReport();
        testPlanTree.add(testPlanTree.getArray()[0], summary);

        runJmeterTest(testParameters, testPlanTree, logFile);
    }

    private static void runJmeterTest(JMeterTest testParameters, HashTree testPlanTree, String logFile) {
        try {
            jMeterEngine.configure(testPlanTree);
            jMeterEngine.run();
            LoggingManager.Info(String.format("Finished performance test for: %s. Results can be found here: %s", testParameters.testDesc, logFile));
            System.exit(0);
        } catch (Exception ex) {
            throw new RuntimeException(ex.getMessage());
        }
    }
}
