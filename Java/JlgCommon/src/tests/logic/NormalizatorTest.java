package tests.logic;

import logic.Normalizator;
import logic.RandomGenerator;
import org.junit.*;
import static org.junit.Assert.*;

import java.util.List;

public class NormalizatorTest {

    private RandomGenerator randomGenerator;
    private Normalizator normalizator;

    @Before
    public void setUp() {
        randomGenerator = new RandomGenerator();
        normalizator = new Normalizator();
    }

    @Test
    public void NormalizeValues() {
        for (int i = 0; i < 70; i++)
        {
            List<Double> randDoubleList = randomGenerator.getRandomDoubleList(100, -300, 900);
            List<Double> normalizedDoubleList1 = normalizator.normalizeValues(randDoubleList, 0, 1);
            assertEquals(randDoubleList.size(), normalizedDoubleList1.size());
            for (double normalizedNr : normalizedDoubleList1) {
                assertTrue(normalizedNr >= 0 && normalizedNr <= 1);
            }

            List<Double> normalizedDoubleList2 = normalizator.normalizeValues(randDoubleList, 50, 7000);
            assertEquals(randDoubleList.size(), normalizedDoubleList2.size());
            for (double normalizedNr : normalizedDoubleList2) {
                assertTrue(normalizedNr >= 50 && normalizedNr <= 7000);
            }

            List<Double> normalizedDoubleList3 = normalizator.normalizeValues(randDoubleList, -22, 19);
            assertEquals(randDoubleList.size(), normalizedDoubleList3.size());
            for (double normalizedNr : normalizedDoubleList3) {
                assertTrue(normalizedNr >= -22 && normalizedNr <= 19);
            }
        }
    }
}
