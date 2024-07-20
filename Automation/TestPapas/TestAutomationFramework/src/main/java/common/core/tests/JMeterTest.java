package common.core.tests;

public class JMeterTest {
    public String testDesc;
    public String domain;
    public String path;
    public String setMethod;
    public int noOfUsers;
    public int rampUp;
    public int duration;

    public JMeterTest(String testDesc, String domain, String path, String setMethod, int noOfUsers, int rampUp, int duration) {
        this.testDesc = testDesc;
        this.domain = domain;
        this.path = path;
        this.setMethod = setMethod;
        this.noOfUsers = noOfUsers;
        this.rampUp = rampUp;
        this.duration = duration;
    }
}
