package logic;

import extensions.DateExtensions;

import java.util.*;

public class RandomGenerator {
    private Random rand = new Random();

    public int getRandomIntBetween(int lowestInclusive, int largestExclusive) {
        return rand.nextInt(largestExclusive-lowestInclusive) + lowestInclusive;
    }

    public String getRandomString(int size) {
        StringBuilder randomString = new StringBuilder(10);
        for (int i = 0; i < size; i++) {
            randomString.append((char)getRandomIntBetween(97, 123));
        }
        return randomString.toString();
    }

    public List<Integer> getRandomIntList(int size, int minInclusive, int maxExclusive) {
        List<Integer> randList = new ArrayList<>();
        for (int i = 0; i < size; i++) {
            randList.add(getRandomIntBetween(minInclusive, maxExclusive));
        }
        return randList;
    }

    public List<Double> getRandomDoubleList(int size, int minInclusive, int maxExclusive) {
        List<Double> randList = new ArrayList<>();
        double one = 1;
        for (int i = 0; i < size; i++) {
            double randomDouble = getRandomIntBetween(minInclusive, maxExclusive) + (one / getRandomIntBetween(2, 50));
            randList.add(randomDouble);
        }
        return randList;
    }

    public List<UUID> getRandomUUIDList(int size) {
        List<UUID> randList = new ArrayList<UUID>();
        for (int i = 0; i < size; i++) {
            UUID uuid = UUID.randomUUID();
            randList.add(uuid);
        }
        return randList;
    }

    public Date getRandomDate(int startYearInclusive, int endYearExclusive) {
        int year = getRandomIntBetween(startYearInclusive, endYearExclusive);
        int month = getRandomIntBetween(1, 13);
        int day = getRandomIntBetween(1, 29);
        int hour = getRandomIntBetween(0, 24);
        int minute = getRandomIntBetween(0, 60);
        int second = getRandomIntBetween(0, 60);
        int millisecond = getRandomIntBetween(0, 1000);

        return DateExtensions.createDate(year, month, day, hour, minute, second, millisecond);
    }

    public List<Date> getRandomDateList(int size, int startYearInclusive, int endYearExclusive){
        List<Date> randList = new ArrayList<Date>();
        for (int i = 0; i < size; i++) {
            Date randDate = getRandomDate(startYearInclusive, endYearExclusive);
            randList.add(randDate);
        }
        return randList;
    }
}
