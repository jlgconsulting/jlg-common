package tests.domain;

import domain.NameValuePair;
import org.junit.Test;

import static org.junit.Assert.*;

public class NameValuePairTest {

    @Test
    public void ConstructorsInitializePropertiesCorrectly(){

        String ownerId = "Dan Misailescu";
        NameValuePair<Integer, String> nvp1 = new NameValuePair<>(4, "value3", ownerId);
        assertNotEquals(nvp1.id, null);
        assertEquals(nvp1.name, (Integer)4);
        assertEquals(nvp1.value, "value3");
        assertEquals(nvp1.ownerId, ownerId);

        NameValuePair<String, Integer> nvp2 = new NameValuePair<>("name2", 5);
        assertNotEquals(nvp2.id, null);
        assertEquals(nvp2.name, "name2");
        assertEquals(nvp2.value, (Integer)5);
        assertEquals(nvp2.ownerId, null);

        NameValuePair<String, String> nvp3 = new NameValuePair<>();
        assertNotEquals(nvp3.id, null);
        assertEquals(nvp3.name, null);
        assertEquals(nvp3.value, null);
        assertEquals(nvp3.ownerId, null);
    }
}