using System;
using UnityEngine;
using UnityEngine.UI;

public class Constants
{
    public const float valueCap = 10f;
    public const float doublevalueCap = valueCap * 2f;
    public const float valueCapSqrd = valueCap * valueCap;
}

[System.Serializable]
public class LargeNumber
{
    public float value;
    public int exponent;
    public bool minus = false;

    public LargeNumber()
    {
        this.value = 0;
        this.exponent = 0;
    }

    public LargeNumber(float value, int exponent = 0)
    {
        this.value = value;
        this.exponent = exponent;
    }
    public override string ToString()
    {
        string name = GetName();
        if (name == "NONE")
        {
            return value.ToString();
        }
        if (minus)
            return -value + " " + name;
        return value + " " + name;
    }
    public string ToEngeneeringString()
    {
        float multiplValue = value;
        int exponentRemainder = Mathf.FloorToInt(exponent % 3);
        int tempExponent = Mathf.FloorToInt((exponent - exponentRemainder) / 3);

        string result = "";

        if (value == 0 && exponent == 0)
        {
            return 0.ToString();
        }



        if (exponentRemainder <= 2 && exponentRemainder != 0)
        {
            multiplValue = value * Mathf.Pow(10, exponentRemainder);
        }

        string name = ((LargeNumbers)tempExponent).ToString();

        if (name == "NONE")
        {
            return multiplValue.ToString("0.0");
        }

        if (minus)
            result = string.Format("{0} {1}", (-multiplValue).ToString("0.0"), name);
        result = string.Format("{0} {1}", multiplValue.ToString("0.0"), name);

        return result;
    }
    public string ToStringNumber()
    {
        string result = value.ToString();

        for (int i = 0; i < exponent; i++)
        {
            result += "0";
        }
        return result;
    }
    public string GetName()
    {
        int lnInt = exponent / 3;
        LargeNumbers ln = (LargeNumbers)lnInt;
        return ln.ToString();
    }
    public float divideByExponent(float value, int exponent)
    {

        if (float.IsNaN(value))
            return 0f;

        float result = value / Mathf.Pow(10, exponent);

        return float.IsNegativeInfinity(result) || float.IsInfinity(result) ? 0 : result;
    }
    public float divideByExponent(LargeNumber ln)
    {

        LargeNumber power = new LargeNumber(10, exponent);
        float result = value / Mathf.Pow(10, exponent);
        return float.IsNegativeInfinity(result) ? 0 : result;
    }
    private void increaseNumber(LargeNumber ln)
    {
        int largerExp = 0;
        int smallerExp = 0;

        if (this.isZero() && ln.isZero())
        {
            return;
        }
        if (this.exponent == ln.exponent)
        {
            increaseValue(ln.value);
            return;
        }
        if (this.exponent == 0 || ln.exponent == 0)
        {
            if (this.exponent > ln.exponent)
            {
                this.increaseValue(divideByExponent(ln.value, this.exponent));
            }
            else
            {
                this.exponent = ln.exponent;
                float val = divideByExponent(this.value, ln.exponent);
                this.value = ln.value;
                this.increaseValue(val);
            }

            return;
        }
        int expDifference = Math.Abs(ln.exponent - this.exponent);

        if (this.exponent < ln.exponent)
        {
            this.exponent = ln.exponent;
        }

        this.increaseValue(divideByExponent(ln.value, expDifference == 0 ? 1 : expDifference));
    }
    public bool isZero()
    {
        return this.value == 0 && this.exponent == 0;
    }
    public double toDouble()
    {
        double res = value;

        res *= Math.Pow(10.0, (double)exponent);

        return res;
    }

    public static LargeNumber decreaseNumber(LargeNumber ln1, LargeNumber ln2)
    {
        LargeNumber value1 = new LargeNumber(ln1.value);
        LargeNumber value2 = new LargeNumber(ln2.value);
        LargeNumber exponent1 = new LargeNumber(10, ln1.exponent);
        LargeNumber exponent2 = new LargeNumber(10, ln2.exponent);

        int expdiff = ln2.exponent - ln1.exponent;
        if (ln2.exponent > ln1.exponent)
        {
            value2 *= new LargeNumber(10, expdiff);
            exponent1.exponent -= expdiff;

            value1 -= value2;
            value1 *= exponent1;
        }
        else
        {
            value1 *= new LargeNumber(10, expdiff);
            exponent2.exponent -= expdiff;

            value2 -= value1;
            value2 *= exponent2;
        }

        return value1;
    }
    private void decreaseNumber(LargeNumber ln)
    {
        int largerExp = 0;
        int smallerExp = 0;

        if (this.isZero() && ln.isZero())
        {
            return;
        }
        if (this.exponent == ln.exponent)
        {
            decreaseValue(ln.value);
            return;
        }
        if (this.exponent == 0 || ln.exponent == 0)
        {
            if (this.exponent > ln.exponent)
            {
                this.decreaseValue(divideByExponent(ln.value, this.exponent));
            }
            else
            {
                this.exponent -= ln.exponent;

                float val = divideByExponent(this.value, ln.exponent);
                this.value = ln.value;
                this.decreaseValue(val);
            }

            return;
        }
        int expDifference = Math.Abs(ln.exponent - this.exponent);


        if (ln.exponent > this.exponent)
        {
            this.decreaseValue(ln.value * Mathf.Pow(10, expDifference));
            this.minus = true;
            this.exponent = ln.exponent;
        }
        else if (this.exponent > ln.exponent)
        {
            LargeNumber value1 = new LargeNumber(this.value);
            LargeNumber value2 = new LargeNumber(ln.value);
            LargeNumber exponent1 = new LargeNumber(10, this.exponent);
            LargeNumber exponent2 = new LargeNumber(10, ln.exponent);

            int expdiff = this.exponent - ln.exponent;
            if (this.exponent > ln.exponent)
            {
                value2 *= new LargeNumber(10, expdiff);
                exponent1.exponent -= expdiff;

                value1 -= value2;
                value1 *= exponent1;
            }

        }

    }

    private void increaseValue(float value)
    {
        float val = this.value + value;
        int exp = this.exponent;

        if (val >= Constants.valueCap && val < Constants.valueCapSqrd)
        {
            exp++;
            Debug.Log("VALP: " + val);
            val = val / Constants.valueCap;

            if (val < 1)
            {

                val *= Constants.valueCap;
                exp--;
            }
        }
        else if (val >= Constants.valueCapSqrd)
        {
            int numOfIncrements = Mathf.FloorToInt(Mathf.Log10(val));
            Debug.Log("Increments: " + numOfIncrements);
            exp += numOfIncrements;
            val = divideByExponent(val, numOfIncrements);

        }

        while (val < 1f && val != 0)
        {
            val *= 10f;
            if (exp == 0)
            {
                exp--;
                minus = true;
            }
            else
            {
                exp -= minus ? -1 : 1;

            }


        }
        this.exponent = exp;
        this.value = val;
    }
    private void decreaseValue(float value, bool isExpDecrease = true)
    {
        float val = this.value - value;
        int exp = this.exponent;

        if (val == 0 && exp == 0)
        {
            this.value = val;
            this.exponent = exp;

            return;
        }

        if (val <= 0.0f && val > -Constants.valueCap)
        {
            if (isExpDecrease)
                exp--;
            val = -(val % Constants.valueCap);
        }
        else if (val <= -Constants.valueCap)
        {
            int numOfDecrements = Mathf.FloorToInt(Mathf.Abs(Mathf.Log10(-val)));
            if (isExpDecrease)
                exponent -= numOfDecrements;

            Debug.Log(Mathf.Log10(-val));
            val = divideByExponent(-val, numOfDecrements);
        }


        while (val < 1f && val != 0)
        {
            val *= 10f;
            if (exp == 0)
            {
                exp--;
                minus = true;
            }
            else
            {
                exp -= minus ? -1 : 1;

            }


        }
        this.value = val;
        this.exponent = exp;
    }

