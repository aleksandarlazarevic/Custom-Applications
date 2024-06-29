package common.utilities;

import common.core.TestInMemoryParameters;
import org.apache.commons.io.IOUtils;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.Enumeration;
import java.util.zip.ZipEntry;
import java.util.zip.ZipException;
import java.util.zip.ZipFile;

public class FileManager {
    public static File[] getFilesWithExtension(String dirPath, String extension) {
        File dir = new File(dirPath);
        File[] fileList = null;

        try {
            fileList = dir.listFiles(new FilenameFilter() {
                public boolean accept(File dir, String name) {
                    return name.endsWith(extension);
                }
            });
        } catch (Exception exception) {
            throw new RuntimeException(
                    String.format("Unable to locate file with extension: %s in: %s - ",
                            extension, dir, exception.getMessage()));
        }

        return fileList;
    }
}
