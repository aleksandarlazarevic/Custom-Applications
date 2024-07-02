package common.core;

public class EmailServiceParameters {
    public String name;
    public String url;

    public EmailServiceParameters(String name, String url) {
        this.name = name;
        this.url = url;
    }

    public String getName() {
        return name;
    }

    public String getUrl() {
        return url;
    }
}
