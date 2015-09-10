package logic;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;


public class FileManager {

    public boolean fileOrDirectoryExists(String path) {
        return Files.exists(Paths.get(path));
    }

    public String read(String filePath) throws IOException {
        byte[] encoded = Files.readAllBytes(Paths.get(filePath));
        return new String(encoded, StandardCharsets.UTF_8);
    }

    public void write(String filePath, String content) throws IOException {
        Files.write(Paths.get(filePath), content.getBytes(StandardCharsets.UTF_8));
    }

    public void delete(String filePath) throws IOException {
        Files.delete(Paths.get(filePath));
    }

    public void createDirectory(String directoryPath) throws IOException {
        if(fileOrDirectoryExists(directoryPath)) {
            Files.createDirectory(Paths.get(directoryPath));
        }
    }
   
}
