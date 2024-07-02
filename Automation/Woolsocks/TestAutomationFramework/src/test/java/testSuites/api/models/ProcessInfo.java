package testSuites.api.models;

public class ProcessInfo {
    String lastrun;
    String cpu_busy;
    String io_busy;
    String idle;
    String pack_received;
    String pack_sent;
    String connections;
    String pack_errors;
    String total_read;
    String total_write;
    String total_errors;

    public String getCpu_busy() {
        return cpu_busy;
    }
}
