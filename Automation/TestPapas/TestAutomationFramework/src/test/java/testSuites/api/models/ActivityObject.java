package testSuites.api.models;

public class ActivityObject {
    private int participants;
    private float price;
    private float accessibility;
    private String activity;
    private String type;
    private String link;
    private String key;

    public ActivityObject(int participants, float price, float accessibility) {
        this.participants = participants;
        this.price = price;
        this.accessibility = accessibility;
    }

    public int getParticipants() {
        return participants;
    }

    public float getPrice() {
        return price;
    }

    public float getAccessibility() {
        return accessibility;
    }

    public void setParticipants(int participants) {
        this.participants = participants;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    public void setAccessibility(float accessibility) {
        this.accessibility = accessibility;
    }

    public String getActivity() {
        return activity;
    }

    public void setActivity(String activity) {
        this.activity = activity;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getLink() {
        return link;
    }

    public void setLink(String link) {
        this.link = link;
    }

    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }
}
