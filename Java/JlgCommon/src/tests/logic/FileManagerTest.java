package tests.logic;

import logic.FileManager;
import logic.Normalizator;
import logic.RandomGenerator;
import org.junit.*;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

import static org.junit.Assert.*;

public class FileManagerTest {

    private FileManager fileManager;
    private String authorFileName = "author";
    private String testDirectoryName = "testfileManager";

    @Before
    public void setUp() {
        fileManager = new FileManager();
    }

    @Test
    public void Write_Read_Delete()
    {
        try {
            fileManager.write(authorFileName, "Dan Misailescu + unicode text: române?te");
        }catch (IOException ex){
            fail();
        }

        try {
            String authorFileContent = fileManager.read(authorFileName);
            assertEquals(authorFileContent, "Dan Misailescu + unicode text: române?te");
        }catch (IOException ex){
            fail();
        }

        try {
            fileManager.delete(authorFileName);
            assertFalse(fileManager.fileOrDirectoryExists(authorFileName));
        }catch (IOException ex){
            fail();
        }
    }
}
