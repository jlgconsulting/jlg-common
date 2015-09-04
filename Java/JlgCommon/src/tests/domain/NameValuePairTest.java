package tests.domain;

import domain.Constants;
import domain.NameValuePair;
import junit.framework.TestCase;
import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Created by Dan on 04/09/2015.
 */
public class NameValuePairTest {

    @Test
    public void ConstructorsInitializePropertiesCorrectly(){

        String ownerId = "Dan Misailescu";
        NameValuePair<Integer, String> nvp1 = new NameValuePair<Integer, String>(4, "value3", ownerId);
        assertNotEquals(nvp1.Id, null);
        assertEquals(nvp1.Name, (Integer)4);
        assertEquals(nvp1.Value, "value3");
        assertEquals(nvp1.OwnerId, ownerId);

        NameValuePair<String, Integer> nvp2 = new NameValuePair<String, Integer>("name2", 5);
        assertNotEquals(nvp2.Id, null);
        assertEquals(nvp2.Name, "name2");
        assertEquals(nvp2.Value, (Integer)5);
        assertEquals(nvp2.OwnerId, null);

        NameValuePair<String, String> nvp3 = new NameValuePair<String, String>();
        assertNotEquals(nvp3.Id, null);
        assertEquals(nvp3.Name, null);
        assertEquals(nvp3.Value, null);
        assertEquals(nvp3.OwnerId, null);
    }
}