    public static bool operator ==(LargeNumber ln1, LargeNumber ln2)
    {
        return ((ln1.exponent == ln2.exponent) && (ln1.value == ln2.value));
    }

    public static bool operator !=(LargeNumber ln1, LargeNumber ln2)
    {
        return !(ln1 == ln2);
    }

    public static bool operator >(LargeNumber ln1, LargeNumber ln2)
    {
        if (ln1.exponent == ln2.exponent)
            return ln1.value > ln2.value;

        return ln1.exponent > ln2.exponent;
    }

    public static bool operator <(LargeNumber ln1, LargeNumber ln2)
    {
        return !(ln1 > ln2);
    }

    public static bool operator >=(LargeNumber ln1, LargeNumber ln2)
    {
        return (ln1 > ln2) || (ln1 == ln2);
    }

    public static bool operator <=(LargeNumber ln1, LargeNumber ln2)
    {
        return (ln1 < ln2) || (ln1 == ln2);
    }

    public static LargeNumber operator +(LargeNumber ln1, LargeNumber ln2)
    {
        LargeNumber largeNumber = new LargeNumber(ln1.value, ln1.exponent);

        largeNumber.increaseNumber(ln2);

        return largeNumber;
    }
    public static LargeNumber operator *(LargeNumber ln1, LargeNumber ln2)
    {

        if (ln1 == LargeNumber.zero || ln2 == LargeNumber.zero)
        {
            return LargeNumber.zero;
        }

        LargeNumber largeNumber = new LargeNumber();

        float value = ln1.value * ln2.value;
        Debug.Log("Value " + value);
        int exponent = ln1.exponent + ln2.exponent;

        largeNumber.exponent = exponent;
        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator *(LargeNumber ln1, int number)
    {
        if (number == 0)
        {
            return LargeNumber.zero;
        }
        LargeNumber largeNumber = new LargeNumber(0, ln1.exponent);
        float value = ln1.value * number;
        Debug.Log(value);
        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator *(int number, LargeNumber ln1)
    {
        if (number == 0)
        {
            return LargeNumber.zero;
        }
        LargeNumber largeNumber = new LargeNumber(0, ln1.exponent);
        float value = ln1.value * number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator *(LargeNumber ln1, float number)
    {
        if (number == 0)
        {
            return LargeNumber.zero;
        }
        LargeNumber largeNumber = new LargeNumber(0, ln1.exponent);
        float value = ln1.value * number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator *(float number, LargeNumber ln1)
    {
        if (number == 0)
        {
            return LargeNumber.zero;
        }
        LargeNumber largeNumber = new LargeNumber(0, ln1.exponent);
        float value = ln1.value * number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator /(LargeNumber ln1, LargeNumber ln2)
    {
        LargeNumber largeNumber = new LargeNumber();
        float value = ln1.value / ln2.value;
        int exponent = ln1.exponent - ln2.exponent;

        largeNumber.exponent = exponent;
        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator /(LargeNumber ln1, int number)
    {
        LargeNumber largeNumber = new LargeNumber();
        float value = ln1.value / number;

        largeNumber.exponent = ln1.exponent;
        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator /(int number, LargeNumber ln1)
    {
        LargeNumber largeNumber = new LargeNumber();
        float value = ln1.value / number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator /(LargeNumber ln1, float number)
    {
        LargeNumber largeNumber = new LargeNumber();
        float value = ln1.value / number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator /(float number, LargeNumber ln1)
    {
        LargeNumber largeNumber = new LargeNumber();
        float value = ln1.value / number;

        largeNumber.increaseValue(value);

        return largeNumber;
    }
    public static LargeNumber operator -(LargeNumber ln1, LargeNumber ln2)
    {
        LargeNumber largeNumber = new LargeNumber(ln1.value, ln1.exponent);

        largeNumber.decreaseNumber(ln2);

        return largeNumber;
    }
    public static LargeNumber operator -(LargeNumber ln1)
    {
        LargeNumber largeNumber = new LargeNumber(-ln1.value, ln1.exponent);

        return largeNumber;
    }
    static int numlength(int n)
    {
        if (n == 0) return 1;
        int l;
        n = Math.Abs(n);
        for (l = 0; n > 0; ++l)
            n /= 10;
        return l;
    }
    public static void increaseNumber(LargeNumber ln1, LargeNumber ln2)
    {
        int largerExp = 0;
        int smallerExp = 0;

        if (ln1.exponent > ln2.exponent && ln2.exponent != 0)
        {
            largerExp = ln1.exponent % ln2.exponent;
            smallerExp = ln1.exponent - largerExp;

            LargeNumber tempValue = new LargeNumber(ln2.value, smallerExp);
            tempValue.increaseNumber(ln1);

            tempValue.exponent += largerExp;

        }
        else if (ln1.exponent < ln2.exponent && ln1.exponent != 0)
        {
            largerExp = ln2.exponent % ln1.exponent;
            smallerExp = largerExp - ln1.exponent;

            LargeNumber tempValue = new LargeNumber(ln1.value, smallerExp);
            tempValue.increaseNumber(ln2);

            tempValue.exponent += largerExp;
        }
        else
        {
            ln1.increaseValue(ln1.value);
        }
    }

    public static LargeNumber zero = new LargeNumber();
}

public enum LargeNumbers
{
    NONE,
    Thousand,
    Million,
    Billion,
    Trillion,
    Quadrillion,
    Quintillion,
    Sextillion,
    Septillion,
    Octillion,
    Nonillion,
    Decillion,
    Undecillion,
    Duodecillion,
    Tredecillion,
    Quatuordecillion,
    Quindecillion,
    Sexdecillion,
    Septendecillion,
    Octodecillion,
    Novemdecillion,
    Vigintillion,
    Unvigintillion,
    Duovigintillion,
    Tresvigintillion,
    Quatuorvigintillion,
    Quinquavigintillion,
    Sesvigintillion,
    Septemvigintillion,
    Octovigintillion,
    Novemvigintillion,
    Trigintillion,
    Untrigintillion,
    Duotrigintillion,
    Googol,
    Trestrigintillion,
    Quatuortrigintillion,
    Quintrigintillion,
    Sestrigintillion,
    Septentrigintillion,
    Octotrigintillion,
    Novemtrigintillion,
    Quadragintillion,
    Unquadragintillion,
    Duoquadragintillion,
    Tresquadragintillion,
    Quatuorquadragintillion,
    Quinquaquadragintillion,
    Sesquadragintillion,
    Septemquadragintillion,
    Octoquadragintillion,
    Novemquadragintillion,
    Quinquagintillion,
    Unquinquagintillion,
    Duoquinquagintillion,
    Tresquinquagintillion,
    Quatuorquinquagintillion,
    Quinquaquinquagintillion,
    Sesquinquagintillion,
    Septenquinquagintillion,
    Octoquinquagintillion,
    Novemquinquagintillion,
    Sexagintillion,
    Unsexagintillion,
    Duosexagintillion,
    Tressexagintillion,
    Quatuorsexagintillion,
    Quinquasexagintillion,
    Sexasexagintillion,
    Septemsexagintillion,
    Octosexagintillion,
    Novemsexagintillion,
    Septuagintillion,
    Unseptuagintillion,
    Duoseptuagintillion,
    Tresseptuagintillion,
    Quatuorseptuagintillion,
    Quinquaseptuagintillion,
    Sexaseptuagintillion,
    Septenseptuagintillion,
    Octoseptuagintillion,
    Novemseptuagintillion,
    Octogintillion,
    Unoctogintillion,
    Duooctogintillion,
    Tresoctogintillion,
    Quatuoroctogintillion,
    Quinquaoctogintillion,
    Sesoctogintillion,
    Septemoctogintillion,
    Octooctogintillion,
    Novemoctogintillion,
    Nonagintillion,
    Unnonagintillion,
    Duononagintillion,
    Tresnonagintillion,
    Quatuornonagintillion,
    Quinquanonagintillion,
    Sesnonagintillion,
    Septemnonagintillion,
    Octononagintillion,
    Novemnonagintillion,
    Centillion,
    Uncentillion,
    Duocentillion,
    Urecentillion,
    Quattrocentillion,
    Quiencentillion,
    sexcentillion,
    Septemcentillion,
    Octocentillion,
    Novemcentillion,
    Decicentillion,
    Duodicicentillion,
    Viginticentillion,
    Ducentillion,
    Trecentillion,
    Googolcime,
    Quadgintillion,
    Qeingintillion,
    Sscentillion,
    Sepugintillion,
    Octgintillion,
    Nongintillion,
    Novemnongentinontillion,
    Millimillion,
    Billinillion,
    Trillinillion,
    Mevillion,
    Quadrillinillion,
    Quintillinillion,
    Sextillinillion,
    Septillinillion,
    Octillinillion,
    Nonillinillion,
    Decillinillion,
    Vigintillinillion,
    Trigintillinillion,
    Centillinillion,
    Maximusmillion,
    Micrillion,
    Quadmegillion,
    Dumicrillion,
    Tremicrillion,
    Quadmicrillion,
    Quinmicrillion,
    Sexamicrillion,
    Septuamicrillion,
    Octomicrillion,
    Nonamicrillion,
    Microgintillion,
    Microgentillion,
    Maximusbillion,
    Nanillion,
    Quadgigillion,
    Ionsillionoromsilvinoillion,
    Dialogillion,
    Zuglillion,
    Nangentillion,
    Maximustrillion,
    Picillion,
    Quadterillion,
    Illion,
    Picogintillion,
    Picogentillion,
    Exaundevigintillion,
    Femtillion,
    Quadpetillion,
    Femquadillion,
    Femtogintillion,
    Femtogentillion,
    Ramanujillion,
    Attillion,
    Aquadtillion,
    Quadexillion,
    Atquadillion,
    Aquadquadillion,
    Attogintillion,
    Attogentillion,
    Zeptillion,
    Quadzettillion,
    Zepquadillion,
    Zeptogintillion,
    Zeptogentillion,
    Avogadrillion,
    Yoctillion,
    Quadyottillion,
    Yocquadillion,
    Yoctogintillion,
    Yoctogentillion,
    Xonillion,
    Xonogintillion,
    Xonogentillion,
    Vecillion,
    Vecogintillion,
    Vecogentillion,
    Mecillion,
    Mecogintillion,
    Mecogentillion,
    Duecillion,
    Duecogintillion,
    Duecogentillion,
    Trecillion,
    Trecogintillion,
    Trecogentillion,
    Tetrecillion,
    Tetrecogintillion,
    Tetrecogentillion,
    Pentecillion,
    Pentecogintillion,
    Pentecogentillion,
    Hexecillion,
    Hexecogintillion,
    Hexecogentillion,
    Heptecillion,
    Heptecogintillion,
    Heptecogentillion,
    Octecillion,
    Octecogintillion,
    Octecogentillion,
    Ennecillion,
    Ennecogintillion,
    Ennecogentillion,
    Icosillion,
    Vanillion,
    Ogolillion,
    Triacontillion,
    Centixillion,
    Googolillion,
    Gooprolillion,
    Vapillion,
    Tetracontillion,
    Asankhyeyillion,
    Asankhyeyatillion,
    Viftillion,
    Dvajagranisamanillion,
    Pentacontillion,
    Vittillion,
    Hexacontillion,
    Gargoogolillion,
    Vizillion,
    Heptacontillion,
    Viyillion,
    Octacontillion,
    Gazillionillion,
    Vixtillion,
    Ennacontillion,
    Hectillion,
    Ecetillion,
    Faxulillion,
    Uttaraparamanurajahpravesatillion,
    Aocillillion,
    Duehectillion,
    Triahectillion,
    Tetrehectillion,
    Pentehectillion,
    Hexehectillion,
    Heptehectillion,
    Octehectillion,
    Ennehectillion,
    Killillion,
    Quasnillion,
    Quasntillion,
    Killacontillion,
    Killahectillion,
    Megillion,
    Megacontillion,
    Megahectillion,
    Gigillion,
    Trialogillion,
    Gigacontillion,
    Gigahectillion,
    Billillion,
    Terillion,
    Centeillion,
    Teracontillion,
    Terahectillion,
    Petillion,
    Petacontillion,
    Petahectillion,
    Exillion,
    Exacontillion,
    Exahectillion,
    Zettillion,
    Zettacontillion,
    Zettahectillion,
    Yottillion,
    Yottacontillion,
    Yottahectillion,
    Xennillion,
    Xennacontillion,
    Xennahectillion,
    Dakillion,
    Hendillion,
    Hrilliondillion,
    Dokillion,
    Tradakillion,
    Tedakillion,
    Pedakillion,
    Exdakillion,
    Zedakillion,
    Yodakillion,
    Wottillion,
    Nedakillion,
    Ikillion,
    Onillion,
    Ikenillion,
    Icodillion,
    Ictrillion,
    Icterillion,
    Icpetillion,
    Ikectillion,
    Iczetillion,
    Ikyotillion,
    Icxenillion,
    Trakillion,
    Googolplexillion,
    Gogillion,
    Tekillion,
    Pekillion,
    Exakillion,
    Gargoogolplexillion,
    Zakillion,
    Yokillion,
    Nekillion,
    Hotillion,
    Botillion,
    Trotillion,
    Bokkerillion,
    Totillion,
    Potillion,
    Exotillion,
    Zotillion,
    Yootillion,
    Notillion,
    Kalillion,
    Dalillion,
    Tralillion,
    Talillion,
    Palillion,
    Exalillion,
    Zalillion,
    Yalillion,
    Nalillion,
    Dakalillion,
    Hotalillion,
    Totalillion,
    Mejillion,
    Dakejillion,
    Hotejillion,
    Trozacterejillion,
    Gijillion,
    Tetralogillion,
    Dakijillion,
    Hotijillion,
    Astillion,
    Dakastillion,
    Hotastillion,
    Lunillion,
    Dakunillion,
    Hotunillion,
    Fermillion,
    Dakermillion,
    Hotermillion,
    Jovillion,
    Dakovillion,
    Hotovillion,
    Solillion,
    Dakolillion,
    Hotolillion,
    Betillion,
    Daketillion,
    Hotetillion,
    Glocillion,
    Dakocillion,
    Hotocillion,
    Gaxillion,
    Dakaxillion,
    Hotaxillion,
    Supillion,
    Dakupillion,
    Hotupillion,
    Versillion,
    Dakersillion,
    Hotersillion,
    Multillion,
    Dakultillion,
    Hotultillion,
    Botultillion,
    Trotultillion,
    Totultillion,
    Potultillion,
    Exotultillion,
    Zotultillion,
    Yotultillion,
    Notultillion,
    Nodakultillion,
    Notikultillion,
    Notrakultillion,
    Notekultillion,
    Nopekultillion,
    Notexakultillion,
    Nozakultillion,
    Noyokultillion,
    Nonekultillion,
    Nonekenultillion,
    Nonecodultillion,
    Nonectrultillion,
    Nonecterultillion,
    Nonecpetultillion,
    Nonekexultillion,
    Noneczetultillion,
    Nonekyotultillion,
    Nonecxenultillion,
    Pyrillion,
    Dyrillion,
    Tryrillion,
    Tyrillion,
    Peyrillion,
    Exyrillion,
    Zyrillion,
    Yyrillion,
    Nyrillion,
    Dakyrillion,
    Hotyrillion,
    Guntillion,
    Dakuntillion,
    Hotuntillion,
    Hopekuntillion,
    Notuntillion,
    Nonecxenuntillion,
    Kentrillion,
    Dakentrillion,
    Hotentrillion,
    Onlillion,
    Dakonlillion,
    Hotonlillion,
    Paptrillion,
    Dakaptrillion,
    Hotaptrillion,
    Housillion,
    Dakousillion,
    Hotousillion,
    Horsillion,
    Recillion,
    Calcillion,
    Bucrillion,
    Rendillion,
    Katorillion,
    Fretillion,
    Voxillion,
    Avtillion,
    Trongillion,
    Rakillion,
    Dakongillion,
    Hotongillion,
    Demillion,
    Lucetillion,
    Frunillion,
    Pordillion,
    Letrillion,
    Toxillion,
    Sipillion,
    Kokillion,
    Ponnillion,
    Batillion,
    Ekillion,
    Dakatillion,
    Hotatillion,
    Almillion,
    Xudillion,
    Stricillion,
    Aderillion,
    Cantillion,
    Felxillion,
    Phebillion,
    Xoncillion,
    Efonillion,
    Handrillion,
    Dakandrillion,
    Hotandrillion,
    Jenillion,
    Ithodillion,
    Tulfillion,
    Xorvillion,
    Kentillion,
    Sasillion,
    Grakillion,
    Onevillion,
    Ecrillion,
    Mastillion,
    Dakomastillion,
    Hotomastillion,
    Hargillion,
    Rodillion,
    Sedrillion,
    Otillion,
    Otenillion,
    Lelillion,
    Kepillion,
    Dotillion,
    Norillion,
    Gotillion,
    Dakotillion,
    Hototillion,
    Chenillion,
    Hondillion,
    Cratillion,
    Morfillion,
    Teckillion,
    Caxtillion,
    Levtillion,
    Xitillion,
    Tontillion,
    Kortillion,
    Dakortillion,
    Hotortillion,
    Auvillion,
    Dulillion,
    Vetrillion,
    Zerillion,
    Kotillion,
    Dilcillion,
    Tevillion,
    Ocillion,
    Gotaptrillion,
    Nenkillion,
    Dakenkillion,
    Hotenkillion,
    Vopistillion,
    Judillion,
    Paptillion,
    Texillion,
    Naucillion,
    Otestillion,
    Magnillion,
    Fondillion,
    Lovillion,
    Janillion,
    Banillion,
    Tranillion,
    Tanillion,
    Panillion,
    Exanillion,
    Yanillion,
    Noanillion,
    Dakanillion,
    Hotanillion,
    Kuevillion,
    Sassillion,
    Tavillion,
    Ketillion,
    Onekillion,
    Ukhillion,
    Zaquillion,
    Corillion,
    Vauzillion,
    Lagtillion,
    Onexillion,
    Jupillion,
    Bersillion,
    Larillion,
    Fetillion,
    Juntillion,
    Unvillion,
    Vutmillion,
    Fechillion,
    Esticillion,
    Vantillion,
    Ongtillion,
    Laretillion,
    Quothillion,
    Burnillion,
    Gallillion,
    Xeqtillion,
    Lessillion,
    Esthillion,
    Cherillion,
    Zatrillion,
    Lauchillion,
    Gecillion,
    Avistrillion,
    Arsenillion,
    TheInillion,
    Cefillion,
    Uftillion,
    Fethardillion,
    Fetquillion,
    Ecillion,
    Fecillion,
    Zaivillion,
    Quirnillion,
    Thralpillion,
    Fromillion,
    Raztillion,
    Ukrillion,
    Auxillion,
    Raztijillion,
    Datmillion,
    Rephillion,
    Unbillion,
    Fubillion,
    Lontillion,
    Elrillion,
    Kargillion,
    Kargalillion,
    Kargejillion,
    Strontillion,
    Otgillion,
    Suspillion,
    Detrillion,
    Hyttillion,
    Parquillion,
    Fytheillion,
    Qemgillion,
    Obtillion,
    Qemgejillion,
    Zircillion,
    Niobillion,
    Olybdillion,
    Ucnetillion,
    Rethillion,
    Shodillion,
    Naldillion,
    Mejanillion,
    Nostillion,
    Gijanillion,
    Rotillion,
    Astanillion,
    Lunanillion,
    Fermanillion,
    Jovanillion,
    Solanillion,
    Betanillion,
    Febrillion,
    Benmizillion,
    Trenmizillion,
    Ralillion,
    Alillion,
    Glocebrillion,
    Janebrillion,
    Marillion,
    Benbizillion,
    Glocarillion,
    Aprillion,
    Bentrizillion,
    Pentalogillion,
    Mayillion,
    Benquadrizillion,
    Junillion,
    Benquintizillion,
    Julillion,
    Bensextizillion,
    Augillion,
    Benseptizillion,
    Septembillion,
    Benoctizillion,
    Octobillion,
    Bennonizillion,
    Novembillion,
    Bendecizillion,
    Decembillion,
    Benundecizillion,
    Betanenketecembillion,
    Betanenketecembe,
    Undecembillion,
    Benduodecizillion,
    Duodecembillion,
    Bentredecizillion,
    Tredecembillion,
    Benquattuordecizillion,
    Quattuordecembillion,
    Apricosembillion,
    Maicosembillion,
    Junicosembillion,
    Julicosembillion,
    Augicosembillion,
    Septembicosembillion,
    Octobicosembillion,
    Novembicosembillion,
    Decembicosembillion,
    Italillion,
    Italifebrillion,
    Gothillion,
    Ugarillion,
    Hexalogillion,
    Persillion,
    Deserillion,
    Shavillion,
    Osmanillion,
    Cyprillion,
    Phoenillion,
    Kharoshillion,
    Cuneifillion,
    Gothcuneifillion,
    Ugaricuneifillion,
    Persicuneifillion,
    Deserecuneifillion,
    Shavicuneifillion,
    Osmancuneifillion,
    Cypricuneifillion,
    Phoenicuneifillion,
    Kharoshicuneifillion,
    Ceneifillion,
    Ugariceneifillion,
    Persiceneifillion,
    Desereceneifillion,
    Shaviceneifillion,
    Osmanceneifillion,
    Cypriceneifillion,
    Phoeniceneifillion,
    Kharoshiceneifillion,
    Redillion,
    Rediguneifillion,
    Redigeneifillion,
    Bitterswillion,
    Bajillion,
    Orangillion,
    Heptalogillion,
    Goldillion,
    Yellillion,
    Limillion,
    Greenillion,
    Aquillion,
    Blueillion,
    Indigillion,
    Purpillion,
    Violillion,
    Vextillion,
    Muxtillion,
    Magentillion,
    Pinkillion,
    Blackillion,
    Whitillion,
    Grayillion,
    Silvillion,
    Brownillion,
    Handillion,
    Wigillion,
    Purplewigillion,
    Switillion,
    Erkillion,
    Alejandrillion,
    Bazillion,
    Bethillion,
    Octalogillion,
    Bagillion,
    Bridgillion,
    Codillion,
    Courtillion,
    Devillion,
    Duncillion,
    Evillion,
    Ezekillion,
    Geofillion,
    Gwenillion,
    Harolillion,
    Heathillion,
    Izzillion,
    Justillion,
    Katillion,
    Ennalogillion,
    Leshawnillion,
    Lindsillion,
    Noahillion,
    Owenillion,
    Sadillion,
    Sierrillion,
    Trentillion,
    Tylillion,
    Derfillion,
    Brittillion,
    Vocillion,
    Proudillion,
    Trainillion,
    Painillion,
    Babillion,
    Scillion,
    Troptillion,
    Bitillion,
    Mitillion,
    Koptillion,
    Hiptillion,
    Dayillion,
    Ontillion,
    Chirpillion,
    Meakoowillion,
    Brazillion,
    Wookipillion,
    Hoowikillion,
    Deckillion,
    Coopillion,
    Ksurniwunourillion,
    Sfreignihgiegtrillion,
    Qfhfmkucufcrillion,
    Morefrooregteoillion,
    Hoogrgtroootroorotrtooortooillion,
    Ooillion,
    Oooillion,
    Kfedsoroetobuuremilillion,
    Trneigvtrbhtnukroillion,
    Tljdjtoeirnfrnchkvjillion,
    Abcdefghijklmnopqrstuvwxyzillion,
    Erpwotqiuporotureotwiueyuhldsgkjd_ladfgkdsgjghbvcxznbmvcxillion,
    Qwertyuiopasdfghjklzxcvbnmillion,
    Ifghdfjhdfjhvdfbgabhgduhhdvjfdbijdsjgthgjkhdfsjbgdhoidhbghfgbuhillion,
    Qaishehdshfghvhfhbdafhbiuhuighrahhghufrhgauiohgehgierhiqewafhfweihfiehroiihqhrwehrfihfuhuewrhiiuhpwquirhefuwhyoiiouwqrhewgfpuoihuirwheuioprwhhrigehorehigfhrehgurehigehgfuiehgeiuhrgeuhregillion,
    Royardillion,
    Troyardillion,
    Tetroyardillion,
    Pentoyardillion,
    Hexoyardillion,
    Heptoyardillion,
    Octoyardillion,
    Ennoyardillion,
    Hectingillion,
    Killingillion,
    Megingillion,
    Gigingillion,
    Teringillion,
    Petingillion,
    Exingillion,
    Zettingillion,
    Yottingillion,
    Xenningillion,
    Wekingillion,
    Vundingillion,
    Duekingillion,
    Trekingillion,
    Tetrekingillion,
    Pentekingillion,
    Hexekingillion,
    Heptekingillion,
    Octekingillion,
    Ennekingillion,
    Ikingillion,
    Trakingillion,
    Tekingillion,
    Pekingillion,
    Exakingillion,
    Zakingillion,
    Yokingillion,
    Nekingillion,
    Hotingillion,
    Kalingillion,
    Rexhillion,
    Jexhillion,
    Trexhillion,
    Codexhillion,
    Vettexhillion,
    Zexhillion,
    Aitexhillion,
    Niexhillion,
    Dingexhillion,
    Ringexhillion,
    Tringexhillion,
    Codingexhillion,
    Blillion,
    Gablillion,
    Brillion,
    Gabrillion,
    Drillion,
    Gadrillion,
    Chillion,
    Gachillion,
    Shillion,
    Gashillion,
    Mimillion,
    Cengexhillion,
    Gamimillion,
    Lillion,
    Galillion,
    Rillion,
    Garillion,
    Killion,
    Zoogolillion,
    Lynnsdillion,
    Gakillion,
    Dillion,
    Gadillion,
    Trilmillion,
    Gatrilmillion,
    Tillion,
    Gatillion,
    Pillion,
    Gapillion,
    Eksillion,
    Gaeksillion,
    Zilmillion,
    Gazilmillion,
    Yillion,
    Gayillion,
    Nillion,
    Ganillion,
    Villion,
    Gavillion,
    Zhillion,
    Gazhillion,
    Willion,
    Gawillion,
    Hillion,
    Gahillion,
    Okillion,
    Gaokillion,
    Haynillion,
    Gahaynillion,
    Dwillion,
    Gadwillion,
    Gwillion,
    Gagwillion,
    Bwillion,
    Gabwillion,
    Pwillion,
    Gapwillion,
    Fwillion,
    Gafwillion,
    Slillion,
    Gaslillion,
    Samillion,
    Gasamillion,
    Sanillion,
    Gasanillion,
    Smillion,
    Gasmillion,
    Snillion,
    Gasnillion,
    Srillion,
    Gasrillion,
    Swillion,
    Gaswillion,
    Shlillion,
    Gashlillion,
    Shamillion,
    Gashamillion,
    Shanillion,
    Gashanillion,
    Shmillion,
    Gashmillion,
    Shnillion,
    Gashnillion,
    Shrillion,
    Gashrillion,
    Shwillion,
    Gashwillion,
    Zlillion,
    Gazlillion,
    Zamillion,
    Gazamillion,
    Zanillion,
    Gazanillion,
    Zmillion,
    Gazmillion,
    Znillion,
    Gaznillion,
    Zrillion,
    Gazrillion,
    Zwillion,
    Gazwillion,
    Klillion,
    Gaklillion,
    Kamillion,
    Gakamillion,
    Kanillion,
    Gakanillion,
    Qmillion,
    Gaqmillion,
    Qnillion,
    Gaqnillion,
    Krillion,
    Gakrillion,
    Kwazillion,
    Gakwazillion,
    Sapillion,
    Gasapillion,
    Spillion,
    Gaspillion,
    Satillion,
    Gasatillion,
    Stillion,
    Gastillion,
    Sakrillion,
    Gasakrillion,
    Skarillion,
    Gaskarillion,
    Skrillion,
    Gaskrillion,
    Saprillion,
    Gasaprillion,
    Sparillion,
    Gasparillion,
    Sprillion,
    Gasprillion,
    Satrillion,
    Gasatrillion,
    Starillion,
    Gastarillion,
    Strillion,
    Gastrillion,
    Zabillion,
    Gazabillion,
    Zbillion,
    Gazbillion,
    Zadillion,
    Gazadillion,
    Zdillion,
    Gazdillion,
    Zagrillion,
    Gazagrillion,
    Zgarillion,
    Gazgarillion,
    Zgrillion,
    Gazgrillion,
    Zabrillion,
    Gazabrillion,
    Zbarillion,
    Gazbarillion,
    Zbrillion,
    Gazbrillion,
    Zadrillion,
    Gazadrillion,
    Zdarillion,
    Gazdarillion,
    Zdrillion,
    Gazdrillion,
    Zgranillion,
    Gillion,
    Gagillion,
    Gokillion,
    Gagokillion,
    Grillion,
    Gagrillion,
    Joogolillion,
    Gradlillion,
    Antedillion,
    Lustrillion,
    Fekillion,
    Ossillion,
    Nozelillion,
    Brocillion,
    Eglillion,
    Dackelillion,
    Ickelillion,
    Frakelillion,
    Dekelillion,
    Prakelillion,
    Vikelillion,
    Nakelillion,
    Jokelillion,
    Rekelillion,
    Hethelillion,
    Dougillion,
    Rugrillion,
    Renstimpillion,
    Rockillion,
    Aaahhrealmonstillion,
    Kablillion,
    Heyarnoldillion,
    Angrybeavillion,
    Catdogillion,
    Wildthornberrillion,
    Katrudillion,
    Brothersflillion,
    Spongebobsquarepillion,
    Rocketpowillion,
    Pelswillion,
    Astoldbygingillion,
    Fairlyoddparillion,
    Invaderzillion,
    Chalkzonillion,
    Adventuresofjimmyneutrillion,
    Allgrownillion,
    Mylifeasateenagerobillion,
    Dannyphantillion,
    Avatarthelastairbendillion,
    Catscrillion,
    Thexillion,
    Mrmeatillion,
    Eltigrillion,
    Waysidillion,
    Takandthepowerofjujillion,
    Backatthebarnyillion,
    Themightillion,
    Rugratspreschooldazillion,
    Thepenguinsofmadagascillion,
    Fanboyandchumchillion,
    Planetshillion,
    Tuffpuppillion,
    Kungfupandalegensofawesomenillion,
    Thelegendofkorrillion,
    Robotandmonstillion,
    Teenagemutantninjaturtillion,
    Monstersvsalienillion,
    Sanjayandcrillion,
    Rabbidsinvasillion,
    Breadwinnillion,
    Losillion,
    Dosillion,
    Trosillion,
    Tosillion,
    Posillion,
    Exosillion,
    Zosillion,
    Yosillion,
    Nosillion,
    Nakoillion,
    Katudillion,
    Kapudillion,
    Kaexudillion,
    Kazudillion,
    Kayudillion,
    Kanudillion,
    Kadedillion,
    Katredillion,
    Katedillion,
    Kapedillion,
    Kaexedillion,
    Kazedillion,
    Kayedillion,
    Kanedillion,
    Giggolillion,
    Kacillion,
    Austillion,
    Lounillion,
    Furmillion,
    Zovillion,
    Colillion,
    Vetillion,
    Austillioillion,
    Lounillioillion,
    Furmillioillion,
    Zovillioillion,
    Colillioillion,
    Vetillioillion,
    Tritrillion,
    Gaggolillion,
    Grand_tritrillion,
    Gaggolplexillion,
    Tritetillion,
    Geegolillion,
    Tripentillion,
    Gigolillion,
    Goggolillion,
    Trihexillion,
    Triseptillion,
    Gagolillion,
    Trioctillion,
    Gougolillion,
    Guggolillion,
    Gurgolillion,
    Gogolillion,
    Goigolillion,
    Gaxivoogolillion,
    Guugolillion,
    Geergolillion,
    Goomgolillion,
    Gimgolillion,
    Geemgolillion,
    Gomgolillion,
    Gaimgolillion,
    Goumgolillion,
    Gumgolillion,
    Gurmgolillion,
    Goamgolillion,
    Goimgolillion,
    Guumgolillion,
    Twixgolillion,
    Twevgolillion,
    Twaitgolillion,
    Twinegolillion,
    Thirtgolillion,
    Trickgolillion,
    Timegolillion,
    Centgolillion,
    Cellgolillion,
    Catgolillion,
    Cartgolillion,
    Gegolillion,
    Largegolillion,
    Boogolillion,
    Googilillion,
    Gugoldillion,
    Googilillionplexillion,
    Gamoogolillion,
    Coltonillion,
    Guegolillion,
    Grahamillion,
    Corporalillion,
    Tetratrillion,
    Greegoldillion,
    Grinningoldillion,
    Golaagoldillion,
    Gruelohgoldillion,
    Gaspgoldillion,
    Ginorgoldillion,
    Godillion,
    Grand_tridecalillion,
    Gugolthrillion,
    Biggolillion,
    Giggilillion,
    Graatagolthrillion,
    Greegolthrillion,
    Grinningolthrillion,
    Golaagolthrillion,
    Gruelohgolthrillion,
    Gaspgolthrillion,
    Ginorgolthrillion,
    Gugolteslillion,
    Baggolillion,
    Gaggilillion,
    Graatagolteslillion,
    Greegolteslillion,
    Grinningolteslillion,
    Golaagolteslillion,
    Gruelohgolteslillion,
    Gaspgolteslillion,
    Ginorgolteslillion,
    Gugolpetillion,
    Beegolillion,
    Geegilillion,
    Graatagolpetillion,
    Greegolpetillion,
    Grinningolpetillion,
    Golaagolpetillion,
    Gruelohgolpetillion,
    Gaspgolpetillion,
    Ginorgolpetillion,
    Gugolhexillion,
    Bigolillion,
    Gigilillion,
    Graatagolhexillion,
    Greegolhexillion,
    Grinningolhexillion,
    Golaagolhexillion,
    Gruelohgolhexillion,
    Gaspgolhexillion,
    Ginorgolhexillion,
    Gugolheptillion,
    Boggolillion,
    Goggilillion,
    Graatagolheptillion,
    Greegolheptillion,
    Grinningolheptillion,
    Golaagolheptillion,
    Gruelohgolheptillion,
    Gaspgolheptillion,
    Tetrasept,
    Ginorgolheptillion,
    Gugoloctillion,
    Bagolillion,
    Gagilillion,
    Graatagoloctillion,
    Greegoloctillion,
    Grinningoloctillion,
    Golaagoloctillion,
    Gruelohgoloctillion,
    Gaspgoloctillion,
    Ginorgoloctillion,
    Tetraoct,
    Gugolnonillion,
    Bougolillion,
    Gougilillion,
    Graatagolnonillion,
    Greegolnonillion,
    Grinningolnonillion,
    Golaagolnonillion,
    Gruelohgolnonillion,
    Gaspgolnonillion,
    Ginorgolnonillion,
    Gugoldecillion,
    Buggolillion,
    Guggilillion,
    Graatagoldecillion,
    Greegoldecillion,
    Grinningoldecillion,
    Golaagoldecillion,
    Gruelohgoldecillion,
    Generalillion,
    Decoogurgolillion,
    Burgolillion,
    Gurgilillion,
    Bogolillion,
    Boigolillion,
    Buugolillion,
    Beergolillion,
    Boomgolillion,
    Bimgolillion,
    Beemgolillion,
    Bomgolillion,
    Baimgolillion,
    Boumgolillion,
    Bumgolillion,
    Burmgolillion,
    Boamgolillion,
    Boimgolillion,
    Buumgolillion,
    Btwixgolillion,
    Btwevgolillion,
    Btwaitgolillion,
    Btwinegolillion,
    Bthirtgolillion,
    Btrickgolillion,
    Btimegolillion,
    Bcentgolillion,
    Bcellgolillion,
    Bcatgolillion,
    Bcartgolillion,
    Bgegolillion,
    Blargegolillion,
    Throogolillion,
    Troogolillion,
    Troogolillioillion,
    Boogilillion,
    Boogilillioillion,
    Boogilillioillioillion,
    Googalillion,
    Tetracentillion,
    Buegolillion,
    Thrangolillion,
    Threagolillion,
    Thrigangolillion,
    Throrgegolillion,
    Thrulgolillion,
    Thraspgolillion,
    Thrinorgolillion,
    Thrugoldillion,
    Throotrigolillion,
    Triggolillion,
    Pentatrillion,
    Gooltroolheptillion,
    Gooltrooloctillion,
    Gooltroolnonillion,
    Gooltrooldecillion,
    Traggolillion,
    Gaggalillion,
    Gaggalplexillion,
    Pentatetillion,
    Goolteroolheptillion,
    Goolterooloctillion,
    Goolteroolnonillion,
    Goolterooldecillion,
    Treegolillion,
    Pentapentillion,
    Trigolillion,
    Pentahexillion,
    Troggolillion,
    Pentaseptillion,
    Tragolillion,
    Pentaoctillion,
    Trougolillion,
    Pentaennillion,
    Truggolillion,
    Pentadecalillion,
    Trurgolillion,
    Trogolillion,
    Troigolillion,
    Truugolillion,
    Treergolillion,
    Troomgolillion,
    Trimgolillion,
    Treemgolillion,
    Tromgolillion,
    Traimgolillion,
    Troumgolillion,
    Trumgolillion,
    Trurmgolillion,
    Troamgolillion,
    Troimgolillion,
    Truumgolillion,
    Trtwixgolillion,
    Trtwevgolillion,
    Trtwaitgolillion,
    Trtwinegolillion,
    Trthirtgolillion,
    Trtrickgolillion,
    Trtimegolillion,
    Trcentgolillion,
    Trcellgolillion,
    Trcatgolillion,
    Trcartgolillion,
    Trgegolillion,
    Trlargegolillion,
    Baingolillion,
    Teroogolillion,
    Quadroogolillion,
    Truegolillion,
    Terangolillion,
    Tereagolillion,
    Giganteroogolillion,
    Gorgeteroogolillion,
    Gulteroogolillion,
    Terugoldillion,
    Teraatagoldillion,
    Terugolthrillion,
    Terugolteslillion,
    Quadriggolillion,
    Hexatrillion,
    Quadraggolillion,
    Hexatetillion,
    Quadreggolillion,
    Hexapentillion,
    Quadrigolillion,
    Hexahexillion,
    Quadroggolillion,
    Hexaseptillion,
    Quadragolillion,
    Hexaoctillion,
    Quadrougolillion,
    Hexaennillion,
    Quadruggolillion,
    Hexadecalillion,
    Quadrurgolillion,
    Quadrogolillion,
    Quadroigolillion,
    Quadruugolillion,
    Quadreergolillion,
    Quadroomgolillion,
    Quadrimgolillion,
    Quadreemgolillion,
    Quadromgolillion,
    Quadraimgolillion,
    Quadroumgolillion,
    Quadrumgolillion,
    Quadrurmgolillion,
    Quadroamgolillion,
    Quadroimgolillion,
    Quadruumgolillion,
    Quadtwixgolillion,
    Quadtwevgolillion,
    Quadtwaitgolillion,
    Quadtwinegolillion,
    Quadthirtgolillion,
    Quadtrickgolillion,
    Quadtimegolillion,
    Quadcentgolillion,
    Quadcellgolillion,
    Quadcatgolillion,
    Quadcartgolillion,
    Quadgegolillion,
    Quadlargegolillion,
    Petoogolillion,
    Quintoogolillion,
    Quintiggolillion,
    Heptatrillion,
    Quintaggolillion,
    Heptatetillion,
    Quinteegolillion,
    Heptapentillion,
    Quintigolillion,
    Heptahexillion,
    Quintoggolillion,
    Heptaseptillion,
    Quintagolillion,
    Heptaoctillion,
    Quintougolillion,
    Heptaennillion,
    Quintuggolillion,
    Heptadecalillion,
    Ectoogolillion,
    Sextoogolillion,
    Sextiggolillion,
    Octatrillion,
    Sextaggolillion,
    Octatetillion,
    Sexteegolillion,
    Octapentillion,
    Sextigolillion,
    Sextoggolillion,
    Octahexillion,
    Sextagolillion,
    Octaoctillion,
    Sextougolillion,
    Octaennillion,
    Sextuggolillion,
    Octadecalillion,
    Septoogolillion,
    Septiggolillion,
    Septaggolillion,
    Septeegolillion,
    Septigolillion,
    Septoggolillion,
    Septagolillion,
    Nonatrillion,
    Septougolillion,
    Septuggolillion,
    Octoogolillion,
    Decatrillion,
    Octaggolillion,
    Octeegolillion,
    Octigolillion,
    Octoggolillion,
    Octagolillion,
    Iterillion,
    Iterillioillion,
    Iterillioillioillion,
    Nonoogolillion,
    Decoogolillion,
    Undecoogolillion,
    Duodecoogolillion,
    Tredecoogolillion,
    Quattuordecoogolillion,
    Quindecoogolillion,
    Sedecoogolillion,
    Septendecoogolillion,
    Octodecoogolillion,
    Novendecoogolillion,
    Vigintrillion,
    Vigintoogolillion,
    Ultatetillion,
    Ultapentillion,
    Ultahexillion,
    Ultaseptillion,
    Ultaoctillion,
    Ultaennillion,
    Ultradecalillion,
    Superultrillion,
    Trigintoogolillion,
    Quadragintoogolillion,
    Quinquagintoogolillion,
    Sexagintoogolillion,
    Septuagintoogolillion,
    Octogintoogolillion,
    Nonagintoogolillion,
    Hectatrillion,
    Hectatetillion,
    Hectapentillion,
    Hectahexillion,
    Hectaseptillion,
    Hectaoctillion,
    Hectaennillion,
    Goobolillion,
    Godgahlahillion,
    Unooogolillion,
    Scratchoogolillion,
    Cakeoogolillion,
    Kilaoogolillion,
    Embooparamoogolillion,
    Aetheroogolillion,
    Empericidoogolillion,
    Enczoogolillion,
    Everyoogolillion,
    Empigneoogolillion,
    Evenoogolillion,
    Encidoogolillion,
    Elviroogolillion,
    Tricidoogolillion,
    Polypigsidoogolillion,
    Debooparamoogolillion,
    Pigzoogolillion,
    Depigneioogolillion,
    Depigsidoogolillion,
    Depatchopoogolillion,
    Enantibooparamoogolillion,
    Rebooparamoogolillion,
    Enaltsylvionymoogolillion,
    Platoogolillion,
    Twokoogolillion,
    Myriaoogolillion,
    Megaoogolillion,
    Gigaoogolillion,
    Teraoogolillion,
    Petaoogolillion,
    Exaoogolillion,
    Zettaoogolillion,
    Yottaoogolillion,
    Xennaoogolillion,
    Wekaoogolillion,
    Vundaoogolillion,
    Dorsalillion,
    Googodillion,
    Dupertrillion,
    Duperdecalillion,
    Truperdecalillion,
    Quadruperdecalillion,
    Gibbolillion,
    Beafgolillion,
    Latrillion,
    Gabbolillion,
    Lowtrillion,
    Gebbolillion,
    Logtrillion,
    Gibolillion,
    Lagtrillion,
    Gobbolillion,
    Gabolillion,
    Goubolillion,
    Gubbolillion,
    Boobolillion,
    Gamoobolillion,
    Googobillion,
    Bibbolillion,
    Babbolillion,
    Beebolillion,
    Bibolillion,
    Bobbolillion,
    Babolillion,
    Boubolillion,
    Bubbolillion,
    Troobolillion,
    Quadroobolillion,
    Quintoobolillion,
    Sextoobolillion,
    Septoobolillion,
    Octoobolillion,
    Nonoobolillion,
    Decoobolillion,
    Gootrolillion,
    Gitrolillion,
    Gatrolillion,
    Geetrolillion,
    Gietrolillion,
    Gotrolillion,
    Gaitrolillion,
    Bootrolillion,
    Trootrolillion,
    Quadrootrolillion,
    Quintootrolillion,
    Sextootrolillion,
    Septootrolillion,
    Octootrolillion,
    Nonootrolillion,
    Decootrolillion,
    Emperillion,
    Gossolillion,
    Godgoldgahlahillion,
    Gissolillion,
    Gassolillion,
    Geesolillion,
    Gussolillion,
    Hyperalillion,
    Mossolillion,
    Bossolillion,
    Trossolillion,
    Quadrossolillion,
    Quintossolillion,
    Sextossolillion,
    Septossolillion,
    Octossoolillion,
    Nonossolillion,
    Decossolillion,
    Dubolillion,
    Dubolplexillion,
    Dubobbolillion,
    Gommolillion,
    Bommolillion,
    Trommolillion,
    Quadrommolillion,
    Quintommolillion,
    Sextommolillion,
    Septommolillion,
    Octommolillion,
    Nonommolillion,
    Decommolillion,
    Gondolillion,
    Mondolillion,
    Bondolillion,
    Dootrolillion,
    Admiralillion,
    Dossolillion,
    Cyperalillion,
    Trubolillion,
    Tetrossolillion,
    Pentossolillion,
    Hexossolillion,
    Heptossolillion,
    Octossolillion,
    Ennossolillion,
    Xappolillion,
    Megubolillion,
    Gigubolillion,
    Terubolillion,
    Petubolillion,
    Exubolillion,
    Zettubolillion,
    Yottubolillion,
    Grand_Xappolillion,
    Grand_Goxxolillion,
    Ggggolillion,
    Colossolillion,
    Gwtfolillion,
    Gplexolillion,
    Gplexianolillion,
    Gmineolillion,
    Gthingolillion,
    Ggoogololillion,
    Gmegaolillion,
    Wekossolillion,
    Cosmalillion,
    Gdecaolillion,
    Gongulillion,
    Trelumillion,
    Tetrelumillion,
    Pentelumillion,
    Hexelumillion,
    Heptelumillion,
    Octelumillion,
    Ennelumillion,
    Decelumillion,
    Ringulillion,
    Bungilillion,
    Dulatrillion,
    Dulatrillioillion,
    Dulatrillioillioillion,
    Gingulillion,
    Gangulillion,
    Geengulillion,
    Gowngulillion,
    Gungulillion,
    Gagulillion,
    Gougulillion,
    Giggulillion,
    Gogulillion,
    Gaagulillion,
    Gosgulillion,
    Bongulillion,
    Bingulillion,
    Bangulillion,
    Beengulillion,
    Bowngulillion,
    Bungulillion,
    Bagulillion,
    Bougulillion,
    Biggulillion,
    Bogulillion,
    Baagulillion,
    Bosgulillion,
    Trongulillion,
    Quadrongulillion,
    Quintongulillion,
    Sextongulillion,
    Septongulillion,
    Octongulillion,
    Nonongulillion,
    Decongulillion,
    Goplexulillion,
    Goduplexulillion,
    Gotriplexulillion,
    Goquadriplexulillion,
    Goquintiplexulillion,
    Gosextiplexulillion,
    Goseptiplexulillion,
    Gooctiplexulillion,
    Gononiplexulillion,
    Godeciplexulillion,
    Goppatothillion,
    Triakulillion,
    Kungulillion,
    Humongulillion,
    Golapulillion,
    Golaplexlillion,
    Goladexlillion,
    Super_gongulillion,
    Wompogulillion,
    Guapamongillion,
    Bukuwahillion,
    Goshomitillion,
    Boshomitillion,
    Meameamealokkapoowillion,
    Meameamealokkabipoowillion,
    Meameamealokkatripoowillion,
    Meameamealokkaquadripoowillion,
    Meameamealokkaquintipoowillion,
    Meameamealokkaoompillion,
    Meameamealokkapoowaoompillion,
    Meameamealokkapoowoomploompillion,
    Pumbabaillion,
    Tritarillion,
    Quadritarillion,
    Quintitarillion,
    Sextitarillion,
    Septitarillion,
    Octitarillion,
    Nonitarillion,
    Dekotarillion,
    Hektotarillion,
    Kilotarillion,
    Megotarillion,
    Gigotarillion,
    Terotarillion,
    Petotarillion,
    Exotarillion,
    Zettotarillion,
    Yottotarillion,
    Unintarillion,
    Bintarillion,
    Trintarillion,
    Quadrintarillion,
    Quintintarillion,
    Sextintarillion,
    Septintarillion,
    Octintarillion,
    Nonintarillion,
    Dekintarillion,
    Hektintarillion,
    Kilintarillion,
    Megintarillion,
    Gigintarillion,
    Terintarillion,
    Petintarillion,
    Exintarillion,
    Zettintarillion,
    Yottintarillion,
    Tarintarillion,
    Loaderillion,
    Rayillion,
    Oblivillion,
    Utter_Oblivillion,
    Turboillion,
    Powerillion,
    Trispowerillion,
    Terispowerillion,
    Pentispowerillion,
    Dekispowerillion,
    Hektispowerillion,
    Pentopowerillion,
    Trispentopowerillion,
    Hexopowerillion,
    Heptopowerillion,
    Expandopowerillion,
    Multiexpandopowerillion,
    Powerexpandopowerillion,
    Explodopowerillion,
    Detonopowerillion,
    Multpowerillion,
    Metapowerillion,
    Xenopowerillion,
    Hyperpowerillion,
    Omnipowerillion,
    Omegillion,
    Epsilonnaughtillion,
    Gammazerillion,
    Infinitillion,
    Meginitillion,
    Giginitillion,
    Terinitillion,
    Petinitillion,
    Exinitillion,
    Zettinitillion,
    Yottinitillion,
    Xenninitillion,
    Gzggolillion,
    Meaninglessillion,
    Boitillion,
    Troitillion,
    Quadroitillion,
    Infinfinfinity,
    Infaityplex,
    Infinfinfinfinity,
    Infeityplex,
    Infinfinfinfinfinity,
    Infityplex,
    Aarexs_Infinity,
    Aarexs_infinityplex,
    Aarexs_infinityplexian,
    Xpasnfsd,
    Xpasnfsdplex,
    Xpasnfsdplexian,
    Xpasnfsdinfinitinity,
    Xpasnfsdinfinitinityplex,
    Xpasnfsdinfinitinityplexian,
    Xpasnfsdinfinitinitymega,
    Xpasnfsdinfinitinitygiga,
    Xpasnfsdinfinityfinity,
    Xpasnfsdinityityfinity,
    Xpasnfsdxpasnfsd,
    Xpasnfsdxpasnfsdxpasnfsd,
    Infuninmaginity,
    Infuninmaginityplex,
    Infuninmaginablinity,
    Infuninmaginablinityplex,
    Infuninmaginablintyinfplex,
    Infuninmaginablintymegplex,
    Infuninmaginablintygigplex,
    Infuninmaginablinfinitinity,
    Infuninmagxpasnfsd,
    Infuninmaginfuninmaginablinity,
    Infuninmaginfuninmaginfuninmaginablinity,
    Beyondinity,
    Beyonduninmaginity,
    Beyonduninmaginablinityity,
    Beyonduninmaginablinityinfinitinity,
    BeyonduninmaginablXpasnfsdinity,
    BeyonduninmaginablXpasnfsdinityinfinity,
    BeyonduninmaginablXpasnfsdinityXpasnfsdinity,
    Beyondbeyondinity,
    BeyondbeyonduninmaginablXpasnfsdinityXpasnfsdinityillion,
    Beyondbeyonduninmaginablinfinituninmaginablfinityillion,
    Zzingiggity,
    Zzingiggityplex,
    Zzingiggityplexian,
    Bondinity,
    Bondinityplex,
    Trondinity,
    Tetrondinity,
    Pentondinity,
    Hexondinity,
    Heptondinity,
    Octondinity,
    Nonondinity,
    Decondinity,
    Hektondinity,
    Googgondinity,
    Meguntsfinity,
    Giguntsfinity,
    Teruntsfinity,
    Petuntsfinity,
    Exuntsfinity,
    Zettuntsfinity,
    Yottuntsfinity,
    Xzeniafinity,
    Infondinity,
    Infondinityondinity,
    Infindinity,
    Infendinity,
    Infandinity,
    Infundinity,
    InfOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOndinity,
    Mamolony,
    Mabolony,
    Matrolony,
    Maquadrolony,
    Maquintolony,
    Masextolony,
    Maseptolony,
    Maoctolony,
    Manonolony,
    Madecolony,
    Mavigintolony,
    Macentolony,
    Mamillolony,
    Mamicrolony,
    Mananolony,
    Magoogololony,
    Bamolony,
    Babolony,
    Batrolony,
    Baquadrolony,
    Tramolony,
    Trabolony,
    Tratrolony,
    Traquadrolony,
    Quadramolony,
    Quadrabolony,
    Quadratrolony,
    Quadraquadrolony,
    Quintamolony,
    Sextamolony,
    Septamolony,
    Octamolony,
    Nonamolony,
    Decamolony,
    Vigintamolony,
    Centamolony,
    Millamolony,
    Micramolony,
    Nanamolony,
    Googolamolony,
    Mamolojy,
    Mabolojy,
    Matrolojy,
    Maquadrolojy,
    Maquintolojy,
    Bamolojy,
    Tramolojy,
    Quadramolojy,
    Quintamolojy,
    Mamoloty,
    Bamoloty,
    Tramoloty,
    Mamoloey,
    Bamoloey,
    Tramoloey,
    Mamolopy,
    Mamoloxy,
    Mamolozy,
    Mamolocy,
    Mamolomy,
    Mamolody,
    Mamotony,
    Mabotony,
    Mabototy,
    Mabotojy,
    Mamamolony,
    Logungolex,
    Bogungol,
    Trogungol,
    Quadrogungol,
    Mamolonyogungol,
    Logungod,
    Logungog,
    Logungot,
    Logungop,
    Logungoz,
    Logungoy,
    Logungox,
    Logungow,
    Logungov,
    Logungou,
    Logungos,
    Logungor,
    Logungoq,
    Logungoo,
    Logungon,
    Logungom,
    Logungol,
    Logungok,
    Logungoj,
    Logungoi,
    Logungoh,
    Logungof,
    Logungoe,
    Logungoc,
    Logungob,
    Logungoa,
    Logungolony,
    Logunbolony,
    Loguntrolony,
    Logubgolony,
    Logutrgolony,
    Zaongzillion,
    Updogillion,
    Fhlojplexillion,
    Jogxillion,
    Goijooillion,
    Zestillion,
    Zegoraxillion,
    Jogoogafillion,
    Pwangarbarfiggolexmixillion,
    Errillion,
    Duoaillion,
    Lonagaaneillion,
    Eazilllion,
    Vazillion,
    Veanaillion,
    Samaillion,
    Oemnillion,
    LAST
}
