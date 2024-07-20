package testSuites.api.models;

public class ImageData {
    String image_id;
    String sub_id;

    public ImageData(String image_id, String sub_id) {
        this.image_id = image_id;
        this.sub_id = sub_id;
    }

    public String getImageId() {
        return image_id;
    }

    public String getSubId() {
        return sub_id;
    }
}
