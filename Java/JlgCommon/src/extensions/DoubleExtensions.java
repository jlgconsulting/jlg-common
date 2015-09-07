package extensions;
import java.text.DecimalFormat;
/**
 * Created by Dan on 07/09/2015.
 */
public final class DoubleExtensions {

    public static double roundToDecimals(double nr, int decimals){

        StringBuilder decimalFormatSb = new StringBuilder();
        decimalFormatSb.append("0.");
        for (int i=0; i<decimals; i++) {
            decimalFormatSb.append("0");
        }
        DecimalFormat df = new DecimalFormat(decimalFormatSb.toString());
        String formatedDouble = df.format(nr);
        return Double.valueOf(formatedDouble);
    }

    public static double differenceInPercents(double nr1, double nr2)
    {
        if (Math.abs(nr2) > 0) {
            return roundToDecimals(nr1 * 100 / nr2 - 100, 2);
        }
        else {
            return Double.NaN;
        }
    }

    public static double percentValue(double nr, double percent)
    {
        double percentValueFromNr = (percent / 100) * nr;
        return percentValueFromNr;
    }

    public static double increaseByPercent(double nr, double percent)
    {
        return nr + percentValue(nr, percent);
    }

    private DoubleExtensions(){
    }
}
